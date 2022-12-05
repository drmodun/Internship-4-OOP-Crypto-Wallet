using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{

    public sealed class SolanaWallet : Wallet, IFungible,INonFungible
    {
        public SolanaWallet() : base() {
            
            HeldNFT = new List<Guid>();
        }
        static SolanaWallet()
        {
            AllowedAssetsFungibleSolana = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("SOL"),
                GlobalWallets.GetAdressOfFungibleToken("DOGE"),
                GlobalWallets.GetAdressOfFungibleToken("LTC"),
                GlobalWallets.GetAdressOfFungibleToken("NMC")
            };

            AllowedNonFungibleSolana = new List<Guid>();
            foreach (var item in GlobalWallets.AllNonFungibleAssets)
            {
                foreach (var id in AllowedAssetsFungibleSolana)
                {
                    if (item.ItsFungible == id)
                        AllowedNonFungibleSolana.Add(item.Adress);
                }
            }
        }
        public static List<Guid> AllowedAssetsFungibleSolana { get; protected set; }
        public static List<Guid> AllowedNonFungibleSolana { get; protected set; }
        public List<Guid> HeldNFT { get; init; }
        public void GetFungible(FungibleAsset assetToAdd, int amount, bool newToken, Guid TransactionAdress )
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
        public void SendFungible(FungibleAsset assetToRemove, int amount, Guid TransactionAdress)
        {
            AmountOfAssets[assetToRemove.Adress] -= amount;
            totalValue -= amount * assetToRemove.ValueInDollar;
            Transactions.Add(TransactionAdress);

        }
        public void GetNFT(NonFungibleAsset assetToAdd, Guid TransactionAdress)
        {
            HeldNFT.Add(assetToAdd.Adress);
            totalValue += assetToAdd.ValueInDollar;
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
            Console.WriteLine("Solana Wallet");
            base.PrintWallet();

        }
        public override void CalculateValue()
        {
            base.CalculateValue();
            foreach (var item in HeldNFT)
            {
                totalValue += GlobalWallets.GetFungibleAssetByAdress(GlobalWallets.GetNonFungibleAssetByAdress(item).ItsFungible).ValueInDollar;
            }
            if (oldValue < 0)
            {
                oldValue = totalValue;
            }
        }
    }
}
