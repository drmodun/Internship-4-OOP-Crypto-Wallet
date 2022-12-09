using CrytpWallet.Assets;

namespace CrytpWallet.Classes.Interfaces
{
    public interface INonFungible
    {
        void GetNFT(NonFungibleAsset assetToAdd);
        void SendNFT(NonFungibleAsset assetToRemove);
    }
}
