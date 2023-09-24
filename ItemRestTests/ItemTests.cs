using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRest.Tests
{
    [TestClass()]
    public class ItemTests
    {
        Item item = new Item() { Id = 1, Name = "Rum", Price = 120 };
        Item itemEqual = new Item() { Id = 1, Name = "Rum", Price = 120 };
        Item notEqual = new Item() { Id = 2, Name = "Rum", Price = 120 };

        [TestMethod()]
        public void ValidateNameTest()
        {
            item.ValidateName();
            item.Name = null;
            Assert.ThrowsException<ArgumentNullException>(() => item.ValidateName());
            item.Name = "a";
            Assert.ThrowsException<ArgumentException>(() => item.ValidateName());
        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            item.ValidatePrice();
            item.Price = -1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => item.ValidatePrice());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            item.Validate();
            item.Price = -1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => item.Validate());
            item.Name = null;
            Assert.ThrowsException<ArgumentNullException>(() => item.Validate());
            item.Name = "a";
            Assert.ThrowsException<ArgumentException>(() => item.Validate());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string expected = "1, Rum: 120kr.";
            Assert.AreEqual(expected, item.ToString());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.AreEqual(itemEqual, item);
            Assert.IsTrue(item.Equals(itemEqual));
            Assert.IsFalse(item.Equals(notEqual));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.AreEqual(item.GetHashCode(), itemEqual.GetHashCode());
            Assert.AreNotEqual(item.GetHashCode(), notEqual.GetHashCode());
        }
    }
}