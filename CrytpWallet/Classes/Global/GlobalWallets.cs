using CrytpWallet.Assets;
using CrytpWallet.Classes.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
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
            AllFungibleAssets = new List<FungibleAsset>();
            AllNonFungibleAssets = new List<NonFungibleAsset>();
        }
    }
}
