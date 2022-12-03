using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public sealed  class FungableTransaction : Transaction
    {
        public FungableTransaction() : base() { }
        public Guid AdressOfToken { get; }
        public double StartBalanceSender { get; }
        public double StartBalanceReceiver { get; }
        public double EndBalanceSender { get; }
        public double EndBalanceReceiver { get; }
        public override void PrintTrasnsaction()
        {
            base.PrintTrasnsaction();
            Console.WriteLine($"Količina: {EndBalanceReceiver-StartBalanceReceiver}\n" +
                $"Ime fungible tokena: {AdressOfToken}\n" +
                $"Opozvana: {Recalled}");
        }

    }
}
