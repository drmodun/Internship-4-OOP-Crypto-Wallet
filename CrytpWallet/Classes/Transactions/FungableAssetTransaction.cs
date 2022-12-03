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
        public Guid AdressOfToken { get; init; }
        public double StartBalanceSender { get; init; }
        public double StartBalanceReceiver { get; init; }
        public double EndBalanceSender { get; init; }
        public double EndBalanceReceiver { get; init; }
        public override void PrintTrasnsaction()
        {
            base.PrintTrasnsaction();
            Console.WriteLine($"Količina: {EndBalanceReceiver-StartBalanceReceiver}\n" +
                $"Ime fungible tokena: {AdressOfToken}\n" +
                $"Opozvana: {Recalled}");
        }

    }
}
