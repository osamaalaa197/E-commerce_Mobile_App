using E_commerce.Database;
using E_commerce.models;

namespace E_commerce.Repo.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly E_commerceDataBase _context;

        public ProductRepository(E_commerceDataBase e_CommerceDataBase)
        {
            _context = e_CommerceDataBase;
        }
        public bool CreateProduct(Products product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return true;
             
        }

        public bool DeleteProduct(int id)
        {
            var products = GetProductByID(id);
            if (products == null)
            {
                return false;
            }
            
                _context.Remove(products);
                _context.SaveChanges();
                return true;

        }

        public IEnumerable<Products> GetAll()
        {
            return _context.Products;
        }

        public Products GetProductByID(int id)
        {
            var producct = _context.Products.FirstOrDefault(p => p.Id == id);
            return producct;
        }

        //public Products SearchProductByName(Func<Products, bool> predicate)
        //{
        //    return _context.Products.FirstOrDefault(predicate);

        //}

        public bool Update(Products product)
        {
            _context.Set<Products>().Update(product);
            _context.SaveChanges();
            return true;
        }

        public List<Products> SearchProductByName(string title)
        {
            var products = _context.Products.Where(p => p.Title == title).ToList();
            return products;
        }


    }
}
