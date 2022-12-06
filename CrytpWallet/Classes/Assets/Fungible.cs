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
        public FungibleAsset(decimal valueInDollar) : base() { 
            ValueInDollar = valueInDollar; 
            OldValueInDollar = valueInDollar; }
        public string Label { get; init; }
        public override void UpdateValue()
        {
            Random random = new Random();
            decimal PriceChange = random.Next(-5, 5)/(decimal)200;
            ValueInDollar +=ValueInDollar*PriceChange;
        }
        public override void PrintAsset()
        {
            base.PrintAsset();
            Console.WriteLine($"Oznaka: {Label}\n" +
                $"Mijenjanje vrijednosti: {((ValueInDollar - OldValueInDollar) / OldValueInDollar )*100}%");
            OldValueInDollar = ValueInDollar;

        }
    }
}
