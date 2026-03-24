using Authentication_Authorization_V8.Data;
using Authentication_Authorization_V8.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Authorization_V8.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext db;

        public OrderDetailsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddOrderDetails(OrderDetails od)
        {
            od.IsActive = 1;
            db.OrderDetails.Add(od);
            return db.SaveChanges();
        }

        public List<OrderDetails> GetOrderDetails(int orderId)
        {
            return db.OrderDetails
                     .Include(o => o.Book)   // 🔥 THIS LINE FIXES YOUR ISSUE
                     .Where(o => o.OrderId == orderId && o.IsActive == 1)
                     .ToList();
        }

        public int DeleteOrderDetails(int id)
        {
            int result = 0;

            var od = db.OrderDetails.Where(x => x.OrderDetailsId == id).SingleOrDefault();

            if (od != null)
            {
                od.IsActive = 0;
                result = db.SaveChanges();
            }

            return result;
        }
    }
}
