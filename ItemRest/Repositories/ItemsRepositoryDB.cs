namespace ItemRest.Repositories
{
    public class ItemsRepositoryDB : IItemsRepository
    {
        private ItemContext _context;
        public ItemsRepositoryDB(ItemContext context)
        {
            _context = context;
        }
        public List<Item> GetAll()
        {
            return _context.Items.ToList();
        }
        public Item? GetById(int id)
        {
            return _context.Items.Find(id);
        }
        public Item Add(Item newItem)
        {
            newItem.Validate();
            newItem.Id = 0;
            _context.Items.Add(newItem);
            _context.SaveChanges();
            return newItem;
        }
        public Item? Delete(int id)
        {
            Item? deleted = GetById(id);
            if (deleted == null) return null;
            _context.Items.Remove(deleted);
            _context.SaveChanges();

            return deleted;
        }
        public Item? Update(int id, Item newData)
        {
            Item? updated = GetById(id);
            if (updated == null) return null;
            newData.Validate();
            updated.Price = newData.Price;
            updated.Name = newData.Name;
            _context.SaveChanges();
            return updated;
        }

        public List<Item> GetAllSort(string name = null, string sortBy = null)
        {
            throw new NotImplementedException();
        }
    }
}
