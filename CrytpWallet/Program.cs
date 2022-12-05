// See https://aka.ms/new-console-template for more information
using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Wallets;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
//main loop
bool loop=true;

var BTC = new FungibleAsset(90)
{
    Adress = Guid.NewGuid(),
    Name = "Bitcoin",
    Label="BTC"

};

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
    
    if ( userWallet as BitcoinWallet != null)
    {
        userWallet=(BitcoinWallet)userWallet;
    }
    else if(userWallet as EtherumWallet !=null)
    {
        userWallet =(EtherumWallet)userWallet;
    }
    else
    {
        userWallet = (SolanaWallet)userWallet;
    }
    Console.Clear();
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Izaberite što želite sa walletom");
        Console.WriteLine("1 - Portfolio" +
            "\n2 - Etherum Wallet" +
            "\n3 - Solana Wallet");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine($"Ukupna vrijendnost: {userWallet.totalValue}");
                foreach (var item in userWallet.AmountOfAssets)
                {
                    Console.WriteLine($"Količina: {item.Value}");
                    GlobalWallets.AllFungibleAssets.Find(x => x.Adress == item.Key).PrintAsset();
                    Console.WriteLine(" ");
                }
                if (userWallet is EtherumWallet)
                {
                    foreach (var item in ((EtherumWallet)userWallet).HeldNFT)
                    {
                        GlobalWallets.AllNonFungibleAssets.Find(x => x.Adress == item).PrintAsset();
                        Console.WriteLine(" ");
                    }
                }
                else if (userWallet is SolanaWallet)
                {
                    foreach (var item in ((SolanaWallet)userWallet).HeldNFT)
                    {
                        GlobalWallets.AllNonFungibleAssets.Find(x => x.Adress == item).PrintAsset();
                        Console.WriteLine(" ");
                    }
                }
                break;
        }

        }
        Console.ReadLine();
    }