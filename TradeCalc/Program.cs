using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TradeCalc
{
    class Program
    {
        static void Main()
        {
            string FileName = "citylist.json";
            Console.WriteLine("TradeCalc: A trade optimizer for Kenshi. Written by Ao Kishuba.\n");

            if (!File.Exists(FileName))
            {
                Console.WriteLine(FileName + " not detected. Creating new file.");

                List<City> CityList = new();
                CityList.Add(new City("new"));

                CityList = CityList[0].UpdateName(FileName, CityList);
                CityList = CityList[0].UpdateLocalPrices(FileName, CityList);
            }
            else
            {
                List<City> CityList = JsonSerializer.Deserialize<List<City>>(File.ReadAllText(FileName));
            }

            while (true)
            {

            }
        }
    }
}
