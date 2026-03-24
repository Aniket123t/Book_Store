using Authentication_Authorization_V8.Data;
using Authentication_Authorization_V8.Models;

namespace Authentication_Authorization_V8.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext db;

        public OrderRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int PlaceOrder(Order order)
        {
            order.IsActive = 1;
            db.Orders.Add(order);
            db.SaveChanges();   // 🔥 IMPORTANT

            return order.OrderId;
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            var orders = (from o in db.Orders
                          where o.UserId == userId && o.IsActive == 1
                          select o).ToList();

            return orders;
        }

        public Order GetOrderById(int id)
        {
            return db.Orders.Where(x => x.OrderId == id).SingleOrDefault();
        }

        public int DeleteOrder(int id)
        {
            int result = 0;

            var o = db.Orders.Where(x => x.OrderId == id).SingleOrDefault();

            if (o != null)
            {
                o.IsActive = 0;
                result = db.SaveChanges();
            }

            return result;
        }
    }
}
