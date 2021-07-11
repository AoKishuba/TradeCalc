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
            foreach (TradeGood item in TradeGood.AllItems)
            {
                ItemList.Add(item);
            }
        }

        public string Name { get; set; }
        public List<TradeGood> ItemList { get; } = new();
        public List<Profit> ProfitList { get; set; }


        /// <summary>
        /// Formats item IDs for neater writing
        /// </summary>
        /// <returns>Formatted ID string for each item</returns>
        IEnumerable<string> GetItemIDList()
        {
            string idString;
            for (int index = 0; index < ItemList.Count; index++)
            {
                // Format string to preserve columns
                if (index < 10)
                {
                    idString = " " + index.ToString() + " " + ItemList[index].ID;
                }
                else
                {
                    idString = index.ToString() + " " + ItemList[index].ID;
                }

                yield return idString;
            }
        }

        /// <summary>
        /// Formats city names for neater writing
        /// </summary>
        /// <returns>Formatted ID string for each city</returns>
        static IEnumerable<string> GetCityIDList(List<City> cityList)
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
        /// Gets new city name from user
        /// </summary>
        /// <param name="fileName">Citylist JSON file</param>
        /// <param name="cityList">List of all cities</param>
        /// <returns>Updated list of all cities</returns>
        public List<City> UpdateName(string fileName, List<City> cityList)
        {
            int minNameLength = 1;
            int maxNameLength = 35;
            // Name list to check for duplicates
            List<string> nameList = new();
            foreach (City city in cityList)
            {
                nameList.Add(city.Name);
            }

            while (true)
            {
                Console.WriteLine("Enter new city name");

                string input = Console.ReadLine();
                if (input.Length < minNameLength || input.Length > maxNameLength) 
                {
                    Console.WriteLine("NAME LENGTH ERROR: Name must contain more than "
                        + (minNameLength - 1)
                        + " and fewer than "
                        + (maxNameLength + 1)
                        + " characters.");
                }
                else if (nameList.Contains(input))
                {
                    Console.WriteLine("DUPLICATE ERROR: A city with the name '" + input + "' already exists");
                }
                else
                {
                    Name = input;
                    break;
                }
            }

            File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
            return cityList;
        }

        /// <summary>
        /// Gets updated local prices from user
        /// </summary>
        /// <param name="fileName">Citylist JSON file</param>
        /// <param name="cityList">List of all cities</param>
        /// <returns>Updated list of all cities</returns>
        public List<City> UpdateLocalPrices(string fileName, List<City> cityList)
        {
            string input;
            int minIndex = 0;
            int maxIndex = ItemList.Count;
            int minPrice = 0;
            int maxPrice = int.MaxValue;
            while (true)
            {
                Console.Write("\n" + Name + "\n");
                foreach (string id in GetItemIDList())
                {
                    Console.WriteLine(id);
                }
                Console.WriteLine("\nEnter item number to update local price. Enter 'done' when finished updating prices.");
                input = Console.ReadLine();
                if (int.TryParse(input, out int index))
                {
                    if (index < minIndex || index > maxIndex)
                    {
                        Console.WriteLine("\nITEM INDEX RANGE ERROR: Enter an integer from " + minIndex + " thru " + maxIndex + ".");
                    }
                    else
                    {
                        Console.WriteLine("\nEnter new local price for " + ItemList[index].Name + ".\nCurrent price: " + ItemList[index].LocalPrice);
                        input = Console.ReadLine();
                        if (int.TryParse(input, out int newPrice))
                        {
                            if (newPrice < minPrice || newPrice > maxPrice)
                            {
                                Console.WriteLine("\nLOCAL PRICE RANGE ERROR: Enter a positive integer.");
                            }
                            else
                            {
                                ItemList[index].LocalPrice = newPrice;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nLOCAL PRICE PARSE ERROR: Enter a positive integer.");
                        }
                    }
                }
                else if (input.ToLower() == "done")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nITEM INDEX PARSE ERROR: Enter an integer from " + minIndex + "thru " + maxIndex + ".");
                }
            }

            File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
            return cityList;
        }

        /// <summary>
        /// Calculates profit per square of hauling an item from this city to destination city
        /// </summary>
        /// <param name="fileName">Citylist JSON file</param>
        /// <param name="cityList">List of all cities</param>
        /// <param name="stackableBonusMin">Min stack size from backpack</param>
        /// <param name="stackableBonusMultiplier">Stack size multiplier from backpack</param>
        /// <returns>Updated list of all cities</returns>
        public static List<City> GetProfitList(string fileName, List<City> cityList, int stackableBonusMin, int stackableBonusMultiplier)
        {
            if (cityList.Count < 2)
            {
                Console.WriteLine("Need at least 2 cities to compare prices.");
            }
            else
            {
                int minCityIndex = 0;
                int maxCityIndex = cityList.Count;
                string input;
                while (true)
                {
                    foreach (string cityName in GetCityIDList(cityList))
                    {
                        Console.WriteLine(cityName);
                    }
                    Console.WriteLine("\nEnter a number from " + minCityIndex + " thru " + maxCityIndex + " to select SOURCE city.");

                    if (int.TryParse(Console.ReadLine(), out int sourceIndex))
                    {
                        if (sourceIndex < minCityIndex || sourceIndex > maxCityIndex)
                        {
                            Console.WriteLine("SOURCE INDEX RANGE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".");
                        }
                        else
                        {
                            while (true)
                            {
                                foreach (string cityName in GetCityIDList(cityList))
                                {
                                    Console.WriteLine(cityName);
                                }
                                Console.WriteLine("\nEnter a number from " + minCityIndex + " thru " + maxCityIndex + " to select DESTINATION city.");

                                if (int.TryParse(Console.ReadLine(), out int destinationIndex))
                                {
                                    if (destinationIndex < minCityIndex || destinationIndex > maxCityIndex)
                                    {
                                        Console.WriteLine("DESTINATION INDEX RANGE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".");
                                    }
                                    else
                                    {
                                        List<TradeGood> sourceList = cityList[sourceIndex].ItemList;
                                        List<TradeGood> destinationList = cityList[destinationIndex].ItemList;

                                        for (int index = 0; index < TradeGood.AllItems.Length; index++)
                                        {
                                            TradeGood item = TradeGood.AllItems[index];

                                            int effectiveStack = stackableBonusMin > item.StackSize ? stackableBonusMin : item.StackSize;
                                            effectiveStack *= stackableBonusMultiplier;

                                            decimal effectiveSquares = (decimal)item.InventorySize / effectiveStack;

                                            decimal profitPerSquare = (destinationList[index].LocalPrice - sourceList[index].LocalPrice)
                                                / effectiveStack;

                                            ProfitList.Add(new Profit() { Name = item.Name, ProfitPerSquare = profitPerSquare });
                                        }

                                        // Sort list by descending profit per square
                                        ProfitList.Sort((x, y) => y.ProfitPerSquare.CompareTo(x.ProfitPerSquare));
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("DESTINATION INDEX PARSE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".");
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("SOURCE INDEX PARSE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".");
                    }
                }
            }

            File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
            return cityList;
        }
    }
}
