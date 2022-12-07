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
        public Dictionary<Guid, int> AmountOfAssets { get; init; }
        public List<Guid> Transactions { get; init; }
        public decimal totalValue { get; protected set; }
        public decimal oldValue { get; protected set; }
        public int type { get; init; }
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
                $"\nTotalna promjena vrijednosti {((totalValue - oldValue) / oldValue)*100}%");
            oldValue = totalValue;
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
            //oldValue = totalValue;
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
                Console.WriteLine($"{assetToAdd.Adress} {AmountOfAssets.Keys.Count}");
                AmountOfAssets.Add(assetToAdd.Adress, amount);
            }
            else
            {
                Console.WriteLine(AmountOfAssets[assetToAdd.Adress]);
                AmountOfAssets[assetToAdd.Adress] += amount;
                Console.WriteLine(AmountOfAssets[assetToAdd.Adress]);
                Console.WriteLine(amount);
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
        public void PrintAllTransactions()
        {
            foreach(var item in Transactions)
            {
                var transaction = GlobalWallets.GetTransactionById(item);
                if (transaction as FungibleTransaction!=null)
                {
                    var transactionFungible= transaction as FungibleTransaction;
                    transactionFungible.PrintTrasnsaction();
                    return;
                }
                ((NonFungibleTransaction)transaction).PrintTrasnsaction();
            }
        }
    }
}

