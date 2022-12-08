using CrytpWallet.Classes.Transactions;
using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class Wallet : IFungible
    {
        public Guid Adress { get; init; }
        public Dictionary<Guid, decimal> AmountOfAssets { get; init; }
        public List<Guid> Transactions { get; init; }
        public decimal TotalValue { get; protected set; }
        public decimal OldValue { get; protected set; }
        public int Type { get; init; } 
        public bool IsPredefined { get; init; }
        protected bool HasChanged;
        public Wallet()
        {
            Adress = Guid.NewGuid();
            AmountOfAssets = new Dictionary<Guid, decimal>();
            Transactions = new List<Guid>();
            TotalValue = -1;
            OldValue = -1;
            IsPredefined= true;
            HasChanged= false;
        }
        public virtual void PrintWallet()
        {
            var changePrint = "";
            if (OldValue <= -1 && (!HasChanged || IsPredefined))
            {
                changePrint = "\nTotalna promjena vrijednosti: 0%";
            }
            else if (HasChanged && OldValue <= 0)
            {
                changePrint = $"\nTotalna promjena vrijednosti: 100% (vrijednost prije bila 0 pa nije moguće reći)";
            }
            else if (OldValue == 0 && !HasChanged && !IsPredefined && TotalValue != 0)
            {
                changePrint = $"\nTotalna promjena vrijednosti: 0%";
            }
            else if (OldValue == 0 && TotalValue == 0)
                changePrint = "\nTotalna promjena vrijednosti: 0%";
            else
            {

                changePrint = $"\nTotalna promjena vrijednosti {((TotalValue - OldValue) / OldValue) * 100}%";
            }
            Console.WriteLine($"Adresa walleta: {Adress}" +
                $"\nTotalna vrijednost: {TotalValue}" +
                $"{changePrint}");
            OldValue = TotalValue;
            HasChanged = false;
        }
        public virtual decimal CalculateValue()
        {
            var startingValue = TotalValue;
            //OldValue = TotalValue;
            TotalValue = 0;
            foreach (var item in AmountOfAssets)
            {
                TotalValue += GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value;
                
            }
            if (TotalValue != startingValue && startingValue!=-1)
            {
                HasChanged= true;
            }
            return startingValue;
        }
        public void GetFungible(FungibleAsset assetToAdd, decimal amount, bool newToken)
        {
            if (newToken == true)
            {
                AmountOfAssets.Add(assetToAdd.Adress, amount);
            }
            else
            {
                AmountOfAssets[assetToAdd.Adress] += amount;
                //TotalValue += amount * assetToAdd.ValueInDollar;
            }
            CalculateValue();
        }
        public void SendFungible(FungibleAsset assetToRemove, decimal amount)
        {
            AmountOfAssets[assetToRemove.Adress] -= amount;
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
                    transactionFungible.PrintTransaction();
                    return;
                }
                ((NonFungibleTransaction)transaction).PrintTransaction();
                Console.WriteLine("");
            }
        }
    }
}

