﻿using CrytpWallet.Classes.Transactions;
using CrytpWallet.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrytpWallet.Classes.Global;

namespace CrytpWallet.Classes.Wallets
{
    public abstract class Wallet
    {
        public Guid Adress { get; init; }
        public Dictionary<Guid, int> AmountOfAssets { get; protected set; }
        public List<Guid> Transactions { get; protected set; }
        public double totalValue { get; protected set; }
        public double oldValue { get; protected set; }
        public Wallet()
        {
            Adress = Guid.NewGuid();
            AmountOfAssets = new Dictionary<Guid, int>();
            Transactions = new List<Guid>();
            totalValue = 0;
            oldValue = -1;
        }
        public virtual void PrintWallet()
        {
            Console.WriteLine($"Adresa walleta: {Adress}" +
                $"\nTotalna vrijednost: {totalValue}" +
                $"\nTotalna promjena vrijednosti {((totalValue - oldValue) / totalValue)*100}%");
        }
        public virtual void PrintWalletCOntents()
        {
            foreach (var item in AmountOfAssets)
            {
                GlobalWallets.AllFungibleAssets.Find(x => x.Adress == item.Key).PrintAsset();
                Console.WriteLine(" ");
            }
        }
        public virtual void CalculateValue()
        {
            oldValue = totalValue;
            foreach (var item in AmountOfAssets)
            {
                totalValue += GlobalWallets.GetFungibleAssetByAdress(item.Key).ValueInDollar * item.Value;
                
            }
            if (oldValue < 0)
                oldValue = totalValue;


        }
    }
}

