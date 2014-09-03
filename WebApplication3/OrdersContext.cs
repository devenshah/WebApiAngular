using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3
{
    public class OrdersContext : DbContext
    {
        public OrdersContext()
            :base("OrdersContext")
        {
            Database.SetInitializer<OrdersContext>(new OrdersDBInitializer());

        }

        public DbSet<Order> Orders { get; set; }
    }
}