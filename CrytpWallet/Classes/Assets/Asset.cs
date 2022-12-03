using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public abstract class Asset
    {
        public Guid Adress { get; }
        public string Name { get; }
        public double ValueInDolar { get; protected set; }
        public double OldValueInDollar { get; protected set; }
        public virtual void PrintAsset()
        {
            Console.WriteLine($"Adresa asseta: {Adress}\n" +
                $"Ime asseta: {Name}\n" +
                $"Vrijednost: {ValueInDolar}\n");
        }
        public abstract void UpdateValue();
    }
}
