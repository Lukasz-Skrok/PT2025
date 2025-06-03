using System.Collections.Generic;
using DataLayer;

public interface IProductRepository
{
    List<Product> GetAll();
    void Add(Product p);
    void Update(Product p);
    void Delete(int id);
}
