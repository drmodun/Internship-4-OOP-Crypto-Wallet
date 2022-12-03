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

    public sealed class SolanaWallet : Wallet, IFungable,INonFungable
    {
        public SolanaWallet() : base() {
            AllowedAssetsFungable = new List<Guid>();
            AllowedNonFungable = new List<Guid>();
            HeldNFT = new List<Guid>();
        }
        public static List<Guid> AllowedAssetsFungable { get; protected set; }
        public static List<Guid> AllowedNonFungable { get; protected set; }
        public List<Guid> HeldNFT { get; init; }
        public void GetFungable(FungableAsset assetToAdd, int amount, bool newToken, Guid TransactionAdress )
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
        public void GetNFT(NonFungableAsset assetToAdd, Guid TransactionAdress)
        {
            HeldNFT.Add(assetToAdd.Adress);
            totalValue += assetToAdd.ValueInDollar;
            Transactions.Add(TransactionAdress);

        }
        public void SendNFT(NonFungableAsset assetToRemove, Guid TransactionAdress)
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
