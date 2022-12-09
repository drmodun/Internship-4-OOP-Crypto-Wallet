namespace CrytpWallet.Assets
{
    public abstract class Asset 
    {
        public Guid Adress { get; init;  }
        public string Name { get; init; }
        public decimal ValueInDollar { get; protected set; }
        public decimal OldValueInDollar { get; protected set; }
        public virtual void PrintAsset()
        {
            Console.WriteLine($"Adresa asseta: {Adress}\n" +
                $"Ime asseta: {Name}\n" +
                $"Vrijednost: {Decimal.Round(ValueInDollar, 4)} $");
        }
        public Asset()
        {
            Adress=Guid.NewGuid();
        }
        public abstract void UpdateValue();
    }
}
