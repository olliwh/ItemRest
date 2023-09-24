using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemRest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRest.Repositories.Tests
{
    [TestClass()]
    public class ItemsRepositoryTests
    {
        ItemsRepository _repository = new ItemsRepository();

        [TestMethod()]
        public void GetAllTest()
        {
            List<Item> items = _repository.GetAll();
            Assert.IsNotNull(items);
            Assert.AreEqual(4, items.Count);
            foreach (var item in items)
            {
                int foundIds = items.FindAll(x => x.Id == item.Id).Count;
                if (foundIds > 1)
                {
                    Assert.Fail($"ID: {item.Id} exists {foundIds} times in the list");
                }
            }
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Item? found = _repository.GetById(1);
            Assert.IsNotNull(found);
            Item? notFound = _repository.GetById(7);
            Assert.IsNull(notFound);

            Assert.AreEqual(1, found.Id);
            Assert.AreEqual("apple", found.Name);
            Assert.AreEqual(3, found.Price);
        }

        [TestMethod()]
        public void AddTest()
        {
            List<Item> beforeAdd = _repository.GetAll();
            int id = 1;
            string name = "Rum";
            int price = 120;

            Item newItem = new Item() { Id = id, Name = name, Price = price };

            Item added = _repository.Add(newItem);
            Assert.IsNotNull(added);
            Assert.AreEqual(typeof(Item), added.GetType());
            Assert.AreEqual(5, added.Id);
            Assert.AreEqual("Rum", added.Name);
            Assert.AreEqual(120, added.Price);
            Assert.AreEqual(beforeAdd.Count + 1, _repository.GetAll().Count);

        }

        [TestMethod()]
        public void DeleteTest()
        {
            List<Item> beforeDelete = _repository.GetAll();
            Item? deleted = _repository.Delete(1);
            Item? failedDelete = _repository.Delete(7);
            Assert.IsNotNull(deleted);
            Assert.IsNull(_repository.GetById(1));
            Assert.IsNull(failedDelete);

            Assert.AreEqual(beforeDelete.Count - 1, _repository.GetAll().Count);

        }

        [TestMethod()]
        public void UpdateTest()
        {
            int id = 1;
            string name = "Rum";
            int price = 120;

            Item newData = new Item() { Id = id, Name = name, Price = price };
            Item? updated = _repository.Update(id, newData);
            Item? failedUpdate = _repository.Update(9, newData);
            Assert.IsNotNull(updated);
            Assert.IsNull(failedUpdate);

            Assert.AreEqual(id, updated.Id);
            Assert.AreEqual(name, updated.Name);
            Assert.AreEqual(price, updated.Price);
            
        }
    }
}