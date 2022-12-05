using CrytpWallet.Classes.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public sealed class NonFungibleAsset:Asset
    {
            public NonFungibleAsset(double valueInDollar, Guid itsFungible, double valueInFungible) : base(valueInDollar) 
        {

            ValueInFungible = valueInFungible;
            ItsFungible = itsFungible;
            ValueInDollar = valueInDollar;
        }
            public Guid ItsFungible { get; init; }
            public double ValueInFungible { get; private set;   }
        public override void UpdateValue()
        {
            
            OldValueInDollar = ValueInDollar;
            ValueInDollar = GlobalWallets.AllFungibleAssets.Find(x => x.Adress == ItsFungible).ValueInDollar * ValueInFungible;
            

            //Mijenjanje vrijenosti radim izvan objekta pošto nemam access cijeloj listi pa ne mogu preko adrese dobiti želejeni objekt
        }
        public override void PrintAsset()
        {
            var token = GlobalWallets.AllFungibleAssets.Find(x => x.Adress == ItsFungible);
            base.PrintAsset();
            Console.WriteLine($"Vrijednost u fungible assetu: {ValueInFungible} {token.Label}\n" +
                $"Mijenjanje vrijednosti: {(ValueInDollar - OldValueInDollar) / 100}%");
        }
    }
    }
