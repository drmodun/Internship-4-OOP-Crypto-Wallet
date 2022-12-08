using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public sealed  class FungibleTransaction : Transaction
    {
        public FungibleTransaction() : base() { }
        public Guid AdressOfToken { get; init; }
        public decimal StartBalanceSender { get; init; }
        public decimal StartBalanceReceiver { get; init; }
        public decimal EndBalanceSender { get; init; }
        public decimal EndBalanceReceiver { get; init; }
        public override void PrintTransaction()
        {
            base.PrintTransaction();
            Console.WriteLine($"Količina: {EndBalanceReceiver-StartBalanceReceiver}\n" +
                $"Ime fungible tokena: {AdressOfToken}\n" +
                $"Opozvana: {Recalled}");
        }

    }
}
