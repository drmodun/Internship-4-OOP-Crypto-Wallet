using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public sealed class FungibleAsset : Asset  
    {
        public FungibleAsset(double valueInDollar) : base(valueInDollar) { }
        public string Label { get; init; }
        public override void UpdateValue()
        {
            Random random = new Random();
            double PriceChange = random.Next(-5, 5)/(double)2;
            OldValueInDollar = ValueInDollar;
            ValueInDollar +=ValueInDollar*PriceChange;
        }
        public override void PrintAsset()
        {
            base.PrintAsset();
            Console.WriteLine($"Oznaka: {Label}\n" +
                $"Mijenjanje vrijednosti: {(ValueInDollar-OldValueInDollar)/100}%");
        }
    }
}
