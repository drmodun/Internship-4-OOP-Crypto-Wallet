using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class BitcoinWallet:Wallet
    {
        public BitcoinWallet() : base() { }
        public static List<FungableAsset> AllowedAssets { get; set; }
    }
}
