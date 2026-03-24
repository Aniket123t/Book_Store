using Authentication_Authorization_V8.Models;
using Authentication_Authorization_V8.Repositories;

namespace Authentication_Authorization_V8.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository repo;

        public CartService(ICartRepository repo)
        {
            this.repo = repo;
        }

        public int AddToCart(Cart cart)
        {
            return repo.AddToCart(cart);
        }

        public List<Cart> GetCartByUser(string userId)
        {
            return repo.GetCartByUser(userId);
        }

        public int UpdateCart(Cart cart)
        {
            return repo.UpdateCart(cart);
        }

        public int RemoveFromCart(int id)
        {
            return repo.RemoveFromCart(id);
        }

        public int ClearCart(string userId)
        {
            return repo.ClearCart(userId);
        }
    }
}
