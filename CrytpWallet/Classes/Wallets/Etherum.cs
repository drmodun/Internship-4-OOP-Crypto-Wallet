using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{

    public sealed  class EtherumWallet : Wallet,INonFungible
    {
        public EtherumWallet() : base() {
            type = 2;
            
            HeldNFT = new List<Guid>();
        }
        static EtherumWallet()
        {
            AllowedAssetsFungibleEtherum = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("ETH"),
                GlobalWallets.GetAdressOfFungibleToken("GRC"),
                GlobalWallets.GetAdressOfFungibleToken("DOGE"),
                GlobalWallets.GetAdressOfFungibleToken("XRP"),
                GlobalWallets.GetAdressOfFungibleToken("BNB")
            };
            AllowedNonFungibleEtherum = new List<Guid>()
            { };
            //Adding all nfts which this wallet can support based on supported cryptocurrencies
            foreach (var item in GlobalWallets.AllNonFungibleAssets)
            {
                foreach (Guid id in AllowedAssetsFungibleEtherum)
                {
                    if (item.ItsFungible == id)
                        AllowedNonFungibleEtherum.Add(item.Adress);
                }
            }
        }
        public static List<Guid> AllowedAssetsFungibleEtherum { get; protected set; }
        public static List<Guid> AllowedNonFungibleEtherum { get; protected set; }
        public List<Guid> HeldNFT { get; init; }
        public void GetNFT(NonFungibleAsset assetToAdd, Guid TransactionAdress) 
        {
            HeldNFT.Add(assetToAdd.Adress);
            totalValue+=assetToAdd.ValueInDollar;
            Transactions.Add(TransactionAdress);
        }
        public void SendNFT(NonFungibleAsset assetToRemove, Guid TransactionAdress)
        {
            HeldNFT.Remove(assetToRemove.Adress);
            totalValue -= assetToRemove.ValueInDollar;
            Transactions.Add(TransactionAdress);
        }
        public override void PrintWallet()
        {
            Console.WriteLine("Etherum Wallet");
            base.PrintWallet();

        }
        public override bool CalculateValue()
        {
            var initial=base.CalculateValue();
            foreach (var item in HeldNFT)
            {
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
        //Change these funcitons later, make them a bit better
    }
}
