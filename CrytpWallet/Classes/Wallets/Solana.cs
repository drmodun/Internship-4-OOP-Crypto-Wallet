using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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
        //public List<Guid> HeldNFT { get; init; }
        
        public override void PrintWallet()
        {
            Console.WriteLine("Solana Wallet");
            base.PrintWallet();

        }
    }
}
