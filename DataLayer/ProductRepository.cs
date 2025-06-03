using DataLayer;
using System.Collections.Generic;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public List<Product> GetAll()
    {
        return _products;
    }

    public void Add(Product p)
    {
        _products.Add(p);
    }

    public void Update(Product p)
    {
        var item = _products.FirstOrDefault(x => x.Id == p.Id);
        if (item != null)
        {
            item.Name = p.Name;
            item.Price = p.Price;
            item.Quantity = p.Quantity;
        }
    }

    public void Delete(int id)
    {
        var item = _products.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _products.Remove(item);
        }
    }
}
