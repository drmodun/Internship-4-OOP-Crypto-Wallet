using CrytpWallet.Classes.Global;


namespace CrytpWallet.Classes.Wallets
{

    public sealed  class EtherumWallet : DoubleWallet
    {
        public EtherumWallet() : base() {
            Type = 2;
            
        }
        static EtherumWallet()
        {
            AllowedAssetsFungible = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("ETH"),
                GlobalWallets.GetAdressOfFungibleToken("GRC"),
                GlobalWallets.GetAdressOfFungibleToken("DOGE"),
                GlobalWallets.GetAdressOfFungibleToken("XRP"),
                GlobalWallets.GetAdressOfFungibleToken("BNB")
            };
            AllowedNonFungible = new List<Guid>()
            { };
            //Adding all nfts which this wallet can support based on supported cryptocurrencies
            foreach (var item in GlobalWallets.AllNonFungibleAssets)
            {
                foreach (Guid id in AllowedAssetsFungible)
                {
                    if (item.ItsFungible == id)
                        AllowedNonFungible.Add(item.Adress);
                }
            }
        }
        public static List<Guid> AllowedAssetsFungible { get; private set; }
        public static List<Guid> AllowedNonFungible { get; private set; }
        
        public override void PrintWallet()
        {
            Console.WriteLine("Etherum Wallet");
            base.PrintWallet();

        }
        
        //Change these funcitons later, make them a bit better
    }
}
