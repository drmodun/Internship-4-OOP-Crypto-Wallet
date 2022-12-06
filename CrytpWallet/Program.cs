// See https://aka.ms/new-console-template for more information
using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Transactions;
using CrytpWallet.Classes.Wallets;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;


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
        "3 - Opozovi Transakciju");
    var userChoice=Console.ReadLine();
    switch (userChoice)
    {
        case "1":
            CreateWallet();
            break;
        case "2":
            CheckWallet();
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
            "\n3 - Solana Wallet");
        var choice = Console.ReadLine();
        switch (choice){
            case "1":
                var newWallet = new BitcoinWallet();
                GlobalWallets.AllBitcoinWallets.Add(newWallet);
                GlobalWallets.Wallets.Add(newWallet);
                Console.WriteLine($"Wallet id {newWallet.Adress} napravljen");
                break;
            case "2":
                var newWalletEtherum = new EtherumWallet();
                GlobalWallets.AllEtherumWallets.Add(newWalletEtherum);
                GlobalWallets.Wallets.Add(newWalletEtherum);
                Console.WriteLine($"Wallet id {newWalletEtherum.Adress} napravljen");

                break;
            case "3":
                var newSolanaWallet = new SolanaWallet();
                GlobalWallets.AllSolanaWallets.Add(newSolanaWallet);
                GlobalWallets.Wallets.Add(newSolanaWallet);
                Console.WriteLine($"Wallet id {newSolanaWallet.Adress} napravljen");

                break;
            case "0":
                return;
            default: Console.WriteLine("Neispravan input");
                    break;
        }
        Console.Clear();
        Console.WriteLine("Pretisnite bilo koju tipku za nastavak");
        Console.ReadLine();
    }
}
void CheckWallet()
{
    foreach (var item in GlobalWallets.Wallets)
    {
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
        Console.Clear();
        Console.WriteLine("Izaberite što želite sa walletom");
        Console.WriteLine("1 - Portfolio" +
            "\n2 - Transfer" +
            "\n3 - Povijest transakcija");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.WriteLine($"Ukupna vrijendnost: {userWallet.totalValue}");

                foreach (var item in userWallet.AmountOfAssets)
                {
                    Console.WriteLine($"Vrijednost (ukupna): {GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value}$ ");
                    Console.WriteLine($"Količina: {item.Value}");
                    GlobalWallets.GetFungibleAssetByAdress(item.Key).PrintAsset();
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
                GlobalWallets.ReCalculateAllWallets();
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
                    switch (receivingWallet.type)
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
                GlobalWallets.AdressPrint();
                Console.WriteLine("");
                var assetToTransferAdress = Console.ReadLine();
                var found = 0;
                foreach (var item in receivingWallet.AmountOfAssets)
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
                        assetToTransfer.UpdateValue();
                        userWallet.SendFungible(assetToTransfer, ammountToTransfer);
                        receivingWallet.GetFungible(assetToTransfer, ammountToTransfer, !receivingWallet.AmountOfAssets.ContainsKey(assetToTransfer.Adress));
                        //Potentially change etherum and solana to one advanced wallet
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
                    if (userWalletNFT != null && receivingWallet!=null )
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
                                var nonFungibleTransaction = new NonFungibleTransaction()
                                {
                                    AdressOfNFT = assetToTransfer.Adress,
                                    Sender = userWallet.Adress,
                                    Receiver = receivingWallet.Adress,

                                };
                                userWalletNFT.SendNFT(assetToTransfer);
                                receivingWalletNFT.GetNFT(assetToTransfer);
                                userWallet.Transactions.Add(nonFungibleTransaction.Id);
                                receivingWallet.Transactions.Add(nonFungibleTransaction.Id);
                                Console.WriteLine("Uspješno napravljen non fungible tranfer");
                                Console.WriteLine("Pretisinte bilo koju tipku za nastavak");
                                Console.ReadLine();
                                break;
                            }
                        }
                    }
                }
                break;

            case "0":
                loop=0;
                break;
        }

        }
        Console.ReadLine();
    }