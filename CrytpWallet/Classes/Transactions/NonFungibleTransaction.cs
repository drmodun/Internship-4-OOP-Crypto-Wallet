using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public  class NonFungibleTransaction:Transaction
    {
        public NonFungibleTransaction() : base() { }
        
        public  Guid AdressOfNFT { get; init; }
        public override void PrintTransaction()
        {
            base.PrintTransaction();
            Console.WriteLine(
                $"Ime fungible tokena: {AdressOfNFT}\n" +
                $"Opozvana: {Recalled}");
        }

    }
}
