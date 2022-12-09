using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class DoubleWallet : Wallet, INonFungible
    {
        public DoubleWallet() : base() {
            HeldNFT = new List<Guid>();
        }
        public List<Guid> HeldNFT { get; init; }
        public void GetNFT(NonFungibleAsset assetToAdd)
        {
            HeldNFT.Add(assetToAdd.Adress);
            TotalValue += assetToAdd.ValueInDollar;
        }
        public void SendNFT(NonFungibleAsset assetToRemove)
        {
            HeldNFT.Remove(assetToRemove.Adress);
            TotalValue -= assetToRemove.ValueInDollar;
        }
        public override decimal CalculateValue()
        {
            var startingValue= base.CalculateValue();

            foreach (var item in HeldNFT)
            {
                TotalValue += GlobalWallets.GetFungibleAssetByAdress(GlobalWallets.GetNonFungibleAssetByAdress(item).ItsFungible).ValueInDollar*GlobalWallets.GetNonFungibleAssetByAdress(item).ValueInFungible;
                //Dont like the way I am getting the vlaue here, might have to make it more tidy
            }
            if (TotalValue != startingValue && startingValue!=-1)
            {
                HasChanged= true;
            }
            return 0;
        }


    }
}
    