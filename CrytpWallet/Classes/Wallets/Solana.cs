using CrytpWallet.Assets;
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
            AllowedAssetsFungible = new List<Guid>();
            AllowedNonFungible = new List<Guid>();
            HeldNFT = new List<Guid>();
        }
        public static List<Guid> AllowedAssetsFungible { get; protected set; }
        public static List<Guid> AllowedNonFungible { get; protected set; }
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
    }
}
