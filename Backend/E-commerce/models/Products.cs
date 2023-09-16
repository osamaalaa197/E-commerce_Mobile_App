using E_commerce.Controllers.Cart;

namespace E_commerce.models
{
    public class Products
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Supplier { get; set; }
        public string Descirption { get; set; }
        public string Product_Location { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }



        public ICollection<IsLike> Islike { get; set; }

        public ICollection<Cart> Cartitem { get; set; }

        //public ICollection<CartProduct> CartProducts { get; set; }



        //public ICollection<CartShopping> CartShopping { get; set; }

    }
}
