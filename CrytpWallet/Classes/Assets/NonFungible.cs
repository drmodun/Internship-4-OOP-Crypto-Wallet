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
            public NonFungibleAsset(Guid itsFungible, double valueInFungible) : base() 
        {

            ValueInFungible = valueInFungible;
            ItsFungible = itsFungible;
            if (GlobalWallets.GetFungibleAssetByAdress(itsFungible) == null)
            {
                Console.WriteLine("Wrong");
                return;
            }
            ValueInDollar = GlobalWallets.GetFungibleAssetByAdress(itsFungible).ValueInDollar*valueInFungible;
            OldValueInDollar = 0;
        }
            public Guid ItsFungible { get; init; }
            public double ValueInFungible { get; private set;   }
        public override void UpdateValue()
        {
            
            OldValueInDollar = ValueInDollar;
            ValueInDollar = GlobalWallets.AllFungibleAssets.Find(x => x.Adress == ItsFungible).ValueInDollar * ValueInFungible;
            

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
