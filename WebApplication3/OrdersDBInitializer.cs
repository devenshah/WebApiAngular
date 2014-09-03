using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3
{
    public class OrdersDBInitializer : CreateDatabaseIfNotExists<OrdersContext>
    {
        protected override void Seed(OrdersContext context)
        {
            context.Orders.Add(new Order { OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true });
            context.Orders.Add(new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false});
            context.Orders.Add(new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false });
            context.Orders.Add(new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false});
            context.Orders.Add(new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true});

            base.Seed(context);
        }

    }
}