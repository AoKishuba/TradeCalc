using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TradeCalc
{
    class City
    {
        /// <summary>
        /// Creates an item list associated with given city name in a JSON file
        /// </summary>
        /// <param name="name">City name</param>
        public City(string name)
        {
            Name = name;
            foreach (TradeGood tGood in TradeGood.AllItems)
            {
                LocalTradeGoods.Add(new TradeGood(tGood.Name, tGood.InventorySize, tGood.StackSize, tGood.AveragePrice, tGood.ItemWeight));
            }
        }

        public string Name { get; set; }
        public List<TradeGood> LocalTradeGoods { get; set; } = new();


        /// <summary>
        /// Formats item IDs for neater writing
        /// </summary>
        /// <returns>Formatted ID string for each item</returns>
        public IEnumerable<string> GetItemIDList()
        {
            string idString;
            for (int index = 0; index < LocalTradeGoods.Count; index++)
            {
                // Format string to preserve columns
                if (index < 10)
                {
                    idString = " " + index.ToString() + " " + LocalTradeGoods[index].ID;
                }
                else
                {
                    idString = index.ToString() + " " + LocalTradeGoods[index].ID;
                }

                yield return idString;
            }
        }

        /// <summary>
        /// Formats city names for neater writing
        /// </summary>
        /// <returns>Formatted ID string for each city</returns>
        public static IEnumerable<string> GetCityIDList(List<City> cityList)
        {
            string idString;
            for (int index = 0; index < cityList.Count; index++)
            {
                // Format string to preserve columns
                if (index < 10)
                {
                    idString = " " + index.ToString() + " " + cityList[index].Name;
                }
                else
                {
                    idString = index.ToString() + " " + cityList[index].Name;
                }

                yield return idString;
            }
        }

        /// <summary>
        /// Updates city name
        /// </summary>
        public void UpdateName(string newName)
        {
            Name = newName;
        }


        /// <summary>
        /// Calculates profit per square of hauling an item from this city to destination city
        /// </summary>
        /// <param name="stackableBonusMin">Min stack size from backpack</param>
        /// <param name="stackableBonusMultiplier">Stack size multiplier from backpack</param>
        /// <returns>Profit item</returns>
        public IEnumerable<Profit> GetProfits(List<TradeGood> destinationList, int stackableBonusMin, int stackableBonusMultiplier)
        {
            for (int index = 0; index < TradeGood.AllItems.Count; index++)
            {
                TradeGood item = TradeGood.AllItems[index];

                int effectiveStack = stackableBonusMin > item.StackSize ? stackableBonusMin : item.StackSize;
                effectiveStack *= stackableBonusMultiplier;

                decimal effectiveSquares = (decimal)item.InventorySize / effectiveStack;

                decimal profitPerSquare = (destinationList[index].LocalPrice - LocalTradeGoods[index].LocalPrice)
                    / effectiveSquares;

                if (destinationList[index].LocalPrice > 0 && LocalTradeGoods[index].LocalPrice > 0 && profitPerSquare > 0)
                {
                    yield return new Profit() { Name = item.Name, ProfitPerSquare = profitPerSquare };
                }
            }
        }
    }
}
