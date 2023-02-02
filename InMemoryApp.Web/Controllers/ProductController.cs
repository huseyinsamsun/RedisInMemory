using Microsoft.AspNetCore.Mvc;
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
             
          
            

            return Ok(cache);
        }
        [HttpGet(Name = "cache")]
        public IActionResult Show()
        {
           
            var getCache = _memoryCache.Get<string>("zaman");
            _memoryCache.TryGetValue("callback", out string callback);
            if(getCache!=null)
            {
                return Ok(getCache);
            }
            return Ok(callback);
           
        }
    }
}
