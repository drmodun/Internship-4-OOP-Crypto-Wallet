using CrytpWallet.Assets;
using CrytpWallet.Classes.Transactions;
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
        public static List<Transaction> AllTransactions { get; private set; }
        static GlobalWallets()
        {
            
            //AllAssets = new List<Asset>();
            AllTransactions= new List<Transaction>();
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
                new NonFungibleAsset(GetAdressOfFungibleToken("LTC"), new Decimal(2))
                {
                    Name="Black Hat"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("LTC"), new Decimal(100))
                {
                    Name="White Hat"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("DOGE"), new Decimal(1000))
                {
                    Name="Coin"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("BNB"), new Decimal(1))
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
                new NonFungibleAsset(GetAdressOfFungibleToken("DOGE"), new Decimal(10))
                {
                    Name="CyberSword"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("NMC"), new Decimal(0.00043))
                {
                    Name="Croatia Flag #1"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("NMC"), new Decimal(230))
                {
                    Name="Croatia Flag #231"
                },
                new NonFungibleAsset(GetAdressOfFungibleToken("NMC"), new Decimal(0.0000001))
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
            AllBitcoinWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("BTC"), new Decimal(20));
            AllBitcoinWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("GRC"), new Decimal(20));
            AllBitcoinWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("AUR"), new Decimal(20));

            AllBitcoinWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("GRC"), new Decimal(2));
            AllBitcoinWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("BTC"), new Decimal(20));
            AllBitcoinWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("XRP"), new Decimal(10));

            AllBitcoinWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("BTC"), new Decimal(20));
            AllBitcoinWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("XRP"), new Decimal(9));
            AllBitcoinWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("AUR"), new Decimal(123));

            AllBitcoinWallets[0].CalculateValue();
            AllBitcoinWallets[1].CalculateValue();
            AllBitcoinWallets[2].CalculateValue();

            AllEtherumWallets = new List<EtherumWallet>()
            {
                new EtherumWallet(),
                new EtherumWallet(),
                new EtherumWallet()
            };

            AllEtherumWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("ETH"), new Decimal(10));
            AllEtherumWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("DOGE"), new Decimal(500));
            AllEtherumWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("BNB"), new Decimal(9));
            AllEtherumWallets[0].HeldNFT.Add(GetAdressOfNonFungibleToken("Red Ape"));
            AllEtherumWallets[0].HeldNFT.Add(GetAdressOfNonFungibleToken("Blue Ape"));
            AllEtherumWallets[0].HeldNFT.Add(GetAdressOfNonFungibleToken("Red Ape"));
            AllEtherumWallets[0].HeldNFT.Add(GetAdressOfNonFungibleToken("Diamond Helmet"));

            AllEtherumWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("ETH"), new Decimal(100));
            AllEtherumWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("GRC"), new Decimal(1000));
            AllEtherumWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("BNB"), new Decimal(80));
            AllEtherumWallets[1].HeldNFT.Add(GetAdressOfNonFungibleToken("Leather leggings"));

            AllEtherumWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("ETH"), new Decimal(1));
            AllEtherumWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("XRP"), new Decimal(10));
            AllEtherumWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("DOGE"), new Decimal(100000));
            AllEtherumWallets[2].HeldNFT.Add(GetAdressOfNonFungibleToken("Copper Axe"));
            AllEtherumWallets[2].HeldNFT.Add(GetAdressOfNonFungibleToken("Coin"));

            AllEtherumWallets[0].CalculateValue();
            AllEtherumWallets[1].CalculateValue();
            AllEtherumWallets[2].CalculateValue();

            AllSolanaWallets = new List<SolanaWallet>()
            {
                new SolanaWallet(),
                new SolanaWallet(),
                new SolanaWallet()
            };

            AllSolanaWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("SOL"), new Decimal(200));
            AllSolanaWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("LTC"), new Decimal(200));
            AllSolanaWallets[0].AmountOfAssets.Add(GetAdressOfFungibleToken("NMC"), new Decimal(200));
            AllSolanaWallets[0].HeldNFT.Add(GetAdressOfNonFungibleToken("CyberSword"));
            AllSolanaWallets[0].HeldNFT.Add(GetAdressOfNonFungibleToken("Cryptopunk"));

            AllSolanaWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("SOL"), new Decimal(200000));
            AllSolanaWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("LTC"), new Decimal(20));
            AllSolanaWallets[1].AmountOfAssets.Add(GetAdressOfFungibleToken("DOGE"), new Decimal(2));
            AllSolanaWallets[1].HeldNFT.Add(GetAdressOfNonFungibleToken("White Hat"));
            AllSolanaWallets[1].HeldNFT.Add(GetAdressOfNonFungibleToken("Black Hat"));


            AllSolanaWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("SOL"), new Decimal(2));
            AllSolanaWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("DOGE"), new Decimal(2000000));
            AllSolanaWallets[2].AmountOfAssets.Add(GetAdressOfFungibleToken("NMC"), new Decimal(2));
            AllSolanaWallets[2].HeldNFT.Add(GetAdressOfNonFungibleToken("Croatia Flag #1"));
            AllSolanaWallets[2].HeldNFT.Add(GetAdressOfNonFungibleToken("Croatia Flag #231"));
            AllSolanaWallets[2].HeldNFT.Add(GetAdressOfNonFungibleToken("Croatia Flag #2335"));

            AllSolanaWallets[0].CalculateValue();
            AllSolanaWallets[1].CalculateValue();
            AllSolanaWallets[2].CalculateValue();

            Wallets = new List<Wallet>()
            {
                AllBitcoinWallets[0],
                AllBitcoinWallets[1],
                AllBitcoinWallets[2],
                AllEtherumWallets[0],
                AllEtherumWallets[1],
                AllEtherumWallets[2],
                AllSolanaWallets[0],
                AllSolanaWallets[1],
                AllSolanaWallets[2],
            };

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
            //Console.WriteLine(label);
            foreach (var item in AllNonFungibleAssets)
            {
                if (item.Name == label)
                {
                    return item.Adress;
                }
            }
            //Console.WriteLine(label);
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
        public static void AdressPrint(Guid adress, Guid receivingAdress)
        {
            Console.WriteLine("Mogući asseti");
            Console.WriteLine("Fungible asseti:");
            var wallet = GetWalletByAdress(adress.ToString());
            var receivingWallet = GetWalletByAdress(receivingAdress.ToString());
            var foundFungible = false;
            var foundNonFungible = false;
            var allowed = false;
            foreach (var item in wallet.AmountOfAssets)
            {
                var asset = GetFungibleAssetByAdress(item.Key);
                switch (receivingWallet.Type)
                {
                    case 1:
                        if (BitcoinWallet.AllowedAssets.Contains(asset.Adress))
                            allowed= true;
                        break;
                    case 2:
                        if (EtherumWallet.AllowedAssetsFungible.Contains(asset.Adress))
                            allowed= true;
                        break;
                    case 3:
                        if (SolanaWallet.AllowedAssetsFungible.Contains(asset.Adress))
                            allowed= true;
                            break;
                }
                if (allowed)
                {
                    Console.WriteLine($"Fungible asset {asset.Name} ({asset.Label}), kolicina {item.Value} i vrijendost {asset.ValueInDollar}$: {item.Key}");
                    foundFungible= true;
                }
                allowed= false;
            }
            if (!foundFungible)
                Console.WriteLine("Nije pronađen ni jedan dopušteni fungible asset");
            Console.WriteLine("");
            if (wallet as DoubleWallet != null)
            {
                Console.WriteLine("NonFungible asseti:");
                var walletNft= wallet as DoubleWallet;
                foreach(var item in walletNft.HeldNFT)
                {
                    var asset = GetNonFungibleAssetByAdress(item);
                    switch (receivingWallet.Type)
                    {
                        case 2:
                            if (EtherumWallet.AllowedNonFungible.Contains(asset.Adress))
                                allowed = true;
                            break;
                        case 3:
                            if (SolanaWallet.AllowedNonFungible.Contains(asset.Adress))
                                allowed = true;
                            break;
                    }
                    if (allowed)
                    {
                        foundNonFungible= true;
                        Console.WriteLine($"NonFungible asset {asset.Name}, vrijednosti {asset.ValueInFungible} {GetFungibleAssetByAdress(asset.ItsFungible).Label} ili {asset.ValueInDollar}$: {item}");
                    }
                    allowed= false;
                    }
            if (!foundNonFungible)
                    Console.WriteLine("Nije dopušten ni jedan dopušteni non fungible asset");
            }
        }
        public static void ReCalculateAllWallets()
        {
            foreach(var item in AllNonFungibleAssets)
            {
                item.UpdateValue();
            }
            foreach (var item in AllBitcoinWallets)
            { item.CalculateValue(); }
            foreach (var item in AllEtherumWallets)
            { item.CalculateValue(); }
            foreach (var item in AllSolanaWallets)
            { item.CalculateValue();
            }
        }
        public static Transaction GetTransactionById(Guid id)
        {
            foreach(var item in AllTransactions)
            {
                if (item.Id==id)
                { return item; }
            }
            return null;
        }
    }
}
