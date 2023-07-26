using E_commerce.models;

namespace E_commerce.Repo.Product
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetAll();
        Products GetProductByID (int id);
        bool DeleteProduct(int id);
        bool Update(Products product);  
        bool CreateProduct(Products product);
        List<Products> SearchProductByName(string name);
        //TEntity SearchProductByName(Func<TEntity, bool> predicate);


    }
}
