using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3
{
    public class OrdersRepository : IDisposable
    {
        OrdersContext _ctx;

        public OrdersRepository()
        {
            _ctx = new OrdersContext();
        }

        public IList<Order> GetOrders()
        {
            return _ctx.Orders.ToList();
        }


        public void Dispose()
        {
            _ctx.Dispose();

        }
    }
}