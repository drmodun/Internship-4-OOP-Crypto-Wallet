using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Transactions
{
    public  class NonFungableTransaction:Transaction
    {
        public NonFungableTransaction() : base() { }
        
        public  Guid AdressOfNFT { get; init; }
        public override void PrintTrasnsaction()
        {
            base.PrintTrasnsaction();
            Console.WriteLine(
                $"Ime fungible tokena: {AdressOfNFT}\n" +
                $"Opozvana: {Recalled}");
        }

    }
}
