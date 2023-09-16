using E_commerce.Database;
using E_commerce.DTO.CartDTO;
using E_commerce.models;
using E_commerce.Repo.Product;
using E_commerce.Repo.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly E_commerceDataBase _context;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CartController(E_commerceDataBase e_CommerceData, IProductRepository productRepository,IUserRepository userRepository)
        {
            _context = e_CommerceData;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetCartForUser(string userid)
        {
            var user = _userRepository.GetUserById(userid);
            if (user == null)
            {
                return BadRequest("User not valid");
            }
            var userwithCartProduct = _context.user.Where(p => p.Id == userid).Select(user => new UserWithCartProductsDTO
            {
                userid = userid,
                UserName = user.UserName,
                CartProdcts = _context.Cartitem.Where(p => p.UsersId == userid).SelectMany(p => p.Product).ToList()

            });
            return Ok(userwithCartProduct);

        }
        [Authorize]
        [HttpPost]
        public ActionResult AddProductTOCart(int productid,string userid)
        {
            var user = _userRepository.GetUserById(userid);
            if (user == null)
            {
                return BadRequest("User not valid");
            }
            var product = _productRepository.GetProductByID(productid);
            if(product == null)
            {
                return BadRequest("Product not valid");

            }

            var isfound = _context.Cartitem.Include(p => p.Product).FirstOrDefault(p => p.UsersId == userid);
            if (isfound == null)
            {
                var newCart = new Cart
                {
                    UsersId = userid,
                    Product = new List<Products> { product }
                };
                _context.Cartitem.Add(newCart);
                _context.SaveChanges();
                return (NoContent());
            }
            else
            {
                isfound.Product.Add(product);
                _context.SaveChanges();
                return (NoContent());

            }
        }
        [Authorize]
        [HttpDelete]
        public ActionResult RemoveFromCart(string userId, int ProductID)
        {
            var product = _productRepository.GetProductByID(ProductID);
            var cart = _context.Cartitem.Include(p => p.Product).FirstOrDefault(p => p.UsersId == userId);
            cart.Product.Remove(product);
            _context.SaveChanges();
            return (NoContent());
        }

    }
}
