// See https://aka.ms/new-console-template for more information
using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Transactions;
using CrytpWallet.Classes.Wallets;

bool loop=true;
void TryNonFungibleTransaction(Wallet userWallet, Wallet receivingWallet, Guid assetToTransferAdress)
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
void TryFungibleTransaction(Wallet userWallet, Wallet receivingWallet, Guid assetToTransferAdress)
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
void MakeTransactionNonFungible(DoubleWallet userWalletNFT, DoubleWallet receivingWalletNFT, NonFungibleAsset assetToTransfer)
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
void MakeTransactionFungible (Wallet userWallet, Wallet receivingWallet, decimal ammountToTransfer, FungibleAsset assetToTransfer)
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
void UserTransaction(Wallet userWallet)
{
    List<List<Guid>> lists = new List<List<Guid>>() { BitcoinWallet.AllowedAssets, EtherumWallet.AllowedAssetsFungible, SolanaWallet.AllowedAssetsFungible, EtherumWallet.AllowedNonFungible, SolanaWallet.AllowedNonFungible };
    Console.Clear();
    Console.WriteLine("Transakcije");
    Console.WriteLine("Upišite adresu walleta koje šaljete asset");
    var adressOfReceivingWallet = Console.ReadLine();
    var receivingWallet = GlobalWallets.GetWalletByAdress(adressOfReceivingWallet);
    var typeReceiver = 0;
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
    if (receivingWallet.Type != userWallet.Type)
    {
        Console.WriteLine("Nije moguće transferati između ta dva tipa walleta");
        Console.ReadLine();
        return;
    }
    Console.WriteLine("Upišite adresu asseta kojeg želite transferati");
    Console.WriteLine("");
    GlobalWallets.AdressPrint(userWallet.Adress, receivingWallet.Adress);
    Console.WriteLine("");
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
void PrintWalletAssets(Wallet userWallet)
{
    Console.Clear();
    userWallet.PrintWallet();
    Console.WriteLine("");
    foreach (var item in userWallet.AmountOfAssets)
    {

        GlobalWallets.GetFungibleAssetByAdress(item.Key).PrintAsset();
        Console.WriteLine($"Vrijednost (ukupna): {Decimal.Round(GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value)}$ ");
        Console.WriteLine($"Količina: {item.Value}");
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
Wallet PrintAndChooseWallet()
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
    if (GlobalWallets.Wallets.Find(x => x.Adress.ToString() == userWalletAdress) == null)
    {
        Console.WriteLine("Nije upisani pravi wallet");
        Console.ReadLine();
        return null;
    }
    var userWallet = GlobalWallets.Wallets.Find(x => x.Adress.ToString() == userWalletAdress);
    return userWallet;
}
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
void RecallTransaction(Wallet userWallet)
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
    var confirmation = DialogOfConfirmation();
    if (!confirmation)
        return;
    Wallet sendWallet = null;
    Wallet receiveWallet = null;

    if (userWallet.Adress != transactionToRecall.Sender)
    {
        Console.WriteLine("Morate opozvati reakciju s walleta koji ju je napravio");
        Console.ReadLine();
        return;
    }

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
void PrintAndChooseTransaction(Wallet userWallet)
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
                var confirmationBitcoin = DialogOfConfirmation();
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
                var confirmationEtherum = DialogOfConfirmation();
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
                var confirmationSolana = DialogOfConfirmation();
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

    var userWallet = PrintAndChooseWallet();
    if (userWallet == null)
        return;
    /*
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
    */
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
                PrintWalletAssets(userWallet);
                break;

            case "2":
                UserTransaction(userWallet);
                break;
            case "3":
                PrintAndChooseTransaction(userWallet);
                break;
            case "0":
                loop=0;
                break;
            default: Console.WriteLine("Nije upisan valjani input");Console.ReadLine();break;
        }

        }
    }