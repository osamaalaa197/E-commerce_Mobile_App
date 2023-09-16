using E_commerce.Database;
using E_commerce.DTO;
using E_commerce.DTO.IslikeDTO;
using E_commerce.models;
using E_commerce.Repo.Product;
using E_commerce.Repo.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslikeController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly E_commerceDataBase _context;
        private readonly IProductRepository _productRepository;

        public IslikeController(IUserRepository userRepository, E_commerceDataBase e_CommerceDataBase, IProductRepository productRepository)
        {
            _userRepository = userRepository;
                _context = e_CommerceDataBase;
            _productRepository = productRepository;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<UserWithLikedProductsDTO> GetAlllike(string id)
        {
            var userid = id;
            var UserWithLikedProducts = _context.user.Where(p=>p.Id==userid).
                Select(user => new UserWithLikedProductsDTO
            {
                UserId = userid,
                UserName = user.UserName,
                LikedProducts = _context.isLikes.Where(p => p.UsersId == user.Id).SelectMany(like => like.Products).ToList()
            }).ToList();
            return (Ok(UserWithLikedProducts));
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddTOLikes(int Productid, string id)
        {
            var userid = id;
            var product = _productRepository.GetProductByID(Productid);
            var isfound =_context.isLikes.Include(p=>p.Products).FirstOrDefault(p=>p.UsersId==userid);
            if(isfound==null)
            {
                var islike = new IsLike
                {
                    
                    UsersId = userid,
                    Products = new List<Products>
                    {
                        product
                    }
                };
                _context.isLikes.Add(islike);
                _context.SaveChanges();
                return Ok("Producted Add");
            }
            else
            {
                isfound.Products.Add(product);
                _context.SaveChanges();

            }
            return Ok("Producted Add");
        }
    }
}
