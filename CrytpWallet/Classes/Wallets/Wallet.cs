using CrytpWallet.Classes.Transactions;
using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class Wallet : IFungible
    {
        public Guid Adress { get; init; }
        public Dictionary<Guid, int> AmountOfAssets { get; protected set; }
        public List<Guid> Transactions { get; protected set; }
        public double totalValue { get; protected set; }
        public double oldValue { get; protected set; }
        public int type { get; protected set; }
        public Wallet()
        {
            Adress = Guid.NewGuid();
            AmountOfAssets = new Dictionary<Guid, int>();
            Transactions = new List<Guid>();
            totalValue = -1;
            oldValue = -1;
        }
        public virtual void PrintWallet()
        {
            Console.WriteLine($"Adresa walleta: {Adress}" +
                $"\nTotalna vrijednost: {totalValue}" +
                $"\nTotalna promjena vrijednosti {((totalValue - oldValue) / totalValue)*100}%");
        }
        public virtual void PrintWalletContents()
        {
            foreach (var item in AmountOfAssets)
            {
                GlobalWallets.AllFungibleAssets.Find(x => x.Adress == item.Key).PrintAsset();
                Console.WriteLine(" ");
            }
        }
        public virtual bool CalculateValue()
        {
            oldValue = totalValue;
            totalValue = 0;
            foreach (var item in AmountOfAssets)
            {
                totalValue += GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value;
                
            }
            if (oldValue < 0)
            {
                oldValue = totalValue;
                return false;
            }
            return true;

        }
        public void GetFungible(FungibleAsset assetToAdd, int amount, bool newToken)
        {
            if (newToken == true)
            {
                AmountOfAssets.Add(assetToAdd.Adress, amount);
            }
            else
            {
                AmountOfAssets[assetToAdd.Adress] += amount;
                //totalValue += amount * assetToAdd.ValueInDollar;
            }
            CalculateValue();
        }
        public void SendFungible(FungibleAsset assetToRemove, int amount)
        {
            AmountOfAssets[assetToRemove.Adress] -= amount;
            //totalValue -= amount * assetToRemove.ValueInDollar;
            CalculateValue();
            //try handling more things in main or global static classes
        }
    }
}

