// See https://aka.ms/new-console-template for more information
using CrytpWallet.Assets;
using CrytpWallet.Classes.Wallets;

Console.WriteLine("Hello, World!");
//main loop
bool loop=true;
var ListOfWallets = new List<Wallet>();
var ListOfBitcoinWallets=new List<BitcoinWallet>();
var ListOfEtheriumWallets = new List<EtherumWallet>();
var ListOfSolanaWallets=new List<SolanaWallet>();
var BTC = new FungableAsset(90)
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
                ListOfBitcoinWallets.Add(newWallet);
                ListOfWallets.Add(newWallet);
                Console.WriteLine($"Wallet id {newWallet.Adress} napravljen");
                break;
            case "2":
                var newWalletEtherum = new EtherumWallet();
                ListOfEtheriumWallets.Add(newWalletEtherum);
                ListOfWallets.Add(newWalletEtherum);
                Console.WriteLine($"Wallet id {newWalletEtherum.Adress} napravljen");

                break;
            case "3":
                var newSolanaWallet = new SolanaWallet();
                ListOfSolanaWallets.Add(newSolanaWallet);
                ListOfWallets.Add(newSolanaWallet);
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
    foreach(var item in ListOfBitcoinWallets)
    {
        item.PrintWallet();
    }
    Console.ReadLine();
}