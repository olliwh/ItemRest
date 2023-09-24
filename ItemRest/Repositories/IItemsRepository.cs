namespace ItemRest.Repositories
{
    public interface IItemsRepository
    {
        Item Add(Item newItem);
        Item? Delete(int id);
        List<Item> GetAll();
        Item? GetById(int id);
        Item? Update(int id, Item newData);
        List<Item> GetAllSort(string name = null, string sortBy = null);

    }
}