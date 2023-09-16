using E_commerce.Controllers.Cart;
using E_commerce.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Database
{
    public class E_commerceDataBase: IdentityDbContext<Users>
    {
        public DbSet<Products> Products { get; set; }

        public DbSet<Users> user { get; set; }
        public DbSet<IsLike> isLikes { get; set; }
        public DbSet<Cart> Cartitem { get; set; }

        public E_commerceDataBase(DbContextOptions<E_commerceDataBase> options):base(options)
        {

        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<CartShopping>()
        //        .HasKey(c => c.Id);

        //    modelBuilder.Entity<CartItem>()
        //        .HasKey(ci => ci.Id);

        //    // Define the relationship between Cart and CartItem
        //    modelBuilder.Entity<CartItem>()
        //        .HasOne(ci => ci.Cart)
        //        .WithMany(c => c.CartItems)
        //        .HasForeignKey(ci => ci.CartId);
        //}

    }
}
