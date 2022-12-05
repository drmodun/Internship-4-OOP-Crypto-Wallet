using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{
    public sealed class BitcoinWallet:Wallet, IFungible
    {
        public BitcoinWallet() : base() { 
            
        }
        static BitcoinWallet()
        {
            AllowedAssets = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("BTC"),
                GlobalWallets.GetAdressOfFungibleToken("GRC"),
                GlobalWallets.GetAdressOfFungibleToken("AUR"),
                GlobalWallets.GetAdressOfFungibleToken("XRP")
            };
        }
        public static List<Guid> AllowedAssets { get; private set; }
        public void GetFungible(FungibleAsset assetToAdd, int amount, bool newToken, Guid TransactionAdress)
        {
            if (newToken)
            {
                AmountOfAssets.Add(assetToAdd.Adress, amount);
            }
            else
            {
                AmountOfAssets[assetToAdd.Adress] += amount;
                totalValue -= amount * assetToAdd.ValueInDollar;
            }
            Transactions.Add(TransactionAdress);

        }
        public void SendFungible(FungibleAsset assetToRemove, int amount, Guid TransactionAdress)
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
