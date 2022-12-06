using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class DoubleWallet : Wallet, INonFungible
    {
        public DoubleWallet() : base() { 

        }
        //public abstract List<DoubleWallet> GetWallets();
        public List<Guid> HeldNFT { get; init; }
        public void GetNFT(NonFungibleAsset assetToAdd)
        {
            HeldNFT.Add(assetToAdd.Adress);
            totalValue += assetToAdd.ValueInDollar;
            //Transactions.Add(TransactionAdress);
        }
        public void SendNFT(NonFungibleAsset assetToRemove)
        {
            HeldNFT.Remove(assetToRemove.Adress);
            totalValue -= assetToRemove.ValueInDollar;
            //Transactions.Add(TransactionAdress);
        }
        public override bool CalculateValue()
        {
            var initial = base.CalculateValue();
            foreach (var item in HeldNFT)
            {
                if (item==Guid.Empty)
                    Console.WriteLine($"{HeldNFT.IndexOf(item)}");
                totalValue += GlobalWallets.GetFungibleAssetByAdress(GlobalWallets.GetNonFungibleAssetByAdress(item).ItsFungible).ValueInDollar;
                //Dont like the way I am getting the vlaue here, might have to make it more tidy
            }
            if (!initial)
            {
                oldValue = totalValue;
                return false;
            }
            return true;
        }


    }
}
