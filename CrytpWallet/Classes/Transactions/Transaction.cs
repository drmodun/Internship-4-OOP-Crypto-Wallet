using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public abstract class Transaction
    {
        public abstract Guid Id { get; }
        public abstract DateTime TimeOfTransaction { get; }
        public abstract bool Recalled { get; }
    }
}
