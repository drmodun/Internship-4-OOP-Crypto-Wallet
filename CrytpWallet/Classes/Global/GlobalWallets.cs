using CrytpWallet.Assets;
using CrytpWallet.Classes.Wallets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Global
{
    public static class GlobalWallets
    {
        public static List<Wallet> Wallets { get; private set; }
        public static List<BitcoinWallet> AllBitcoinWallets { get; private set; }
        public static List<EtherumWallet> AllEtherumWallets { get; private set; }
        public static List<SolanaWallet> AllSolanaWallets { get; private set;  }

        public static List<Asset> AllAssets { get; private set; }
        public static List<FungibleAsset> AllFungibleAssets { get; private set; }
        public static List<NonFungibleAsset> AllNonFungibleAssets { get; private set; }
        
        static GlobalWallets()
        {
            Wallets = new List<Wallet>();
            AllBitcoinWallets = new List<BitcoinWallet>();
            AllEtherumWallets = new List<EtherumWallet>();
            AllSolanaWallets = new List<SolanaWallet>();
            AllAssets = new List<Asset>();

            AllFungibleAssets = new List<FungibleAsset>()
            {
                new FungibleAsset(16923.60)
                {
                    Name="Bitcoin",
                    Label="BTC"
                },
                new FungibleAsset(79.59)
                {
                    Name="Litcoin",
                    Label="LTC"
                },
                new FungibleAsset(1)
                {
                    Name="Namecoin",
                    Label="NMC"
                },
                new FungibleAsset(1255.55)
                {
                    Name="Etherum",
                    Label="ETH"
                },
                new FungibleAsset(14.16)
                {
                    Name="Solana",
                    Label="SOL"
                },
                new FungibleAsset(0.1)
                {
                    Name="Dogecoin",
                    Label="DOGE"
                },
                new FungibleAsset(0.00457)
                {
                    Name="Gridcoin",
                    Label="GRC"
                },
                new FungibleAsset(0.39)
                {
                    Name="XRP",
                    Label="XRP"
                },
                new FungibleAsset(287.12)
                {
                    Name="Binance",
                    Label="BNB"
                },
                new FungibleAsset(0.04411)
                {
                    Name="Auroracoin",
                    Label="AUR"
                }
            };

            AllNonFungibleAssets = new List<NonFungibleAsset>()
            {
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), 1000)
                {
                    Name="Cryptopunk"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), 10000)
                {
                    Name="Red Ape"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), 1.450)
                {
                    Name="Blue Ape"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BTC"), 2)
                {
                    Name="Black Hat"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BTC"), 100)
                {
                    Name="White Hat"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("AUR"), 1000)
                {
                    Name="Coin"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("AUR"), 1)
                {
                    Name="HackSilver"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("GRC"), 1950.346)
                {
                    Name="Leviathan Axe"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("GRC"), 0.3452235)
                {
                    Name="Copper Axe"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("XRP"), 12021)
                {
                    Name="Diamond Helmet"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("XRP"), 21)
                {
                    Name="Leather leggings"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BTC"), 10)
                {
                    Name="CyberSword"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), 0.00043)
                {
                    Name="Croatia Flag #1"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), 230)
                {
                    Name="Croatia Flag #231"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), 0.0000001)
                {
                    Name="Croatia Flag #2335"
                }
                //Add 5 more

            };

        }
        private static Guid GetAdressOfFungibleToken(string label)
        {
            foreach(var item in AllFungibleAssets)
            {
                if (item.Name == label)
                    return item.Adress;
            }
            return Guid.Empty;
        }
        public static FungibleAsset GetFungibleAssetByAdress(Guid adress)
        {
            foreach (var item in AllFungibleAssets)
            {
                if (item.Adress==adress)
                    return item;
            }
            return null;
        }
    }
}
