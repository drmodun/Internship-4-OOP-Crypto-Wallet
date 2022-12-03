using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Assets
{
    public abstract class Asset
    {
        public Guid Adresa { get; set; }
        public string Name { get; }
        public double ValueInDolar { get; }
    }
}
