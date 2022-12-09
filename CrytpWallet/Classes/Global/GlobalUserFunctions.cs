using CrytpWallet.Assets;
using CrytpWallet.Classes.Interfaces;
using CrytpWallet.Classes.Transactions;
using CrytpWallet.Classes.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Global
{
    public static class GlobalUserFunctions 
    {
        public static void TryNonFungibleTransaction(Wallet userWallet, Wallet receivingWallet, Guid assetToTransferAdress)
        {
            List<List<Guid>> lists = new List<List<Guid>>() { BitcoinWallet.AllowedAssets, EtherumWallet.AllowedAssetsFungible, SolanaWallet.AllowedAssetsFungible, EtherumWallet.AllowedNonFungible, SolanaWallet.AllowedNonFungible };
            DoubleWallet userWalletNFT = userWallet as DoubleWallet;
            DoubleWallet receivingWalletNFT = receivingWallet as DoubleWallet;
            if (userWalletNFT.HeldNFT.Contains(assetToTransferAdress) && lists[receivingWallet.Type + 1].Contains(assetToTransferAdress))
            {
                var assetToTransfer = GlobalWallets.GetNonFungibleAssetByAdress(assetToTransferAdress);

                if (!lists[receivingWallet.Type + 1].Contains(assetToTransfer.Adress))
                {
                    Console.WriteLine("Taj wallet ne podržava taj nft");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Izabrani asset je nonfungible asset " + assetToTransfer.Name);
                var confirmationNFT = DialogOfConfirmation();
                if (!confirmationNFT)
                    return;
                MakeTransactionNonFungible(userWalletNFT, receivingWalletNFT, assetToTransfer);

            }
            else
            {
                Console.WriteLine("Nije pronađen ni jedan token sa tom adresom");
                Console.ReadLine();
                return;
            }
        }
        public static void TryFungibleTransaction(Wallet userWallet, Wallet receivingWallet, Guid assetToTransferAdress)
        {
            List<List<Guid>> lists = new List<List<Guid>>() { BitcoinWallet.AllowedAssets, EtherumWallet.AllowedAssetsFungible, SolanaWallet.AllowedAssetsFungible, EtherumWallet.AllowedNonFungible, SolanaWallet.AllowedNonFungible };
            var assetToTransfer = GlobalWallets.GetFungibleAssetByAdress(assetToTransferAdress);
            if (!lists[receivingWallet.Type - 1].Contains(assetToTransfer.Adress))
            {
                Console.WriteLine("Taj Wallet ne podržava taj fungible asset");
                Console.WriteLine("Upišite bilo koju tipku za nastavak");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Upišite količinu asseta koju želite tranferat");
            var ammountToTransferTry = Console.ReadLine();
            var ammountToTransfer = new Decimal(0);
            decimal.TryParse(ammountToTransferTry, out ammountToTransfer);
            if (ammountToTransfer > userWallet.AmountOfAssets[assetToTransferAdress] || ammountToTransfer <= 0)
            {
                Console.WriteLine("Upisana netočna količina");
                Console.ReadLine();
                return;
            }
            var transaction = new FungibleTransaction()
            {
                Sender = userWallet.Adress,
                Receiver = receivingWallet.Adress
            };
            var confirmationTransaction = DialogOfConfirmation();
            Console.WriteLine("Izabrani asset je fungible asset " + assetToTransfer.Name + " količine " + ammountToTransfer.ToString());
            if (!confirmationTransaction)
            {
                return;
            }
            MakeTransactionFungible(userWallet, receivingWallet, ammountToTransfer, assetToTransfer);
        }
        public static void MakeTransactionNonFungible(DoubleWallet userWalletNFT, DoubleWallet receivingWalletNFT, NonFungibleAsset assetToTransfer)
        {
            var nonFungibleTransaction = new NonFungibleTransaction()
            {
                AdressOfNFT = assetToTransfer.Adress,
                Sender = userWalletNFT.Adress,
                Receiver = receivingWalletNFT.Adress,

            };
            GlobalWallets.AllTransactions.Add(nonFungibleTransaction);
            GlobalWallets.GetFungibleAssetByAdress(assetToTransfer.ItsFungible).UpdateValue();
            assetToTransfer.UpdateValue();
            userWalletNFT.SendNFT(assetToTransfer);
            receivingWalletNFT.GetNFT(assetToTransfer);
            userWalletNFT.Transactions.Add(nonFungibleTransaction.Id);
            receivingWalletNFT.Transactions.Add(nonFungibleTransaction.Id);
            Console.WriteLine("Uspješno napravljen non fungible tranfer");
            Console.ReadLine();
        }
        public static void MakeTransactionFungible(Wallet userWallet, Wallet receivingWallet, decimal ammountToTransfer, FungibleAsset assetToTransfer)
        {
            assetToTransfer.UpdateValue();
            userWallet.SendFungible(assetToTransfer, ammountToTransfer);
            receivingWallet.GetFungible(assetToTransfer, ammountToTransfer, !receivingWallet.AmountOfAssets.ContainsKey(assetToTransfer.Adress));
            Console.WriteLine("Uspješno napravljen transfer");
            Console.ReadLine();
            var transaction = new FungibleTransaction()
            {
                AdressOfToken = assetToTransfer.Adress,
                Sender = userWallet.Adress,
                Receiver = receivingWallet.Adress,
                TimeOfTransaction = DateTime.Now,
                StartBalanceReceiver = receivingWallet.AmountOfAssets[assetToTransfer.Adress] - ammountToTransfer,
                EndBalanceReceiver = receivingWallet.AmountOfAssets[assetToTransfer.Adress],
                StartBalanceSender = userWallet.AmountOfAssets[assetToTransfer.Adress] + ammountToTransfer,
                EndBalanceSender = userWallet.AmountOfAssets[assetToTransfer.Adress],
            };
            GlobalWallets.AllTransactions.Add(transaction);
            userWallet.Transactions.Add(transaction.Id);
            receivingWallet.Transactions.Add(transaction.Id);
            return;
        }
        public static void UserTransaction(Wallet userWallet)
        {
            List<List<Guid>> lists = new List<List<Guid>>() { BitcoinWallet.AllowedAssets, EtherumWallet.AllowedAssetsFungible, SolanaWallet.AllowedAssetsFungible, EtherumWallet.AllowedNonFungible, SolanaWallet.AllowedNonFungible };
            Console.Clear();
            Console.WriteLine("Transakcije");
            Console.WriteLine("Upišite adresu walleta koje šaljete asset");
            var adressOfReceivingWallet = Console.ReadLine();
            var receivingWallet = GlobalWallets.GetWalletByAdress(adressOfReceivingWallet);
            if (receivingWallet == null || receivingWallet.Adress == userWallet.Adress)
            {
                Console.WriteLine("Nije upisana pravilna adresa Walleta");
                Console.WriteLine("Pretisnite bilo koju tipku za povratak");
                Console.ReadLine();
                return;
            }
            else
            {
                switch (receivingWallet.Type)
                {
                    case 1:
                        receivingWallet = (BitcoinWallet)receivingWallet;
                        break;
                    case 2:
                        receivingWallet = (EtherumWallet)receivingWallet;
                        break;
                    default:
                        receivingWallet = (SolanaWallet)receivingWallet;
                        break;
                }
            }
            if (receivingWallet.Type != userWallet.Type)
            {
                Console.WriteLine("Nije moguće transferati između ta dva tipa walleta");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Upišite adresu asseta kojeg želite transferati");
            Console.WriteLine("");
            GlobalWallets.AdressPrint(userWallet.Adress, receivingWallet.Adress);
            Console.WriteLine("Asset kojega želite transferati: ");
            var assetToTransferAdressTry = Console.ReadLine();
            var assetToTransferAdress = Guid.Empty;
            Guid.TryParse(assetToTransferAdressTry, out assetToTransferAdress);
            if (userWallet.AmountOfAssets.ContainsKey(assetToTransferAdress))
            {
                TryFungibleTransaction(userWallet, receivingWallet, assetToTransferAdress);
                return;

            }
            else if (userWallet as DoubleWallet == null)
            {
                Console.WriteLine("Nije pronađen taj asset");
                Console.ReadLine();
                return;
            }
            else
            {
                TryNonFungibleTransaction(userWallet, receivingWallet, assetToTransferAdress);
                return;
            }

        }
        public static void PrintWalletAssets(Wallet userWallet)
        {
            Console.Clear();
            userWallet.PrintWallet();
            Console.WriteLine("");
            foreach (var item in userWallet.AmountOfAssets)
            {

                GlobalWallets.GetFungibleAssetByAdress(item.Key).PrintAsset();
                Console.WriteLine($"Vrijednost (ukupna): {Decimal.Round(GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value)}$ ");
                Console.WriteLine($"Količina: {Decimal.Round(item.Value, 4)}");
                Console.WriteLine(" ");
            }

            if (userWallet as DoubleWallet != null)
            {
                foreach (var item in ((DoubleWallet)userWallet).HeldNFT)
                {
                    GlobalWallets.GetNonFungibleAssetByAdress(item).PrintAsset();
                    Console.WriteLine(" ");
                }
            }

            Console.WriteLine("Pretisnite bilo koju tipku za nastavak");
            Console.ReadLine();

        }
        public static Wallet PrintAndChooseWallet()
        {
            Console.Clear();
            foreach (var item in GlobalWallets.Wallets)
            {
                if (item.Type==1)
                {
                    ((BitcoinWallet)item).PrintWallet();
                }
                else if (item.Type==2)
                {
                    ((EtherumWallet)item).PrintWallet();
                }
                else
                {
                    ((SolanaWallet)item).PrintWallet();
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("Unesite kojem walletu želite pristupiti");
            var userWalletAdress = Console.ReadLine();
            if (GlobalWallets.Wallets.Find(x => x.Adress.ToString() == userWalletAdress) == null)
            {
                Console.WriteLine("Nije upisani pravi wallet");
                Console.ReadLine();
                return null;
            }
            var userWallet = GlobalWallets.Wallets.Find(x => x.Adress.ToString() == userWalletAdress);
            return userWallet;
        }
        public static bool DialogOfConfirmation()
        {
            Console.WriteLine("Ova akcija će zauvijek promjeniti podatke aplikacije, jeste li sigurni da je želite napraviti" +
                "\n1-DA" +
                "\n0-NE");
            var confirmation = Console.ReadLine();
            if (confirmation == "1")
                return true;
            return false;
        }
        public static void RecallTransaction(Wallet userWallet)
        {
            Console.WriteLine("Upišite adresu transakcije koju želite opozvati");
            var adressOfTransactionTry = Console.ReadLine();
            Guid adressOfTransaction = Guid.Empty;
            Guid.TryParse(adressOfTransactionTry, out adressOfTransaction);
            if (!userWallet.Transactions.Contains(adressOfTransaction) || adressOfTransaction == Guid.Empty)
            {
                Console.WriteLine("Nije upisan pravilan Id transakcije");
                Console.ReadLine();
                return;
            }
            var transactionToRecall = GlobalWallets.GetTransactionById(adressOfTransaction);
            if (transactionToRecall.Recalled)
            {
                Console.WriteLine("Ta transakcija je već opozvana");
                Console.ReadLine();
                return;
            }
            if ((DateTime.Now - transactionToRecall.TimeOfTransaction).TotalSeconds > 45)
            {
                Console.WriteLine("Prošlo je više od 45 sekunda od prošle transakcije");
                Console.ReadLine();
                return;
            }
            if (userWallet.Adress != transactionToRecall.Sender)
            {
                Console.WriteLine("Morate opozvati reakciju s walleta koji ju je napravio");
                Console.ReadLine();
                return;
            }
            var confirmation = DialogOfConfirmation();
            if (!confirmation)
                return;
            Wallet sendWallet;
            Wallet receiveWallet;
            receiveWallet = userWallet;
            sendWallet = GlobalWallets.GetWalletByAdress(transactionToRecall.Receiver.ToString());

            if (transactionToRecall as NonFungibleTransaction == null)
            {
                var transactionToRecallFungible = transactionToRecall as FungibleTransaction;
                sendWallet.SendFungible(GlobalWallets.GetFungibleAssetByAdress(transactionToRecallFungible.AdressOfToken), (decimal)(transactionToRecallFungible.EndBalanceReceiver - transactionToRecallFungible.StartBalanceReceiver));
                receiveWallet.GetFungible(GlobalWallets.GetFungibleAssetByAdress(transactionToRecallFungible.AdressOfToken), (decimal)(transactionToRecallFungible.EndBalanceReceiver - transactionToRecallFungible.StartBalanceReceiver), false);
                transactionToRecall.Recalled = true;
            }
            else
            {
                var transactionToRecallNonFungible = transactionToRecall as NonFungibleTransaction;
                ((DoubleWallet)sendWallet).SendNFT(GlobalWallets.GetNonFungibleAssetByAdress(transactionToRecallNonFungible.AdressOfNFT));
                ((DoubleWallet)receiveWallet).GetNFT(GlobalWallets.GetNonFungibleAssetByAdress(transactionToRecallNonFungible.AdressOfNFT));
                transactionToRecall.Recalled = true;
            }
            Console.WriteLine("Uspješno recallana transakcija, recallanje samo vraca kolicinu izmjenjenju između walleta, ali neće vratiti proslu vrijdnost Fungible tokena");
            Console.ReadLine();
        }
        public static void PrintAndChooseTransaction(Wallet userWallet)
        {
            Console.Clear();
            if (userWallet.Transactions.Count == 0)
            {
                Console.WriteLine("Na tom walletu nije napravljena ni jedna transakcija");
                Console.ReadLine();
                return;
            }
            userWallet.PrintAllTransactions();
            Console.WriteLine("Pretisnite 1 da biste poćeli proces opozivanja transakcije");
            var choiceTransaction = Console.ReadLine();
            if (choiceTransaction != "1")
            {
                return;
            }
            RecallTransaction(userWallet);

            return;
        }
    }
}
