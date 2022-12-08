using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Interfaces
{
    public  interface IFungible
    {
        void GetFungible(FungibleAsset assetToAdd, decimal amount, bool newToken);
        void SendFungible(FungibleAsset assetToRemove, decimal amount);
    }
}
