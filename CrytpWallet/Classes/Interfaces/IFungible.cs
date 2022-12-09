using CrytpWallet.Assets;
namespace CrytpWallet.Classes.Interfaces
{
    public  interface IFungible
    {
        void GetFungible(FungibleAsset assetToAdd, decimal amount, bool newToken);
        void SendFungible(FungibleAsset assetToRemove, decimal amount);
    }
}
