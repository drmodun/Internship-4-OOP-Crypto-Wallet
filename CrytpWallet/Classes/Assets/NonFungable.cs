using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public sealed class NonFungableAsset:Asset
    {
            public NonFungableAsset(double valueInDollar) : base(valueInDollar) { }
            public Guid ItsFungableValue { get; init; }
            public double ValueInFungable { get; init;  }
        public override void UpdateValue()
        {
            
            OldValueInDollar = ValueInDollar;
            //Mijenjanje vrijenosti radim izvan objekta pošto nemam access cijeloj listi pa ne mogu preko adrese dobiti želejeni objekt
        }
        public override void PrintAsset()
        {
            base.PrintAsset();
            Console.WriteLine($"Fungable asset: {ItsFungableValue}\n" +
                $"\nKoličina fungable assera {ValueInFungable}" +
                $"Mijenjanje vrijednosti: {(ValueInDollar - OldValueInDollar) / 100}%");
        }
    }
    }
/*Pitanja
 * Na koju se vrijednost misli u NFT
 * Šta znači nije moguće mijenjati referencu klase
 * Na koje načine implementirati interface, abstrakciju, inheritance i enkapsulaciju
 * Moramo li polimorfizam
 * Pitanja za funkciju aplikacije
 * Funkcije nutar klase ili moze i van
 * Smijemo li koristiti global
 * Jeu li formati konačni ili izmenjivi
 * Smijemo li korisitit konstruktore
 *Dodaj bolji output
 *Provjeravaj casting
 *Pokusaj skuzit polimorfizam
 *Dodaj predefinirane gluposti
 */
