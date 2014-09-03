using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            using (var repo = new OrdersRepository())
            {
                var list = repo.GetOrders();
                return Ok(list);
            }            
        }
    }
}
