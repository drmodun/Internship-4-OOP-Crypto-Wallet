using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Interfaces
{
    public  interface IFungable
    {
        void GetFungable(FungableAsset assetToAdd, int amount, bool newToken, Guid TransactionAdress);
        void SendFungable(FungableAsset assetToRemove, int amount, Guid TransactionAdress);
    }
}
