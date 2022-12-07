using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public abstract class Transaction
    {
        public  Guid Id { get; init; }
        public  Guid Sender { get; init; }
        public  Guid Receiver { get; init; }
        public  DateTime TimeOfTransaction { get; init; }
        public  bool Recalled { get; set;}
        public Transaction()
        {
            Id = Guid.NewGuid();
            Recalled = false;
        }
        public virtual void PrintTrasnsaction()
        {
            Console.WriteLine($"Id transakcije: {Id}\n" +
                $"Pošiljatelj: {Sender}\n" +
                $"Primatelj: {Receiver}" +
                $"Vrijeme transakcije: {TimeOfTransaction}");
        }
    }
}
