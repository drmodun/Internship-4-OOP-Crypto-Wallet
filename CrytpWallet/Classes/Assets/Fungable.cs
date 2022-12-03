using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public class FungableAsset : Asset  
    {
        public FungableAsset() : base() { }
        public string Label { get; }
        public double ValueInDollar { get; }
    }
}
