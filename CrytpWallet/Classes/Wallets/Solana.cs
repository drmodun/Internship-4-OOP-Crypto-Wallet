using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;

namespace CrytpWallet.Classes.Wallets
{

    public sealed class SolanaWallet : DoubleWallet
    {
        public SolanaWallet() : base() {
            Type = 3;
            
            HeldNFT = new List<Guid>();
        }
        static SolanaWallet()
        {
            AllowedAssetsFungible = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("SOL"),
                GlobalWallets.GetAdressOfFungibleToken("DOGE"),
                GlobalWallets.GetAdressOfFungibleToken("LTC"),
                GlobalWallets.GetAdressOfFungibleToken("NMC")
            };

            AllowedNonFungible = new List<Guid>();
            foreach (var item in GlobalWallets.AllNonFungibleAssets)
            {
                foreach (var id in AllowedAssetsFungible)
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
            Console.WriteLine("Solana Wallet");
            base.PrintWallet();

        }
    }
}
