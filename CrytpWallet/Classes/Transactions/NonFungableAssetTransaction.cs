using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public abstract class NonFunGableTransaction : Transaction
    {
        public NonFunGableTransaction() : base() { }
        public abstract Guid Sender { get; }
        public abstract Guid Receiver { get; }
        public Guid AdressOfNFT { get; }
        public abstract double StartBalanceSender { get; }
        public double StartBalanceReceiver { get; }
        public double EndBalanceSender { get; }
        public double EndBalanceReceiver { get; }

    }
}
