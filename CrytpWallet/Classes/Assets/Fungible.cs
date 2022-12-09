
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
            var old = ValueInDollar;
            Random random = new Random();
            decimal PriceChange = random.Next(-250, 250)/(decimal)10000;
            ValueInDollar +=ValueInDollar*PriceChange;
            Console.WriteLine($"Vrijednost od {Label} changed from {Decimal.Round(old, 4)}$ to {Decimal.Round(ValueInDollar, 4)}$");
        }
        public override void PrintAsset()
        {
            base.PrintAsset();
            Console.WriteLine($"Oznaka: {Label}\n" +
                $"Mijenjanje vrijednosti (jednog): {Decimal.Round(((ValueInDollar - OldValueInDollar) / OldValueInDollar )*100,2)}%");
            OldValueInDollar = ValueInDollar;

        }
    }
}
