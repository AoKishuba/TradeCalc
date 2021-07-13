using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace TradeCalc
{
    class Program
    {

        static int GetCityIndex(List<City> cityList, string message)
        {
            int minCityIndex = 0;
            int maxCityIndex = cityList.Count - 1;
            while (true)
            {
                Console.WriteLine(message);
                foreach (string id in City.GetCityIDList(cityList))
                {
                    Console.WriteLine(id);
                }

                if (int.TryParse(Console.ReadLine(), out int cityIndex))
                {
                    if (cityIndex < minCityIndex || cityIndex > maxCityIndex)
                    {
                        Console.WriteLine("CITY INDEX RANGE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".\n");
                    }
                    else
                    {
                        Console.WriteLine("\n");
                        return cityIndex;
                    }
                }
                else
                {
                    Console.WriteLine("CITY INDEX PARSE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".\n");
                }
            }
        }

        static void AddCity(string fileName)
        {
            Console.WriteLine("\n");
            List<City> cityList = JsonSerializer.Deserialize<List<City>>(File.ReadAllText(fileName));
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
                Console.WriteLine("Enter name for new city:");

                string input = Console.ReadLine();
                if (input.Length < minNameLength || input.Length > maxNameLength)
                {
                    Console.WriteLine("NAME LENGTH ERROR: Name must contain more than "
                        + (minNameLength - 1)
                        + " and fewer than "
                        + (maxNameLength + 1)
                        + " characters.\n");
                }
                else if (nameList.Contains(input))
                {
                    Console.WriteLine("DUPLICATE ERROR: A city with the name '" + input + "' already exists.\n");
                }
                else
                {
                    cityList.Add(new City(input));
                    Console.WriteLine("Added new city " + input);
                    break;
                }
            }

            File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
        }

        /// <summary>
        /// Removes city from city list
        /// </summary>
        public static void DeleteCity(string fileName)
        {
            Console.WriteLine("\n");
            List<City> cityList = JsonSerializer.Deserialize<List<City>>(File.ReadAllText(fileName));
            if (cityList.Count == 0)
            {
                Console.WriteLine("No cities in list. Add a city.\n");
            }
            else
            {
                int minCityIndex = 0;
                int maxCityIndex = cityList.Count - 1;

                while (true)
                {
                    Console.WriteLine("Select a city to delete, or enter 'none':");
                    foreach (string id in City.GetCityIDList(cityList))
                    {
                        Console.WriteLine(id);
                    }
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int cityIndex))
                    {
                        if (cityIndex < minCityIndex || cityIndex > maxCityIndex)
                        {
                            Console.WriteLine("CITY INDEX RANGE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".\n");
                        }
                        else
                        {
                            string cityName = cityList[cityIndex].Name;
                            cityList.RemoveAt(cityIndex);
                            Console.WriteLine("Deleted " + cityName + ".\n");
                            File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
                            break;
                        }
                    }
                    else if (input.ToLower() == "none")
                    {
                        Console.WriteLine("Deletion skipped\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("CITY INDEX PARSE ERROR: Enter an integer from " + minCityIndex + " thru " + maxCityIndex + ".\n");
                    }
                }
            }
        }

        static void UpdateName(string fileName)
        {
            Console.WriteLine("\n");
            List<City> cityList = JsonSerializer.Deserialize<List<City>>(File.ReadAllText(fileName));
            if (cityList.Count == 0)
            {
                Console.WriteLine("No cities in list. Add a city.\n");
            }
            else
            {
                int cityIndex = GetCityIndex(cityList, "Select a city to rename:");
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
                    Console.WriteLine("Enter new name for " + cityList[cityIndex].Name + ":");

                    string input = Console.ReadLine();
                    if (input.Length < minNameLength || input.Length > maxNameLength)
                    {
                        Console.WriteLine("NAME LENGTH ERROR: Name must contain more than "
                            + (minNameLength - 1)
                            + " and fewer than "
                            + (maxNameLength + 1)
                            + " characters.\n");
                    }
                    else if (nameList.Contains(input))
                    {
                        Console.WriteLine("DUPLICATE ERROR: A city with the name '" + input + "' already exists.\n");
                    }
                    else
                    {
                        cityList[cityIndex].UpdateName(input);
                        Console.WriteLine(nameList[cityIndex] + " renamed to " + input + ".\n");
                        break;
                    }
                }

                File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
            }
        }

        static void SetLocalPrices(string fileName)
        {
            Console.WriteLine("\n");
            List<City> cityList = JsonSerializer.Deserialize<List<City>>(File.ReadAllText(fileName));
            if (cityList.Count == 0)
            {
                Console.WriteLine("No cities in list. Add a city.\n");
            }
            else
            {
                int cityIndex = GetCityIndex(cityList, "Select a city to update prices:");
                string input;
                int minIndex = 0;
                int maxIndex = cityList[cityIndex].LocalTradeGoods.Count - 1;
                int minPrice = 0;
                int maxPrice = int.MaxValue;
                while (true)
                {
                    Console.WriteLine("\n" + cityList[cityIndex].Name + " local prices.");
                    Console.WriteLine("Enter item number to update local price. Enter 'done' when finished updating prices:");
                    foreach (string id in cityList[cityIndex].GetItemIDList())
                    {
                        Console.WriteLine(id);
                    }
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int itemIndex))
                    {
                        if (itemIndex < minIndex || itemIndex > maxIndex)
                        {
                            Console.WriteLine("ITEM INDEX RANGE ERROR: Enter an integer from " + minIndex + " thru " + maxIndex + ".\n");
                        }
                        else
                        {
                            Console.WriteLine("Current local price for "
                                + cityList[cityIndex].LocalTradeGoods[itemIndex].Name
                                + ": "
                                + cityList[cityIndex].LocalTradeGoods[itemIndex].LocalPrice
                                + "\nEnter new price:");
                            if (int.TryParse(Console.ReadLine(), out int newPrice))
                            {
                                if (newPrice < minPrice || newPrice > maxPrice)
                                {
                                    Console.WriteLine("LOCAL PRICE RANGE ERROR: Enter a positive integer.\n");
                                }
                                else
                                {
                                    cityList[cityIndex].LocalTradeGoods[itemIndex].LocalPrice = newPrice;
                                    Console.WriteLine(cityList[cityIndex].LocalTradeGoods[itemIndex].Name
                                        + " price set to "
                                        + cityList[cityIndex].LocalTradeGoods[itemIndex].LocalPrice + ".\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("LOCAL PRICE PARSE ERROR: Enter a positive integer.\n");
                            }
                        }
                    }
                    else if (input.ToLower() == "done")
                    {
                        foreach (TradeGood tGood in cityList[cityIndex].LocalTradeGoods)
                        {
                            Console.WriteLine(tGood.ID);
                        }
                        Console.WriteLine("\n");
                        File.WriteAllText(fileName, JsonSerializer.Serialize(cityList));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("ITEM INDEX PARSE ERROR: Enter an integer from " + minIndex + "thru " + maxIndex + ".\n");
                    }
                }
            }

        }

        static Backpack GetBackPack()
        {
            Console.WriteLine("\n");
            while (true)
            {
                Console.WriteLine("Enter a number to select a backpack:");
                foreach (string id in Backpack.GetBackPackIDList())
                {
                    Console.WriteLine(id);
                }

                int minBackpackIndex = 0;
                int maxBackpackIndex = Backpack.AllBackpacks.Length - 1;

                if (int.TryParse(Console.ReadLine(), out int backpackIndex))
                {
                    if (backpackIndex < minBackpackIndex || backpackIndex > maxBackpackIndex)
                    {
                        Console.WriteLine("BACKPACK INDEX RANGE ERROR: Enter an integer from " + minBackpackIndex + " thru " + maxBackpackIndex + ".\n");
                    }
                    else
                    {
                        Console.WriteLine("Selected " + Backpack.AllBackpacks[backpackIndex].Name + ".\n");
                        return Backpack.AllBackpacks[backpackIndex];
                    }
                }
                else
                {
                    Console.WriteLine("BACKPACK INDEX PARSE ERROR: Enter an integer from " + minBackpackIndex + " thru " + maxBackpackIndex + ".\n");
                }
            }
        }

        static void GetProfitList(string fileName)
        {
            Console.WriteLine("\n");
            List<City> cityList = JsonSerializer.Deserialize<List<City>>(File.ReadAllText(fileName));
            List<Profit> profitList = new();
            if (cityList.Count < 2)
            {
                Console.WriteLine("Need at least 2 cities to compare prices.\n");
            }
            else
            {
                int sourceIndex = GetCityIndex(cityList, "Select a SOURCE city:");
                int destinationIndex;
                while (true)
                {
                    destinationIndex = GetCityIndex(cityList, "Select a DESTINATION city:");
                    if (destinationIndex == sourceIndex)
                    {
                        Console.WriteLine("Source and destination must be different cities.\n");
                    }
                    else
                    {
                        break;
                    }
                }
                Backpack backpack = GetBackPack();
                foreach (Profit profit in cityList[sourceIndex].GetProfits(
                    cityList[destinationIndex].LocalTradeGoods,
                    backpack.StackableBonusMin,
                    backpack.StackableBonusMultiplier))
                {
                    profitList.Add(profit);
                }

                profitList.Sort((x, y) => y.ProfitPerSquare.CompareTo(x.ProfitPerSquare));
                Console.WriteLine("Profit per inventory square for goods from "
                    + cityList[sourceIndex].Name
                    + " to " + cityList[destinationIndex].Name
                    + ":");
                foreach (Profit profit in profitList)
                {
                    Console.WriteLine(profit.ID);
                }
                Console.WriteLine("\n");
            }
        }

        static void Main()
        {
            string FileName = "citylist.json";

            Console.WriteLine("TradeCalc: A trade optimizer for Kenshi. Written by Ao Kishuba.\n");

            if (!File.Exists(FileName))
            {
                Console.WriteLine(FileName + " not detected. Creating new file. Be sure to start by changing city name.");

                List<City> CityList = new();
                CityList.Add(new City("New City"));
                File.WriteAllText(FileName, JsonSerializer.Serialize(CityList));
            }

            while (true)
            {
                Console.WriteLine("\nEnter a number to select an option:");
                Console.WriteLine("0 Add city\n1 Delete city\n2 Change name\n3 Edit price list\n4 Optimize trade");
                int minMenuIndex = 0;
                int maxMenuIndex = 4;
                if (int.TryParse(Console.ReadLine(), out int menuIndex))
                {
                    if (menuIndex < minMenuIndex || menuIndex > maxMenuIndex)
                    {
                        Console.WriteLine("MENU INDEX RANGE ERROR: Enter an integer from " + minMenuIndex + " thru " + maxMenuIndex + ".\n");
                    }
                    else if (menuIndex == 0)
                    {
                        AddCity(FileName);
                    }
                    else if (menuIndex == 1)
                    {
                        DeleteCity(FileName);
                    }
                    else if (menuIndex == 2)
                    {
                        UpdateName(FileName);
                    }
                    else if (menuIndex == 3)
                    {
                        SetLocalPrices(FileName);
                    }
                    else if (menuIndex == 4)
                    {
                        GetProfitList(FileName);
                    }
                }
                else
                {
                    Console.WriteLine("MENU INDEX PARSE ERROR: Enter an integer from " + minMenuIndex + " thru " + maxMenuIndex + ".\n");
                }
            }
        }
    }
}
