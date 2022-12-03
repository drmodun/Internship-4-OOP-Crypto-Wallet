using CrytpWallet.Classes.Transactions;
using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class Wallet
    {
        public abstract Guid Adress { get; }
        public abstract Dictionary<FungableAsset, int> AmountOfAssets{ get; }
        public abstract Dictionary<Guid, Transaction> Transaction { get; }

    }
}
