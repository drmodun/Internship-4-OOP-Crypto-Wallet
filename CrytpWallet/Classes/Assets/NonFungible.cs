using CrytpWallet.Classes.Global;

namespace CrytpWallet.Assets
{
    public sealed class NonFungibleAsset:Asset
    {
            public NonFungibleAsset(Guid itsFungible, decimal valueInFungible) : base() 
        {

            ValueInFungible = valueInFungible;
            ItsFungible = itsFungible;
            if (GlobalWallets.GetFungibleAssetByAdress(itsFungible) == null)
            {
                return;
            }
            ValueInDollar = GlobalWallets.GetFungibleAssetByAdress(itsFungible).ValueInDollar*valueInFungible;
            OldValueInDollar =ValueInDollar;
        }
            public Guid ItsFungible { get; init; }
            public decimal ValueInFungible { get; private set;   }
        public override void UpdateValue()
        {
            ValueInDollar = GlobalWallets.AllFungibleAssets.Find(x => x.Adress == ItsFungible).ValueInDollar * ValueInFungible;
            

        }
        public override void PrintAsset()
        {
            var token = GlobalWallets.AllFungibleAssets.Find(x => x.Adress == ItsFungible);
            base.PrintAsset();
            Console.WriteLine($"Vrijednost u fungible assetu: {ValueInFungible} {token.Label}\n" +
                $"Mijenjanje vrijednosti: {Decimal.Round(((ValueInDollar - OldValueInDollar) /  ValueInDollar)*100, 4)}%");
            OldValueInDollar = ValueInDollar;
        }
    }
    }
