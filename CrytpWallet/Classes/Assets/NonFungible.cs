using CrytpWallet.Classes.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Wrong");
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
                $"Mijenjanje vrijednosti: {((ValueInDollar - OldValueInDollar) /  ValueInDollar)*100}%");
            OldValueInDollar = ValueInDollar;
        }
    }
    }
