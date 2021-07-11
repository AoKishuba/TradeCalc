using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeCalc
{
    class Backpack
    {
        public Backpack(string name, int stackableBonusMin, int stackableBonusMultiplier)
        {
            Name = name;
            StackableBonusMin = stackableBonusMin;
            StackableBonusMultiplier = stackableBonusMultiplier;
        }
        public string Name { get; }
        public int StackableBonusMin { get; }
        public int StackableBonusMultiplier { get; }

        static Backpack Bull = new("Bull", 1, 5);
        static Backpack Garru = new("Garru", 1, 6);
        static Backpack Trader = new("Trader", 3, 3);
        static Backpack None = new("No Backpack", 1, 1);

        public static Backpack[] AllBackpacks { get; } =
        {
            None,
            Bull,
            Garru,
            Trader
        };
    }
}
