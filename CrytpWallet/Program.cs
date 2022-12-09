using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Wallets;

bool loop=true;
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
                var confirmationBitcoin = GlobalUserFunctions.DialogOfConfirmation();
                if (!confirmationBitcoin)
                    return;
                Console.WriteLine($"Wallet Bitcoin adrese {newWallet.Adress} napravljen");
                break;
            case "2":
                var newWalletEtherum = new EtherumWallet() { IsPredefined = false};
                GlobalWallets.AllEtherumWallets.Add(newWalletEtherum);
                GlobalWallets.Wallets.Add(newWalletEtherum);
                Console.Clear() ;
                newWalletEtherum.CalculateValue();
                var confirmationEtherum = GlobalUserFunctions.DialogOfConfirmation();
                if (!confirmationEtherum)
                    return;
                Console.WriteLine($"Wallet id {newWalletEtherum.Adress} napravljen");

                break;
            case "3":
                var newSolanaWallet = new SolanaWallet() { IsPredefined = false };
                GlobalWallets.AllSolanaWallets.Add(newSolanaWallet);
                GlobalWallets.Wallets.Add(newSolanaWallet);
                newSolanaWallet.CalculateValue();
                Console.Clear() ;
                var confirmationSolana = GlobalUserFunctions.DialogOfConfirmation();
                if (!confirmationSolana)
                {
                    return;
                }
                Console.WriteLine($"Wallet id {newSolanaWallet.Adress} napravljen");

                break;
            case "0":
                return;
            default: Console.WriteLine("Neispravan input"); Console.ReadLine();
                    break;
        }
        Console.WriteLine("Pretisnite bilo koju tipku za nastavak");
        Console.ReadLine();
    }
}
void CheckWallet()
{

    var userWallet = GlobalUserFunctions.PrintAndChooseWallet();
    if (userWallet == null)
        return;
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
               GlobalUserFunctions.PrintWalletAssets(userWallet);
                break;

            case "2":
                GlobalUserFunctions.UserTransaction(userWallet);
                break;
            case "3":
               GlobalUserFunctions.PrintAndChooseTransaction(userWallet);
                break;
            case "0":
                loop=0;
                break;
            default: Console.WriteLine("Nije upisan valjani input");Console.ReadLine();break;
        }

        }
    }