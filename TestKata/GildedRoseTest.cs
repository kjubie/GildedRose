using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace GildedRose {
	[TestFixture]
	public class GildedRoseTest {
		[Test]
		public void foo() {
			IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual("fixme", Items[0].Name);
		}

		[Test]
		public void TestLowerValues(){
			IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 5, Quality = 5 }, new Item { Name = "bar", SellIn = 8, Quality = 8 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(4, Items[0].SellIn);
			Assert.AreEqual(4, Items[0].Quality);

			Assert.AreEqual(7, Items[1].SellIn);
			Assert.AreEqual(7, Items[1].Quality);
		}

		[Test]
		public void TestSellByDateDoubleQualityLoss() {
			IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 8 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(6, Items[0].Quality);
		}

		[Test]
		public void TestNotNegativeQuality() {
			IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(0, Items[0].Quality);
		}

		[Test]
		public void TestAgedBrieQualityIncrease() {
			IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(11, Items[0].Quality);
		}

		[Test]
		public void TestQualityCap() {
			IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 60 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(50, Items[0].Quality);
		}

		[Test]
		public void TestSulfuras() {
			IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras", SellIn = 10, Quality = 80 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(10, Items[0].SellIn);
			Assert.AreEqual(80, Items[0].Quality);
		}

		[Test]
		public void TestBackstagePasses() {
			IList<Item> Items = new List<Item> { new Item { Name = "Backstage pass 01", SellIn = 20, Quality = 10 }, new Item { Name = "Backstage pass 02", SellIn = 10, Quality = 10 }, new Item { Name = "Backstage pass 03", SellIn = 5, Quality = 10 }, new Item { Name = "Backstage pass 04", SellIn = -1, Quality = 10 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(11, Items[0].Quality);
			Assert.AreEqual(12, Items[1].Quality);
			Assert.AreEqual(13, Items[2].Quality);
			Assert.AreEqual(0, Items[3].Quality);
		}

		[Test]
		public void TestConjured() {
			IList<Item> Items = new List<Item> { new Item { Name = "Conjured cake 01", SellIn = 10, Quality = 10 }, new Item { Name = "Conjured cake 01", SellIn = -1, Quality = 10 } };
			GildedRose app = new GildedRose(Items);
			app.UpdateQuality();

			Assert.AreEqual(8, Items[0].Quality);
			Assert.AreEqual(6, Items[1].Quality);
		}
	}
}

