using static System.Runtime.InteropServices.JavaScript.JSType;//??

namespace ItemRest.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private int _nextId;
        private List<Item> _items;
        public ItemsRepository()
        {
            _nextId = 1;
            _items = new List<Item>()
            {
                new Item() {Id = _nextId++, Name = "apple", Price = 3},
                new Item() {Id = _nextId++, Name = "Sofa", Price = 3000},
                new Item() {Id = _nextId++, Name = "Tomato", Price = 4},
                new Item() {Id = _nextId++, Name = "Cola", Price = 30}
            };
        }
        public List<Item> GetAll()
        {
            return new List<Item>(_items);
        }
        public Item? GetById(int id)
        {
            Item? foundItem = _items.Find(x => x.Id == id);
            return foundItem;
        }
        public Item Add(Item newItem)
        {
            newItem.Validate();
            newItem.Id = _nextId++;
            _items.Add(newItem);
            return newItem;
        }
        public Item? Delete(int id)
        {
            Item? deletedItem = GetById(id);
            if (deletedItem != null)
            {
                _items.Remove(deletedItem);
            }
            return deletedItem;
        }
        public Item? Update(int id, Item newData)
        {
            newData.Validate();
            Item? updatedItem = GetById(id);
            if (updatedItem != null)
            {
                updatedItem.Name = newData.Name;
                updatedItem.Price = newData.Price;
            }
            return updatedItem;
        }


        //sorting:

        public List<Item> GetAllSort(string name = null, string sortBy = null)
        {
            List<Item> items = new List<Item>(_items);
            // copy constructor
            // Callers should no get a reference to the Data object, but rather get a copy

            if (name != null)
            {
                items = items.FindAll(item => item.Name.StartsWith(name));
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "id":
                        items = items.OrderBy(item => item.Id).ToList();
                        break;
                    case "name":
                        items = items.OrderBy(item => item.Name).ToList();
                        break;
                    case "priceasc":
                        items = items.OrderBy(item => item.Price).ToList();
                        break;
                    case "pricedesc":
                        items = items.OrderByDescending(item => item.Price).ToList();
                        break;
                }
            }
            return items;
        }
    }
}
