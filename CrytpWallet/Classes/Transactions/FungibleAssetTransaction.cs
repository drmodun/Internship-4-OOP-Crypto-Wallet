using CrytpWallet.Classes.Global;

namespace CrytpWallet.Classes.Transactions
{
    public sealed  class FungibleTransaction : Transaction
    {
        public FungibleTransaction() : base() { }
        public Guid AdressOfToken { get; init; }
        public decimal StartBalanceSender { get; init; }
        public decimal StartBalanceReceiver { get; init; }
        public decimal EndBalanceSender { get; init; }
        public decimal EndBalanceReceiver { get; init; }
        public override void PrintTransaction()
        {
            base.PrintTransaction();
            Console.WriteLine($"Količina: {EndBalanceReceiver-StartBalanceReceiver}\n" +
                $"Adresa fungible tokena: {AdressOfToken}\n" +
                $"Ime fungible tokena {GlobalWallets.GetFungibleAssetByAdress(AdressOfToken).Name}" +
                $"Opozvana: {Recalled}");
        }

    }
}
