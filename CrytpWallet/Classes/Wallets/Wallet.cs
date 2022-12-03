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
        public  Guid Adress { get; }
        public  Dictionary<Guid, int> AmountOfAssets{ get; }
        public  List<Guid> Transactions { get; }
        public double totalValue { get; protected set; }
        public double oldValue { get; protected set; }
        public virtual void PrintWallet()
        {
            Console.WriteLine($"Adresa walleta: {Adress}" +
                $"\nTotalna vrijednost: {totalValue}" +
                $"\nTotalna promjena vrijednosti {(totalValue-oldValue)/100}%");
        }
    }
}
