using Authentication_Authorization_V8.Models;
using Authentication_Authorization_V8.Repositories;

namespace Authentication_Authorization_V8.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository repo;

        public OrderDetailsService(IOrderDetailsRepository repo)
        {
            this.repo = repo;
        }

        public int AddOrderDetails(OrderDetails orderDetails)
        {
            return repo.AddOrderDetails(orderDetails);
        }

        public List<OrderDetails> GetOrderDetails(int orderId)
        {
            return repo.GetOrderDetails(orderId);
        }

        public int DeleteOrderDetails(int id)
        {
            return repo.DeleteOrderDetails(id);
        }
    }
}
