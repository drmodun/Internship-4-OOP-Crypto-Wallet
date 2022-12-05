﻿using System;
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

            //Mijenjanje vrijenosti radim izvan objekta pošto nemam access cijeloj listi pa ne mogu preko adrese dobiti želejeni objekt
        }
        public override void PrintAsset()
        {
            base.PrintAsset();
            Console.WriteLine($"Vrijednost u fungible assetu: {ItsFungible}\n" +
                $"\nKoličina Fungible asseta {ValueInFungible}" +
                $"Mijenjanje vrijednosti: {(ValueInDollar - OldValueInDollar) / 100}%");
        }
    }
    }
