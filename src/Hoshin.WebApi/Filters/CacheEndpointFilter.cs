using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hoshin.WebApi.Filters
{
    public class CacheEndpointFilter : Attribute, IAsyncResourceFilter
        {
        private readonly IDistributedCache _cache;
        public CacheEndpointFilter(IDistributedCache cache)
        {
            _cache = cache;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
            if(context.Result is OkObjectResult)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                string keyCache = context.HttpContext.Request.Path.Value + "-" + context.HttpContext.User.FindFirst("id").Value;
                string serializedResponse = JsonConvert.SerializeObject((context.Result as OkObjectResult).Value, settings);

                DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
                opt.AbsoluteExpirationRelativeToNow = System.TimeSpan.FromSeconds(60);
                opt.SlidingExpiration = System.TimeSpan.FromSeconds(60);

                _cache.SetString(keyCache, serializedResponse, opt);
            }
            

            //throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string keyCache = context.HttpContext.Request.Path.Value + "-" + context.HttpContext.User.FindFirst("id").Value;


            var cachedData = _cache.GetString(keyCache);
            if (!string.IsNullOrEmpty(cachedData))
            {
                context.Result = new OkObjectResult(JsonConvert.DeserializeObject(cachedData, settings));
                
            }
            //context.Result = new ContentResult()
            //{
            //    Content = "Cacheado bigote"
            //};

            //throw new NotImplementedException();
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string keyCache = context.HttpContext.Request.Path.Value + "-" + context.HttpContext.User.FindFirst("id").Value;


            var cachedData = await _cache.GetStringAsync(keyCache);
            if (!string.IsNullOrEmpty(cachedData))
            {
                context.Result = new OkObjectResult(JsonConvert.DeserializeObject(cachedData, settings));

            }
            else
            {
                var contextExecuted = await next();

                string serializedResponse = JsonConvert.SerializeObject((contextExecuted.Result as OkObjectResult).Value, settings);

                DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
                opt.AbsoluteExpirationRelativeToNow = System.TimeSpan.FromSeconds(60);
                opt.SlidingExpiration = System.TimeSpan.FromSeconds(60);

                await _cache.SetStringAsync(keyCache, serializedResponse, opt);
            }
        }
    }
}
