using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{

    public sealed  class EtherumWallet : DoubleWallet
    {
        public EtherumWallet() : base() {
            type = 2;
            
            HeldNFT = new List<Guid>();
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
        //public List<Guid> HeldNFT { get; init; }
        
        public override void PrintWallet()
        {
            Console.WriteLine("Etherum Wallet");
            base.PrintWallet();

        }
        
        //Change these funcitons later, make them a bit better
    }
}
