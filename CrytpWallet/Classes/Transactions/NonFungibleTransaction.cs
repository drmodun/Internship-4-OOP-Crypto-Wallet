using CrytpWallet.Classes.Global;

namespace CrytpWallet.Classes.Transactions
{
    public  class NonFungibleTransaction:Transaction
    {
        public NonFungibleTransaction() : base() { }
        
        public  Guid AdressOfNFT { get; init; }
        public override void PrintTransaction()
        {
            base.PrintTransaction();
            Console.WriteLine(
                $"Adresa nonfungible tokena: {AdressOfNFT}\n" +
                $"Ime nonfungible tokena {GlobalWallets.GetNonFungibleAssetByAdress(AdressOfNFT).Name}" +
                $"Opozvana: {Recalled}");
        }

    }
}
