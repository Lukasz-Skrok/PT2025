using DataLayer;

public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public List<Product> LoadProducts() => _repo.GetAll();
    public void Add(Product p) => _repo.Add(p);
    public void Update(Product p) => _repo.Update(p);
    public void Delete(int id) => _repo.Delete(id);
}
