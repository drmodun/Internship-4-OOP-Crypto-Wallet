using CrytpWallet.Assets;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{
    public sealed class BitcoinWallet:Wallet, IFungable
    {
        public BitcoinWallet() : base() { }
        public static List<Guid> AllowedAssets { get; set; }
        public void GetFungable(FungableAsset assetToAdd, int amount, bool newToken, Guid TransactionAdress)
        {
            if (newToken == true)
            {
                AmountOfAssets.Add(assetToAdd.Adress, amount);
            }
            else
            {
                AmountOfAssets[assetToAdd.Adress] += amount;
                totalValue += amount * assetToAdd.ValueInDollar;
            }
            Transactions.Add(TransactionAdress);

        }
        public void SendFungable(FungableAsset assetToRemove, int amount, Guid TransactionAdress)
        {
            AmountOfAssets[assetToRemove.Adress] -= amount;
            totalValue -= amount * assetToRemove.ValueInDollar;
            Transactions.Add(TransactionAdress);

        }
        public override void PrintWallet()
        {
            Console.WriteLine("Bitcoin Wallet");
            base.PrintWallet();

        }
    }
}
