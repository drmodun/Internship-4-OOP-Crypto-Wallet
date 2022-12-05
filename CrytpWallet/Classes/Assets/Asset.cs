using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public abstract class Asset
    {
        public Guid Adress { get; init;  }
        public string Name { get; init; }
        public double ValueInDollar { get; protected set; }
        public double OldValueInDollar { get; protected set; }
        public virtual void PrintAsset()
        {
            Console.WriteLine($"Adresa asseta: {Adress}\n" +
                $"Ime asseta: {Name}\n" +
                $"Vrijednost: {ValueInDollar}\n");
        }
        public Asset(double valueInDollar)
        {
            Adress=Guid.NewGuid();
            ValueInDollar = valueInDollar;
            OldValueInDollar = 0;
        }
        public abstract void UpdateValue();
    }
}
