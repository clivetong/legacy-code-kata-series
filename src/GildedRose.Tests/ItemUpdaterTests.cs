using System.Collections.Generic;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    public class ItemUpdaterTests
    {
        [Test]
        public void StandardItemShouldLowerQualityAndSellInByOne()
        {
            var item = new PerishableItem { Name = "+5 Dexterity Vest", SellIn = 3, Quality = 6 };

            item.Update();

            Assert.AreEqual(5, item.Quality);
            Assert.AreEqual(2, item.SellIn);
        }

        [Test]
        public void StandardItemShouldLowerQualityTwiceAsFastWhenSellInIsNegative()
        {
            var item = new PerishableItem { Name = "+5 Dexterity Vest", SellIn = -2, Quality = 6 };

            item.Update();

            Assert.AreEqual(4, item.Quality);
            Assert.AreEqual(-3, item.SellIn);
        }

        [Test]
        public void StandardItemShouldLowerQualityTwiceAsFastWhenSellInIsZero()
        {
            var item = new PerishableItem { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 6 };

            item.Update();

            Assert.AreEqual(4, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void BackstagePassItemShouldIncreaseQualityTwiceAsFastWhenSellInLessThanElevenDays()
        {
            var item = new DesirableEventItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 6 };

            item.Update();

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(9, item.SellIn);
        }

        [Test]
        public void BackstagePassItemShouldIncreaseQualityThreeTimesAsFastWhenSellInLessThanSixDays()
        {
            var item = new DesirableEventItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 6 };

            item.Update();

            Assert.AreEqual(9, item.Quality);
            Assert.AreEqual(4, item.SellIn);
        }

        [Test]
        public void BackstagePassItemShouldHaveZeroQualityWhenSellInBelowZero()
        {
            var item = new DesirableEventItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 6 };

            item.Update();

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void AgedBrieQualityIncreasesTwiceAsFastWhenSellInIsLessThanZero()
        {
            var item = new AgeingItem { Name = "Aged Brie", SellIn = 0, Quality = 6 };

            item.Update();

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void StandardItemQualityIsNeverNegative()
        {
            var item = new PerishableItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0 };

            item.Update();

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(9, item.SellIn);
        }

        [Test]
        public void SulfurasNeverDecreasesInQualityAndNeverHasToBeSold()
        {
            var item = new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 };

            item.Update();

            Assert.AreEqual(80, item.Quality);
            Assert.AreEqual(10, item.SellIn);
        }

        [Test]
        public void AgedBrieQualityCanNeverBeMoreThanFifty()
        {
            var item = new AgeingItem { Name = "Aged Brie", SellIn = -1, Quality = 50 };

            item.Update();

            Assert.AreEqual(50, item.Quality);
            Assert.AreEqual(-2, item.SellIn);
        }

        [Test]
        public void ConjuredManaCakeQualityDecreasesTwiceAsFast()
        {
            var item = new ConjuredItem { Name = "Conjured Mana Cake", SellIn = 6, Quality = 10 };

            item.Update();

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(5, item.SellIn);
        }

        [TestCase("Merlot Red Wine")]
        [TestCase("Stilton")]
        [TestCase("Gruyere Cheese")]
        public void ItemsGetsBetterWithAge(string name)
        {
            var item = new AgeingItemIgnoringSellBy { Name = name, SellIn = 6, Quality = 10 };

            item.Update();

            Assert.AreEqual(11, item.Quality);
            Assert.AreEqual(5, item.SellIn);
        }

        [TestCase("Merlot Red Wine")]
        [TestCase("Stilton")]
        [TestCase("Gruyere Cheese")]
        public void ItemsGetsBetterWithAgeAfterSellBy(string name)
        {
            var item = new AgeingItemIgnoringSellBy { Name = name, SellIn = 1, Quality = 10 };

            item.Update();
            item.Update();

            Assert.AreEqual(12, item.Quality);
            Assert.AreEqual(-1, item.SellIn); 
        }

        [Test]
        public void CubanCigarNeverDropsInQuality()
        {
            var item = new NeverDropsInQualityItem { Name = "Cuban Cigar", SellIn = 1, Quality = 10 };

            item.Update();
            item.Update();
            item.Update();

            Assert.AreEqual(10, item.Quality);
            Assert.AreEqual(-2, item.SellIn);
        }
    }
}