using Authentication_Authorization_V8.Models;

namespace Authentication_Authorization_V8.Repositories
{
    public interface IOrderDetailsRepository
    {
        List<OrderDetails> GetOrderDetails(int orderId);

        int AddOrderDetails(OrderDetails orderDetails);

        int DeleteOrderDetails(int id);
    }
}
