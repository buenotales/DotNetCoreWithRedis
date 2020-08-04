using DotNetCoreWithRedis.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCoreWithRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly DefaultServices defaultServices;

        public DefaultController(DefaultServices defaultServices)
        {
            this.defaultServices = defaultServices;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await defaultServices.GetCache();
        }

        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            await defaultServices.SetCache(value);
        }

        [HttpPut]
        public async Task Put()
        {
            await defaultServices.RefreshCache();
        }

        [HttpDelete]
        public async Task Delete()
        {
            await defaultServices.RemoveCache();
        }
    }
}
