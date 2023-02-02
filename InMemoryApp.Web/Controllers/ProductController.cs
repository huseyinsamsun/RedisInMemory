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
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            options.SlidingExpiration = TimeSpan.FromSeconds(10);
            //  options.Priority = CacheItemPriority.High; data benim için önemli 
            //options.Priority = CacheItemPriority.Normal; data benim için normal düzeyde önemli
            //  options.Priority = CacheItemPriority.Low;çokta önemli değil herhangi bir durumda sil
             //options.Priority = CacheItemPriority.NeverRemove; ram dolsa bile asla silme

            var cache =   _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);
             
          
            

            return Ok(cache);
        }
        [HttpGet(Name = "cache")]
        public IActionResult Show()
        {
           
            var getCache = _memoryCache.Get<string>("zaman");
            if(getCache!=null)
            {
                return Ok(getCache);
            }
            return Ok("cache yok");
           
        }
    }
}
