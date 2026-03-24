using Authentication_Authorization_V8.Models;
using Authentication_Authorization_V8.Repositories;

namespace Authentication_Authorization_V8.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repo;

        public OrderService(IOrderRepository repo)
        {
            this.repo = repo;
        }

        public int PlaceOrder(Order order)
        {
            return repo.PlaceOrder(order);
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return repo.GetOrdersByUser(userId);
        }

        public Order GetOrderById(int id)
        {
            return repo.GetOrderById(id);
        }

        public int DeleteOrder(int id)
        {
            return repo.DeleteOrder(id);
        }
    }
}
