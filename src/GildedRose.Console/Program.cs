using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    internal class Program
    {
        private IList<Item> Items;

        internal static void Main(string[] args)
        {
            UpdateAndPrintItems();

            System.Console.ReadKey();
        }

        internal static void UpdateAndPrintItems()
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            app.UpdateQuality();

            PrintItems(app);
        }

        private static void PrintItems(Program app)
        {
            foreach (var item in app.Items)
            {
                System.Console.WriteLine("{0}|{1}|{2}", item.Name, item.Quality, item.SellIn);
            }
        }

        public void UpdateQuality()
        {
            UpdateQuality(Items.ToArray());
        }

        public static void UpdateQuality(Item[] items)
        {
            foreach (var currentItem in items)
            {
                switch (currentItem.Name)
                {
                    case ("Aged Brie"):
                        AdjustQualityIfLessThan50(currentItem, QualityAdjustment(currentItem));
                        DecrementSellinAndDoIfNegative(currentItem, () => AdjustQualityIfLessThan50(currentItem));
                        break;

                    case ("Backstage passes to a TAFKAL80ETC concert"):
                        AdjustQualityIfLessThan50(currentItem, QualityAdjustment(currentItem));
                        DecrementSellinAndDoIfNegative(currentItem, () => { currentItem.Quality = 0; });
                        break;

                    case "Sulfuras, Hand of Ragnaros":
                        break;

                    default:
                        DecrementQualityIfNonZero(currentItem);
                        DecrementSellinAndDoIfNegative(currentItem, () => DecrementQualityIfNonZero(currentItem));
                        break;
                }
            }
        }

        static void DecrementSellinAndDoIfNegative(Item currentItem, Action todo)
        {
            currentItem.SellIn--;

            if (currentItem.SellIn < 0)
            {
                todo();
            }
        }

        static int QualityAdjustment(Item currentItem)
        {
            switch (currentItem.Name)
            {
                case "Aged Brie":
                    return 1;

                case "Backstage passes to a TAFKAL80ETC concert":
                    if (currentItem.SellIn <= 5)
                    {
                        return 3;
                    }
                    if (currentItem.SellIn <= 10)
                    {
                        return 2;
                    }

                    return 1;

                default:
                    throw new Exception("Unknown case");
            }
        }

        private static void DecrementQualityIfNonZero(Item currentItem)
        {
            if (currentItem.Quality > 0)
            {
                currentItem.Quality = currentItem.Quality - 1;
            }
        }

        private static void AdjustQualityIfLessThan50(Item currentItem, int amount = 1)
        {
            if (currentItem.Quality < 50)
            {
                currentItem.Quality = currentItem.Quality + amount;
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}