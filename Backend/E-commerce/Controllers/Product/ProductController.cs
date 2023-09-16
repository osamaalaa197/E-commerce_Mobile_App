using E_commerce.models;
using E_commerce.Repo.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public ActionResult<List<Products>> GetAllProduct()
        {
            return _productRepository.GetAll().ToList();
        }

        [Route("id")]
        [HttpGet]
        public ActionResult<Products> GetById(int id)
        {
            var product=_productRepository.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product); 
        }
        [HttpPost]
        public ActionResult CreateProduct(Products products)
        {
            _productRepository.CreateProduct(products);
            return Ok(products);
        }
        [HttpPut]
        public ActionResult UpdateProduct(int id,Products newProduct)
        {
            var oldProduct = _productRepository.GetProductByID(id);
            if (oldProduct == null)
            {
                return NotFound();
            }
            oldProduct.Title = newProduct.Title;
            oldProduct.Supplier=newProduct.Supplier;
            oldProduct.Price=newProduct.Price;
            oldProduct.Product_Location=newProduct.Product_Location;
            oldProduct.ImageUrl=newProduct.ImageUrl;
            oldProduct.Descirption = newProduct.Descirption;
            var isUpdate=_productRepository.Update(oldProduct);
            if (isUpdate)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("id")]
        public ActionResult deleteProduct(int id)
        {
            var isdelete=_productRepository.DeleteProduct(id);
            if (isdelete)
            {
                return NoContent();

            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet]
        [Route("title")]
        public ActionResult<List<Products>> SearchProduct(string title)
        {
           var product= _productRepository.SearchProductByName(title);
            return Ok(product);
        }

    }
}
