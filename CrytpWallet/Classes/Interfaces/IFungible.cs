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
        void GetFungible(FungibleAsset assetToAdd, int amount, bool newToken);
        void SendFungible(FungibleAsset assetToRemove, int amount);
    }
}
