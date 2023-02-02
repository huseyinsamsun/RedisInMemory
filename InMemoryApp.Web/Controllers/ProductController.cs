using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpPost(Name = "setcache")]
        public IActionResult Index()
        {


                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            //options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback",$"{key}=> {value}=>sebep:{reason}");
            });
            options.Priority = CacheItemPriority.High;

            var cache =   _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);


            Product product = new Product { Id = 1, Name = "Kalem", Price = 200 };
            _memoryCache.Set<Product>("product:1", product);

            

            return Ok(cache);
        }
        [HttpGet(Name = "cache")]
        public IActionResult Show()
        {

            //var getCache = _memoryCache.Get<string>("zaman");
            //_memoryCache.TryGetValue("callback", out string callback);
            //if(getCache!=null)
            //{
            //    return Ok(getCache);
            //}
            var product = _memoryCache.Get<Product>("product:1");
            return Ok(product);
           
        }
    }
}
