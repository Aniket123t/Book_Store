using Authentication_Authorization_V8.Models;

namespace Authentication_Authorization_V8.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetOrdersByUser(string userId);

        Order GetOrderById(int id);

        int PlaceOrder(Order order);

        int DeleteOrder(int id);
    }
}
