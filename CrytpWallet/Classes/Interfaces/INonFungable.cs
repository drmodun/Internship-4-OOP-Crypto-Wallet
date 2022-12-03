using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Interfaces
{
    public interface INonFungable
    {
        void GetNFT(NonFungableAsset assetToAdd, Guid TransactionAdress);
        void SendNFT(NonFungableAsset assetToRemove, Guid TransactionAdress);
    }
}
