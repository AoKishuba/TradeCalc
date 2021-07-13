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

        static readonly Backpack Bull = new("Bull", 1, 5);
        static readonly Backpack Garru = new("Garru", 1, 6);
        static readonly Backpack Trader = new("Trader", 3, 3);
        static readonly Backpack None = new("No Backpack", 1, 1);

        public static Backpack[] AllBackpacks { get; } =
        {
            None,
            Bull,
            Garru,
            Trader
        };

        /// <summary>
        /// Formats item IDs for neater writing
        /// </summary>
        /// <returns>Formatted ID string for each item</returns>
        public static IEnumerable<string> GetBackPackIDList()
        {
            string idString;
            for (int index = 0; index < AllBackpacks.Length; index++)
            {
                // Format string to preserve columns
                if (index < 10)
                {
                    idString = " " + index.ToString() + " " + AllBackpacks[index].Name;
                }
                else
                {
                    idString = index.ToString() + " " + AllBackpacks[index].Name;
                }

                yield return idString;
            }
        }
    }
}
