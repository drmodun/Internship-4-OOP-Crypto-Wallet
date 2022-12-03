using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public abstract class Transaction
    {
        public  Guid Id { get; }
        public  Guid Sender { get; }
        public  Guid Receiver { get; }
        public  DateTime TimeOfTransaction { get; }
        public  bool Recalled { get; }
        public virtual void PrintTrasnsaction()
        {
            Console.WriteLine($"Id transakcije: {Id}\n" +
                $"Pošiljatelj: {Sender}\n" +
                $"Primatelj: {Receiver}" +
                $"Vrijeme transakcije: {TimeOfTransaction}");
        }
    }
}
