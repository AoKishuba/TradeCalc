using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeCalc
{
    class TradeGood
    {
        /// <summary>
        /// An item bought and sold by vendors
        /// </summary>
        /// <param name="name">Item name</param>
        /// <param name="inventorySize">Inventory size in squares</param>
        /// <param name="stackSize">How many of this item can be stacked in overlapping squares before a new stack is created</param>
        /// <param name="averagePrice">Average price</param>
        /// <param name="itemWeight">Weight in kg</param>
        TradeGood(string name, int inventorySize, int stackSize, int averagePrice, decimal itemWeight)
        {
            Name = name;
            InventorySize = inventorySize;
            StackSize = stackSize;
            AveragePrice = averagePrice;
            ItemWeight = itemWeight;

            // Format ID to match columns
            ID = name;
            for (int i = 0; i < 35 - name.Length - LocalPrice.ToString().Length; i++)
            {
                ID += " ";
            }
            ID += "0";
        }

        // Set at item creation
        public string Name { get; }
        public int InventorySize { get; }
        public int StackSize { get; }
        decimal ItemWeight { get; }
        int AveragePrice { get; }

        // Set by user
        int _localPrice;
        public int LocalPrice
        {
            get => _localPrice;
            set
            {
                _localPrice = value;

                // Format ID to match columns
                ID = Name;
                for (int i = 0; i < 35 - Name.Length - LocalPrice.ToString().Length; i++)
                {
                    ID += " ";
                }
                ID += LocalPrice.ToString();

                LocalMarkup = (decimal)LocalPrice / AveragePrice;
            }
        }

        // Calculated
        public decimal LocalMarkup { get; set; }
        public string ID { get; set; }


        // All trade goods
        static TradeGood AdvFirstAidKit { get; } = new("Advanced First Aid Kit", 6, 1, 265, 2);
        static TradeGood AdvSplintKit { get; } = new("Advanced Splint Kit", 9, 1, 311, 3);
        static TradeGood AnimalClaw { get; } = new("Animal Claw", 2, 1, 90, 1);
        static TradeGood AnimalHorn { get; } = new("Animal Horn", 3, 1, 150, 2);
        static TradeGood AnimalSkin { get; } = new("Animal Skin", 8, 1, 240, 1);
        static TradeGood AnimalTeeth { get; } = new("Animal Teeth", 1, 1, 30, 1);
        static TradeGood ArmourPlating { get; } = new("Armour Plating", 9, 4, 504, 5);
        static TradeGood AuthSkeleRepKit { get; } = new("Authetic Skeleton Repair Kit", 6, 1, 1865, 3);
        static TradeGood BasFirstAidKit { get; } = new("Basic First Aid Kit", 2, 1, 76, 1);
        static TradeGood Bloodrum { get; } = new("Bloodrum", 9, 1, 1328, 1);
        static TradeGood Book { get; } = new("Book", 4, 4, 300, 1);
        static TradeGood Bread { get; } = new("Bread", 6, 1, 488, 0.5m);
        static TradeGood BuildingMat { get; } = new("Building Material", 18, 1, 108, 2);
        static TradeGood Cactus { get; } = new("Cactus", 3, 1, 15, 1);
        static TradeGood CactusRum { get; } = new("Cactus Rum", 12, 1, 520, 1);
        static TradeGood Capacitor { get; } = new("Capacitor", 6, 1, 90, 1);
        static TradeGood CeramicBowl { get; } = new("Ceramic Bowl", 4, 1, 180, 1);
        static TradeGood ChainmailSheets { get; } = new("Chainmail Scheets", 9, 1, 2527, 6);
        static TradeGood Chewsticks { get; } = new("Chewsticks", 6, 1, 152, 0.5m);
        static TradeGood CookedVegetables { get; } = new("Cooked Vegetables", 9, 1, 318, 1);
        static TradeGood Copper { get; } = new("Copper", 6, 1, 180, 4);
        static TradeGood CopperAlloy { get; } = new("Copper Alloy Plates", 12, 1, 608, 2);
        static TradeGood Cotton { get; } = new("Cotton", 9, 1, 75, 2);
        static TradeGood CpuUnit { get; } = new("CPU Unit", 9, 1, 6000, 1);
        static TradeGood CrossbowParts { get; } = new("Crossbow Parts", 6, 1, 408, 4);
        static TradeGood DriedFish { get; } = new("Dried Fish", 3, 1, 360, 1);
        static TradeGood DriedMeat { get; } = new("Dried Meat", 4, 1, 78, 1);
        static TradeGood Dustwich { get; } = new("Dustwich", 6, 1, 730, 1);
        static TradeGood Cup { get; } = new("Cup", 4, 1, 6, 1);
        static TradeGood ElectricalComponents { get; } = new("Electrical Components", 6, 1, 216, 1);
        static TradeGood EngineeringResearch { get; } = new("Engineering Research", 4, 4, 8000, 0);
        static TradeGood Fabrics { get; } = new("Fabrics", 6, 1, 63, 1);
        static TradeGood Foodcube { get; } = new("Foodcube", 6, 1, 874, 1);
        static TradeGood Fuel { get; } = new("Fuel", 9, 1, 192, 1);
        static TradeGood Gears { get; } = new("Gears", 9, 1, 162, 1);
        static TradeGood GeneratorCore { get; } = new("Generator Core", 6, 1, 2755, 20);
        static TradeGood Gohan { get; } = new("Gohan", 9, 1, 444, 1);
        static TradeGood GrandFish { get; } = new("Grand Fish", 6, 1, 360, 1);
        static TradeGood Greenfruit { get; } = new("Greenfruit", 9, 1, 30, 0.5m);
        static TradeGood Grog { get; } = new("Grog", 6, 1, 1155, 1);
        static TradeGood Hacksaw { get; } = new("Hacksaw", 8, 1, 72, 1);
        static TradeGood Hashish { get; } = new("Hashish", 6, 1, 144, 1);
        static TradeGood Hemp { get; } = new("Hemp", 16, 1, 19, 2);
        static TradeGood Hinge { get; } = new("Hinge", 6, 1, 81, 1);
        static TradeGood IronPlates { get; } = new("Iron Plates", 12, 1, 135, 2);
        static TradeGood LanternOfRadiance { get; } = new("Lantern of Radiance", 2, 1, 90, 4);
        static TradeGood Leather { get; } = new("Leather", 6, 4, 156, 1);
        static TradeGood LuxuryGoods { get; } = new("Luxury Goods", 6, 1, 240, 1);
        static TradeGood Meatwrap { get; } = new("Meatwrap", 6, 1, 658, 1);
        static TradeGood MedicalSupplies { get; } = new("Medical Supplies", 12, 1, 120, 1);
        static TradeGood Motor { get; } = new("Motor", 12, 1, 421, 1.5m);
        static TradeGood PearlCup { get; } = new("Pearl Cup", 4, 1, 240, 1);
        static TradeGood PearlSwordHolder { get; } = new("Pearl Sword Holder", 12, 1, 300, 1);
        static TradeGood PearlUrn { get; } = new("Pearl Urn", 4, 1, 600, 1);
        static TradeGood PearlVase { get; } = new("Pearl Vase", 12, 1, 450, 1);
        static TradeGood PowerCore { get; } = new("Power Core", 12, 1, 3000, 1);
        static TradeGood Press { get; } = new("Press", 6, 1, 324, 1);
        static TradeGood RationPack { get; } = new("Ration Pack", 4, 1, 1143, 1);
        static TradeGood RawIron { get; } = new("Raw Iron", 6, 1, 90, 9);
        static TradeGood RawMeat { get; } = new("Raw Meat", 4, 1, 60, 1);
        static TradeGood RawStone { get; } = new("Raw Stone", 24, 1, 36, 4);
        static TradeGood RiceBowl { get; } = new("Rice Bowl", 9, 1, 186, 1);
        static TradeGood Riceweed { get; } = new("Riceweed", 6, 1, 13, 0.5m);
        static TradeGood RoboticsComp { get; } = new("Robotics Components", 8, 1, 2838, 2);
        static TradeGood Sake { get; } = new("Sake", 6, 1, 428, 1);
        static TradeGood SimpleRug { get; } = new("Simple Rug", 16, 1, 180, 5);
        static TradeGood SkeletonRepKit { get; } = new("Skeleton Repair Kit", 6, 1, 4341, 2);
        static TradeGood SleepingBag { get; } = new("Sleeping Bag", 15, 1, 605, 5);
        static TradeGood SplintKit { get; } = new("Splint Kit", 4, 1, 212, 2);
        static TradeGood SpringSteel { get; } = new("Spring Steel", 4, 1, 259, 3);
        static TradeGood StdFirstAidKit { get; } = new("Standard First Aid Kit", 4, 1, 147, 2);
        static TradeGood SteelBars { get; } = new("Steel Bars", 6, 1, 648, 4);
        static TradeGood Strawflour { get; } = new("Strawflour", 20, 1, 400, 4);
        static TradeGood HolyFlame { get; } = new("The Holy Flame", 9, 1, 90, 1);
        static TradeGood Thinfish { get; } = new("Thinfish", 3, 1, 360, 1);
        static TradeGood Tools { get; } = new("Tools", 6, 1, 240, 3);
        static TradeGood Water { get; } = new("Water", 25, 1, 25, 10);
        static TradeGood WaterJug { get; } = new("Water Jug", 6, 1, 30, 1);
        static TradeGood Wheatstraw { get; } = new("Wheatstraw", 25, 1, 40, 1);
        static TradeGood Wrench { get; } = new("Wrench", 6, 1, 90, 1);

        // List all items for reference
        public static TradeGood[] AllItems { get; } =
       {
            AdvFirstAidKit,
            AdvSplintKit,
            AnimalClaw,
            AnimalHorn,
            AnimalSkin,
            AnimalTeeth,
            ArmourPlating,
            AuthSkeleRepKit,
            BasFirstAidKit,
            Bloodrum,
            Book,
            Bread,
            BuildingMat,
            Cactus,
            CactusRum,
            Capacitor,
            CeramicBowl,
            ChainmailSheets,
            Chewsticks,
            CookedVegetables,
            Copper,
            CopperAlloy,
            Cotton,
            CpuUnit,
            CrossbowParts,
            DriedFish,
            DriedMeat,
            Dustwich,
            Cup,
            ElectricalComponents,
            EngineeringResearch,
            Fabrics,
            Foodcube,
            Fuel,
            Gears,
            GeneratorCore,
            Gohan,
            GrandFish,
            Greenfruit,
            Grog,
            Hacksaw,
            Hashish,
            Hemp,
            Hinge,
            IronPlates,
            LanternOfRadiance,
            Leather,
            LuxuryGoods,
            Meatwrap,
            MedicalSupplies,
            Motor,
            PearlCup,
            PearlSwordHolder,
            PearlUrn,
            PearlVase,
            PowerCore,
            Press,
            RationPack,
            RawIron,
            RawMeat,
            RawStone,
            RiceBowl,
            Riceweed,
            RoboticsComp,
            Sake,
            SimpleRug,
            SkeletonRepKit,
            SleepingBag,
            SplintKit,
            SpringSteel,
            StdFirstAidKit,
            SteelBars,
            Strawflour,
            HolyFlame,
            Thinfish,
            Tools,
            Water,
            WaterJug,
            Wheatstraw,
            Wrench
        };
    }
}
