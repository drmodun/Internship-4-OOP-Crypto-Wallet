using CrytpWallet.Classes.Global;


namespace CrytpWallet.Classes.Wallets
{
    public sealed class BitcoinWallet:Wallet
    {
        public BitcoinWallet() : base() {
            Type = 1;
        }
        static BitcoinWallet()
        {
            AllowedAssets = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("BTC"),
                GlobalWallets.GetAdressOfFungibleToken("GRC"),
                GlobalWallets.GetAdressOfFungibleToken("AUR"),
                GlobalWallets.GetAdressOfFungibleToken("XRP")
            };
        }
        public static List<Guid> AllowedAssets { get; private set; }
        
        public override void PrintWallet()
        {
            Console.WriteLine("Bitcoin Wallet");
            base.PrintWallet();
            

        }
    }
}
