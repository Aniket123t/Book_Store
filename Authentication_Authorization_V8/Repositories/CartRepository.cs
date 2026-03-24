using Authentication_Authorization_V8.Data;
using Authentication_Authorization_V8.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Authorization_V8.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext db;

        public CartRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddToCart(Cart cart)
        {
            cart.IsActive = 1;
            db.Carts.Add(cart);
            return db.SaveChanges();
        }

        public List<Cart> GetCartByUser(string userId)
        {
            return db.Carts
                .Include(c => c.Book)   // 🔥 VERY IMPORTANT
                .Where(c => c.UserId == userId && c.IsActive == 1)
                .ToList();
        }

        public int UpdateCart(Cart cart)
        {
            int result = 0;

            var c = db.Carts.Where(x => x.CartId == cart.CartId).SingleOrDefault();

            if (c != null)
            {
                c.Quantity = cart.Quantity;
                result = db.SaveChanges();
            }

            return result;
        }

        public int RemoveFromCart(int id)
        {
            int result = 0;

            var c = db.Carts.Where(x => x.CartId == id).SingleOrDefault();

            if (c != null)
            {
                c.IsActive = 0;
                result = db.SaveChanges();
            }

            return result;
        }

        public int ClearCart(string userId)
        {
            int result = 0;

            var items = db.Carts.Where(x => x.UserId == userId).ToList();

            foreach (var item in items)
            {
                item.IsActive = 0;
            }

            result = db.SaveChanges();
            return result;
        }
    }
}
