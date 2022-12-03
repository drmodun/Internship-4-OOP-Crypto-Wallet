using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{

    public abstract class EtherumWallet : Wallet
    {
        public EtherumWallet() : base() { }
        public  static List<Asset> AllowedAssets { get; }
        public List<NonFungableAsset> HeldNFT { get; } 
    }
}
}
