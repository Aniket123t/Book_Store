using Authentication_Authorization_V8.Models;

namespace Authentication_Authorization_V8.Repositories
{
    public interface ICartRepository
    {
        List<Cart> GetCartByUser(string userId);

        int AddToCart(Cart cart);

        int UpdateCart(Cart cart);

        int RemoveFromCart(int cartId);

        int ClearCart(string userId);
    }
}
