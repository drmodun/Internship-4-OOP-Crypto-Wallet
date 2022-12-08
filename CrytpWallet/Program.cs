// See https://aka.ms/new-console-template for more information
using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Transactions;
using CrytpWallet.Classes.Wallets;


Console.WriteLine("Hello, World!");
//main loop
bool loop=true;

bool DialogOfConfirmation()
{
    Console.WriteLine("Ova akcija će zauvijek promjeniti podatke aplikacije, jeste li sigurni da je želite napraviti" +
        "\n1-DA" +
        "\n0-NE");
    var confirmaiton=Console.ReadLine();
    if (confirmaiton=="1")
        return true;
    return false;
}

var BTC = new FungibleAsset(90)
{
    Adress = Guid.NewGuid(),
    Name = "Bitcoin",
    Label="BTC"

};
//GlobalWallets.PrintAll();
while (loop)
{
    Console.Clear();
    Console.WriteLine("1 - Kreiraj Wallet\n" +
        "2 - Posjeti Wallet\n" +
        "0 - Izlaz iz aplikacije");
    var userChoice=Console.ReadLine();
    switch (userChoice)
    {
        case "1":
            CreateWallet();
            break;
        case "2":
            CheckWallet();
            break;
        case "0":
            Environment.Exit(0);
            break;
    }
}
void CreateWallet()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Koji wallet želite kreirati?");
        Console.WriteLine("1 - Bitcoin Wallet " +
            "\n2 - Etherum Wallet" +
            "\n3 - Solana Wallet" +
            "\n0 - Main Menu");
        var choice = Console.ReadLine();
        switch (choice){
            case "1":
                var newWallet = new BitcoinWallet() { IsPredefined = false };
                GlobalWallets.AllBitcoinWallets.Add(newWallet);
                GlobalWallets.Wallets.Add(newWallet);
                newWallet.CalculateValue();
                Console.Clear() ;
                Console.WriteLine($"Wallet id {newWallet.Adress} napravljen");
                break;
            case "2":
                var newWalletEtherum = new EtherumWallet() { IsPredefined = false};
                GlobalWallets.AllEtherumWallets.Add(newWalletEtherum);
                GlobalWallets.Wallets.Add(newWalletEtherum);
                Console.Clear() ;
                newWalletEtherum.CalculateValue();
                Console.WriteLine($"Wallet id {newWalletEtherum.Adress} napravljen");

                break;
            case "3":
                var newSolanaWallet = new SolanaWallet() { IsPredefined = false };
                GlobalWallets.AllSolanaWallets.Add(newSolanaWallet);
                GlobalWallets.Wallets.Add(newSolanaWallet);
                newSolanaWallet.CalculateValue();
                Console.Clear() ;
                Console.WriteLine($"Wallet id {newSolanaWallet.Adress} napravljen");

                break;
            case "0":
                return;
            default: Console.WriteLine("Neispravan input");
                    break;
        }
        Console.WriteLine("Pretisnite bilo koju tipku za nastavak");
        Console.ReadLine();
    }
}
void CheckWallet()
{
    Console.Clear();
    foreach (var item in GlobalWallets.Wallets)
    {
        //cahnge this part to make sense
        if (GlobalWallets.AllBitcoinWallets.Find(x => x.Adress == item.Adress) != null)
        {
            ((BitcoinWallet)item).PrintWallet();
        }
        else if (GlobalWallets.AllEtherumWallets.Find(x => x.Adress == item.Adress) != null)
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
    if (GlobalWallets.Wallets.Find(x=>x.Adress.ToString() == userWalletAdress) == null)
    {
        Console.WriteLine("Nije upisani pravi wallet");
        Console.WriteLine("Pretisnite bilo kojio gumb za vraćanje na main menu");
        Console.ReadLine();
        return;
    }
    var userWallet = GlobalWallets.Wallets.Find(x => x.Adress.ToString() == userWalletAdress);
    var type = 0;
    if ( userWallet as BitcoinWallet != null)
    {
        userWallet=(BitcoinWallet)userWallet;
        type = 1;
    }
    else if(userWallet as EtherumWallet !=null)
    {
        userWallet =(EtherumWallet)userWallet;
        type=2;
    }
    else
    {
        userWallet = (SolanaWallet)userWallet;
        type=3;
    }
    Console.Clear();
    var loop = 1;
    while (loop==1)
    {
        GlobalWallets.ReCalculateAllWallets();
        Console.Clear();
        Console.WriteLine("Izaberite što želite sa walletom");
        Console.WriteLine("1 - Portfolio" +
            "\n2 - Transfer" +
            "\n3 - Povijest transakcija" +
            "\n0 - Main Menu");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Clear();
                userWallet.PrintWallet();
                Console.WriteLine("");
                foreach (var item in userWallet.AmountOfAssets)
                {
                    
                    GlobalWallets.GetFungibleAssetByAdress(item.Key).PrintAsset();
                    Console.WriteLine($"Vrijednost (ukupna): {GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value}$ ");
                    Console.WriteLine($"Količina: {item.Value}");
                    Console.WriteLine(" ");
                }

                if (userWallet is EtherumWallet)
                {
                    foreach (var item in ((EtherumWallet)userWallet).HeldNFT)
                    {
                        GlobalWallets.GetNonFungibleAssetByAdress(item).PrintAsset();
                        Console.WriteLine(" ");
                    }
                }
                else if (userWallet is SolanaWallet)
                {
                    foreach (var item in ((SolanaWallet)userWallet).HeldNFT)
                    {
                        GlobalWallets.GetNonFungibleAssetByAdress(item).PrintAsset();
                        Console.WriteLine(" ");
                    }
                }

                Console.WriteLine("Pretisnite bilo koju tipku za nastavak");
                Console.ReadLine();
                break;

            case "2":
                List<List<Guid>> lists = new List<List<Guid>>() { BitcoinWallet.AllowedAssets, EtherumWallet.AllowedAssetsFungible, SolanaWallet.AllowedAssetsFungible, EtherumWallet.AllowedNonFungible, SolanaWallet.AllowedNonFungible };
                Console.Clear();
                Console.WriteLine("Transakcije");
                Console.WriteLine("Upišite adresu walleta koje šaljete asset");
                var adressOfReceivingWallet=Console.ReadLine();
                var receivingWallet = GlobalWallets.GetWalletByAdress(adressOfReceivingWallet);
                var typeReceiver = 0;
                if (receivingWallet == null || receivingWallet.Adress==userWallet.Adress)
                {
                    Console.WriteLine("Nije upisana pravilna adresa Walleta");
                    Console.WriteLine("Pretisnite bilo koju tipku za povratak");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    switch (receivingWallet.Type)
                    {
                        case 1:
                            typeReceiver = 1;
                            receivingWallet = (BitcoinWallet)receivingWallet;
                            break;
                        case 2:
                            typeReceiver = 2;
                            receivingWallet = (EtherumWallet)receivingWallet;
                            break;
                        default:
                            typeReceiver = 3;
                            receivingWallet = (SolanaWallet)receivingWallet;
                            break;
                    }
                }
                Console.WriteLine("Upišite adresu asseta kojeg želite transferati");
                Console.WriteLine("");
                GlobalWallets.AdressPrint(userWallet.Adress, receivingWallet.Adress);
                Console.WriteLine("");
                var assetToTransferAdress = Console.ReadLine();
                var found = 0;
                foreach (var item in userWallet.AmountOfAssets)
                {
                    if (assetToTransferAdress == item.Key.ToString())
                    {
                        var assetToTransfer = GlobalWallets.GetFungibleAssetByAdress(item.Key);

                        if (!lists[typeReceiver - 1].Contains(assetToTransfer.Adress))
                        {
                            Console.WriteLine("Taj Wallet ne podržava taj fungible asset");
                            Console.WriteLine("Upišite bilo koju tipku za nastavak");
                            Console.ReadLine();
                            found = 2;
                            break;
                        }

                        Console.WriteLine("Upišite količinu asseta koju želite tranferat");
                        var ammountToTransferTry = Console.ReadLine();
                        var ammountToTransfer = 0;
                        int.TryParse(ammountToTransferTry, out ammountToTransfer);
                        if (ammountToTransfer>item.Value || ammountToTransfer <= 0)
                        {
                            Console.WriteLine("Upisani netočna količina");
                            Console.WriteLine("Upišite bilo koju tipku za nastavak");
                            Console.ReadLine();
                            found = 2;
                            break;
                        }
                        var transaction = new FungibleTransaction()
                        {
                            Sender = userWallet.Adress,
                            Receiver = receivingWallet.Adress
                        };
                        var confirmationTransactionRecall = DialogOfConfirmation();
                        if (!confirmationTransactionRecall)
                            break;
                        assetToTransfer.UpdateValue();
                        userWallet.SendFungible(assetToTransfer, ammountToTransfer);
                        receivingWallet.GetFungible(assetToTransfer, ammountToTransfer, !receivingWallet.AmountOfAssets.ContainsKey(assetToTransfer.Adress));
                        found = 1;
                        Console.WriteLine("Uspješno narpavljen transfer");
                        Console.ReadLine();
                        transaction = new FungibleTransaction()
                        {
                            AdressOfToken = assetToTransfer.Adress,
                            Sender = userWallet.Adress,
                            Receiver = receivingWallet.Adress,
                            TimeOfTransaction = DateTime.Now,
                            StartBalanceReceiver = receivingWallet.AmountOfAssets[assetToTransfer.Adress]-ammountToTransfer,
                            EndBalanceReceiver = receivingWallet.AmountOfAssets[assetToTransfer.Adress],
                            StartBalanceSender = userWallet.AmountOfAssets[assetToTransfer.Adress]+ammountToTransfer,
                            EndBalanceSender = userWallet.AmountOfAssets[assetToTransfer.Adress],
                        };
                        GlobalWallets.AllTransactions.Add(transaction);
                        userWallet.Transactions.Add(transaction.Id);
                        receivingWallet.Transactions.Add(transaction.Id);
                        //switch typewith enum
                        break;
                    }
                }
                if (found == 2)
                {
                    Console.WriteLine("Wallet ili token nije pronađen");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    DoubleWallet userWalletNFT = userWallet as DoubleWallet;
                    DoubleWallet receivingWalletNFT= receivingWallet as DoubleWallet;
                    if (userWalletNFT != null && receivingWallet != null)
                    {
                        foreach (var item in userWalletNFT.HeldNFT)
                        {
                            if (item.ToString() == assetToTransferAdress)
                            {
                                var assetToTransfer = GlobalWallets.GetNonFungibleAssetByAdress(item);

                                if (!lists[typeReceiver + 1].Contains(assetToTransfer.Adress))
                                {
                                    Console.WriteLine("Taj wallet ne podržava taj nft");
                                    Console.ReadLine();
                                    break;
                                }
                                var confirmationNFT = DialogOfConfirmation();
                                if (!confirmationNFT)
                                    break;
                                var nonFungibleTransaction = new NonFungibleTransaction()
                                {
                                    AdressOfNFT = assetToTransfer.Adress,
                                    Sender = userWallet.Adress,
                                    Receiver = receivingWallet.Adress,

                                };
                                GlobalWallets.AllTransactions.Add(nonFungibleTransaction);
                                GlobalWallets.GetFungibleAssetByAdress(assetToTransfer.ItsFungible).UpdateValue();
                                assetToTransfer.UpdateValue();
                                userWalletNFT.SendNFT(assetToTransfer);
                                receivingWalletNFT.GetNFT(assetToTransfer);
                                userWallet.Transactions.Add(nonFungibleTransaction.Id);
                                receivingWallet.Transactions.Add(nonFungibleTransaction.Id);
                                Console.WriteLine("Uspješno napravljen non fungible tranfer");
                                Console.WriteLine("Pretisinte bilo koju tipku za nastavak");
                                Console.ReadLine();
                                found = 1;
                                break;
                            }
                        }
                        if (found==0){
                            Console.WriteLine("Nije pronađen ni jedan token sa tom adresom");
                            Console.ReadLine();
                        }
                    }
                }
                break;
            case "3":
                Console.Clear();
                if (userWallet.Transactions.Count == 0)
                {
                    Console.WriteLine("Na tom walletu nije napravljena ni jedna transakcija");
                    Console.WriteLine("Upišite bilo koju tipku za vratiti se na menu");
                    Console.ReadLine();
                    break;
                }
                userWallet.PrintAllTransactions();
                Console.WriteLine("Pretisnite 1 da biste poćeli proces opozivanja transakcije");
                var choiceTransaction = Console.ReadLine();
                if (choiceTransaction != "1") {
                    break;
                }
                Console.WriteLine("Upišite adresu transakcije koju želite opozvati");
                var adressOfTransactionTry = Console.ReadLine();
                Guid adressOfTransaction = Guid.Empty;
                Guid.TryParse(adressOfTransactionTry, out adressOfTransaction);
                if (!userWallet.Transactions.Contains(adressOfTransaction) || adressOfTransaction==Guid.Empty)
                {
                    Console.WriteLine("Nije upisan pravilan Id transakcije");
                    Console.ReadLine();
                    break;
                }
                var transactionToRecall = GlobalWallets.GetTransactionById(adressOfTransaction);
                if (transactionToRecall.Recalled)
                {
                    Console.WriteLine("Ta transakcija je već opozvana");
                    Console.ReadLine();
                    break;
                }
                if ((DateTime.Now - transactionToRecall.TimeOfTransaction).TotalSeconds > 45)
                {
                    Console.WriteLine("Prošlo je više od 45 sekunda od prošle transakcije");
                    Console.ReadLine();
                    break;
                }
                var confirmation = DialogOfConfirmation();
                if (!confirmation)
                    break;
                Wallet sendWallet = null;
                Wallet receiveWallet = null;
                if (userWallet.Adress != transactionToRecall.Sender)
                {
                    sendWallet= userWallet;
                    receiveWallet= GlobalWallets.GetWalletByAdress(transactionToRecall.Sender.ToString());

                }
                else
                {
                    receiveWallet = userWallet;
                    sendWallet = GlobalWallets.GetWalletByAdress(transactionToRecall.Receiver.ToString());
                }

                if (transactionToRecall as NonFungibleTransaction == null)
                {
                    var transactionToRecallFungible= transactionToRecall as FungibleTransaction;
                    sendWallet.SendFungible(GlobalWallets.GetFungibleAssetByAdress(transactionToRecallFungible.AdressOfToken), (int)(transactionToRecallFungible.EndBalanceReceiver - transactionToRecallFungible.StartBalanceReceiver));
                    receiveWallet.GetFungible(GlobalWallets.GetFungibleAssetByAdress(transactionToRecallFungible.AdressOfToken), (int)(transactionToRecallFungible.EndBalanceReceiver - transactionToRecallFungible.StartBalanceReceiver), false);
                    transactionToRecall.Recalled= true;  
                }
                //transactionToRecall.Recalled=true;
                else
                {
                    var transactionToRecallNonFungible = transactionToRecall as NonFungibleTransaction;
                    ((DoubleWallet)sendWallet).SendNFT(GlobalWallets.GetNonFungibleAssetByAdress(transactionToRecallNonFungible.AdressOfNFT));
                    ((DoubleWallet)receiveWallet).GetNFT(GlobalWallets.GetNonFungibleAssetByAdress(transactionToRecallNonFungible.AdressOfNFT));
                    transactionToRecall.Recalled= true;
                }
                Console.WriteLine("Uspješno recallana transakcija, recallanje samo vraca kolicinu izmjenjenju između walleta, ali neće vratiti proslu vrijdnost Fungible tokena");
                Console.ReadLine();
                break;
            case "0":
                loop=0;
                break;
            default: Console.WriteLine("Nije upisan valjani input");Console.ReadLine();break;
        }

        }
    }