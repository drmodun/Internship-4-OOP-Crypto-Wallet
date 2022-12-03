using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public class NonFungableAsset:Asset
    {
            public NonFungableAsset() : base() { }
            public Guid ItsFungableValue { get; }
            public double ValueInDollar { get; }
        }
    }
