using CrytpWallet.Assets;
using CrytpWallet.Classes.Global;
using CrytpWallet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrytpWallet.Classes.Wallets
{
    public sealed class BitcoinWallet:Wallet
    {
        public BitcoinWallet() : base() {
            type = 1;
        }
        static BitcoinWallet()
        {
            AllowedAssets = new List<Guid>()
            {
                GlobalWallets.GetAdressOfFungibleToken("BTC"),
                GlobalWallets.GetAdressOfFungibleToken("GRC"),
                GlobalWallets.GetAdressOfFungibleToken("AUR"),
                GlobalWallets.GetAdressOfFungibleToken("XRP")
            };
        }
        public static List<Guid> AllowedAssets { get; private set; }
        
        public override void PrintWallet()
        {
            Console.WriteLine("Bitcoin Wallet");
            base.PrintWallet();
            

        }
    }
}
