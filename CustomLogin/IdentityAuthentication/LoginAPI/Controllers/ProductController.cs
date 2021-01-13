using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        [HttpGet]
        public Product GetProduct()
        {
            return new Product() { Id = 1, Price = 10 };
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
    }
}
