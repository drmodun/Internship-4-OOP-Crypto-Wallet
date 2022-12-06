using CrytpWallet.Assets;
using CrytpWallet.Classes.Wallets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
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

        //public static List<Asset> AllAssets { get; private set; }
        public static List<FungibleAsset> AllFungibleAssets { get; private set; }
        public static List<NonFungibleAsset> AllNonFungibleAssets { get; private set; }
        
        static GlobalWallets()
        {
            
            //AllAssets = new List<Asset>();

            AllFungibleAssets = new List<FungibleAsset>()
            {
                new FungibleAsset(new Decimal(16923.60))
                {
                    Name="Bitcoin",
                    Label="BTC"
                },
                new FungibleAsset(new Decimal(79.59))
                {
                    Name="Litcoin",
                    Label="LTC"
                },
                new FungibleAsset(1)
                {
                    Name="Namecoin",
                    Label="NMC"
                },
                new FungibleAsset(new Decimal(1255.55))
                {
                    Name="Etherum",
                    Label="ETH"
                },
                new FungibleAsset(new Decimal(14.16))
                {
                    Name="Solana",
                    Label="SOL"
                },
                new FungibleAsset(new Decimal(0.1))
                {
                    Name="Dogecoin",
                    Label="DOGE"
                },
                new FungibleAsset(new Decimal(0.00457))
                {
                    Name="Gridcoin",
                    Label="GRC"
                },
                new FungibleAsset(new Decimal(0.39))
                {
                    Name="XRP",
                    Label="XRP"
                },
                new FungibleAsset(new Decimal(287.12))
                {
                    Name="Binance",
                    Label="BNB"
                },
                new FungibleAsset(new Decimal(0.04411))
                {
                    Name="Auroracoin",
                    Label="AUR"
                }
            };

            AllNonFungibleAssets = new List<NonFungibleAsset>()
            {
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), new Decimal(1000))
                {
                    Name="Cryptopunk"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), new Decimal(10000))
                {
                    Name="Red Ape"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), new Decimal(1.450))
                {
                    Name="Blue Ape"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BTC"), new Decimal(2))
                {
                    Name="Black Hat"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BTC"), new Decimal(100))
                {
                    Name="White Hat"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("AUR"), new Decimal(1000))
                {
                    Name="Coin"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("AUR"), new Decimal(1))
                {
                    Name="HackSilver"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("GRC"), new Decimal(1950.346))
                {
                    Name="Leviathan Axe"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("GRC"), new Decimal(0.3452235))
                {
                    Name="Copper Axe"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("XRP"), new Decimal(12021))
                {
                    Name="Diamond Helmet"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("XRP"), new Decimal(21))
                {
                    Name="Leather leggings"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BTC"), new Decimal(10))
                {
                    Name="CyberSword"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), new Decimal(0.00043))
                {
                    Name="Croatia Flag #1"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), new Decimal(230))
                {
                    Name="Croatia Flag #231"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("ETH"), new Decimal(0.0000001))
                {
                    Name="Croatia Flag #2335"
                }
                //Add 5 more

            };
            AllBitcoinWallets = new List<BitcoinWallet>()
            {
                new BitcoinWallet(),
                new BitcoinWallet(),
                new BitcoinWallet(),
            };
            AllBitcoinWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("BTC"), 20);
            AllBitcoinWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("GRC"), 20);
            AllBitcoinWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("AUR"), 20);

            AllBitcoinWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("GRC"), 2);
            AllBitcoinWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("BTC"), 20);
            AllBitcoinWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("XRP"), 10);

            AllBitcoinWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("BTC"), 20);
            AllBitcoinWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("XRP"), 9);
            AllBitcoinWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("AUR"), 123);

            AllBitcoinWallets[0].CalculateValue();
            AllBitcoinWallets[1].CalculateValue();
            AllBitcoinWallets[2].CalculateValue();

            AllEtherumWallets = new List<EtherumWallet>();
            AllSolanaWallets = new List<SolanaWallet>();
            Wallets = new List<Wallet>()
            {
                AllBitcoinWallets[0],
                AllBitcoinWallets[1],
                AllBitcoinWallets[2],
            };
            //add 6 more wallets

        }
        public static Guid GetAdressOfFungibleToken(string label)
        {
            foreach(var item in AllFungibleAssets)
            {
                if (item.Label == label)
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
        public static Guid GetAdressOfNonFungibleToken(string label)
        {
            foreach (var item in AllNonFungibleAssets)
            {
                if (item.Name == label)
                    return item.Adress;
            }
            return Guid.Empty;
        }
        public static NonFungibleAsset GetNonFungibleAssetByAdress(Guid adress)
        {
            foreach (var item in AllNonFungibleAssets)
            {
                if (item.Adress == adress)
                    return item;
            }
            return null;
        }
        public static Wallet GetWalletByAdress(string adress)
        {
            foreach(var item in Wallets)
            {
                if (item.Adress.ToString() == adress)
                    return item;
            }
            return null;
        }

        //For debugging sake, will most likely delete later
        public static void PrintAll()
        {
            foreach (var item in AllFungibleAssets)
            {
                item.PrintAsset();
                Console.WriteLine("");
            }
            foreach (var item in AllNonFungibleAssets)
            {
                item.PrintAsset();
                Console.WriteLine("");
            }
        }
        public static void AdressPrint()
        {
            foreach (var item in AllFungibleAssets)
            {
                Console.WriteLine($"{item.Name}: {item.Adress}");
            }
        }
    }
}
