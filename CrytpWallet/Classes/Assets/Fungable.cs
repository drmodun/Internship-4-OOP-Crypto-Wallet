using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public sealed class FungableAsset : Asset  
    {
        public FungableAsset() : base() { }
        public string Label { get; }
        public double ValueInDollar { get; }
        public override void UpdateValue()
        {
            Random random = new Random();
            double PriceChange = random.Next(-5, 5)/(double)2;
            OldValueInDollar = ValueInDolar;
            ValueInDolar +=ValueInDolar*PriceChange;
        }
        public override void PrintAsset()
        {
            base.PrintAsset();
            Console.WriteLine($"Oznaka: {Label}\n" +
                $"Mijenjanje vrijednosti: {(ValueInDolar-OldValueInDollar)/100}%");
        }
    }
}
