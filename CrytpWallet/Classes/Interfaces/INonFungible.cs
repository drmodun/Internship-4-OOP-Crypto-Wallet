using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Interfaces
{
    public interface INonFungible
    {
        void GetNFT(NonFungibleAsset assetToAdd);
        void SendNFT(NonFungibleAsset assetToRemove);
    }
}
