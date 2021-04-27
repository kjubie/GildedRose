using System.Collections.Generic;

namespace GildedRose {
    public class GildedRose {
        private readonly IList<Item> Items;
        public GildedRose(IList<Item> Items) {
            this.Items = Items;
        }

        public void UpdateQuality_old() {
            for (var i = 0; i < Items.Count; i++) {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert") {
                    if (Items[i].Quality > 0) {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros") {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                } else {
                    if (Items[i].Quality < 50) {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert") {
                            if (Items[i].SellIn < 11) {
                                if (Items[i].Quality < 50) {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6) {
                                if (Items[i].Quality < 50) {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros") {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0) {
                    if (Items[i].Name != "Aged Brie") {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert") {
                            if (Items[i].Quality > 0) {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros") {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        } else {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    } else {
                        if (Items[i].Quality < 50) {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }

        public void UpdateQuality() {
            foreach (Item item in Items) {
                if (item.Name.Equals("Sulfuras"))
                    continue;

                item.SellIn--;

                if (item.Name.Contains("Backstage pass")) {
                    if (item.SellIn < 0) {
                        item.Quality = 0;
                    } else {

                        item.Quality = IncreaseQuality(item.Quality);

                        if (item.SellIn <= 10)
                        item.Quality = IncreaseQuality(item.Quality);

                        if (item.SellIn <= 5)
                        item.Quality = IncreaseQuality(item.Quality);
                    }

                    continue;
                }

                if (item.Name.Equals("Aged Brie")) {
                    item.Quality = IncreaseQuality(item.Quality);
                    continue;
                }

                item.Quality = ReduceQuality(item.Quality, item.SellIn);

                if (item.Name.Contains("Conjured"))
                    item.Quality = ReduceQuality(item.Quality, item.SellIn);

                if (item.Quality == 0) {
                    item.Name = "fixme";
                }
            }
        }

        private int IncreaseQuality(int quality) {
            quality++;

            if (quality > 50)
                quality = 50;

            return quality;
        }

        private int ReduceQuality(int quality, int sellIn) {
            quality--;

            if (sellIn < 0)
                quality--;

            if (quality < 0)
                quality = 0;

            return quality;
        }
    }

    public class Item {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
