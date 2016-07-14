using System.Data;

namespace GildedRose.Console
{
    public abstract class Item
    {
        public abstract void Update();

        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        protected bool HasPassedSellByDate()
        {
            return SellIn < 0;
        }

        protected void IncreaseQuality()
        {
            if (Quality < 50)
            {
                Quality = Quality + 1;
            }
        }

        protected void DecreaseQuality()
        {
            if (Quality > 0)
            {
                Quality = Quality - 1;
            }
        }

        protected void DecreaseSellIn()
        {
            SellIn = SellIn - 1;
        }
    }

    public class AgeingItem : Item
    {
        public override void Update()
        {
            IncreaseQuality();
            DecreaseSellIn();

            if (HasPassedSellByDate())
            {
                IncreaseQuality();
            }
        }
    }

    public class AgeingItemIgnoringSellBy : Item
    {
        public override void Update()
        {
            IncreaseQuality();
            DecreaseSellIn();
        }
    }

    public class NeverDropsInQualityItem : Item
    {

        public override void Update()
        {
            DecreaseSellIn();
        }
    }

    public class DesirableEventItem : Item
    {
        public override void Update()
        {
            // Tickets are more valuable when an event is closer
            if (SellIn <= 10)
            {
                IncreaseQuality();
            }

            // They increase in value much more the closer we are to the event
            if (SellIn <= 5)
            {
                IncreaseQuality();
            }

            IncreaseQuality();
            DecreaseSellIn();

            if (HasPassedSellByDate())
            {
                Quality = 0;
            }
        }
    }

    public class LegendaryItem : Item
    {
        public override void Update()
        {
        }
    }

    public class ConjuredItem : Item
    {
        public override void Update()
        {
            DecreaseQuality();
            DecreaseQuality();
            DecreaseSellIn();
        }
    }

    public class PerishableItem : Item
    {
        public override void Update()
        {
            DecreaseQuality();
            DecreaseSellIn();

            if (HasPassedSellByDate())
            {
                DecreaseQuality();
            }
        }
    }

}