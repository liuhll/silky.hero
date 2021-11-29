using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;

namespace Silky.Identity.Application.Contracts.User;

[ServiceRoute]
public interface IUserAppService
{
    [HttpPost]
    [HttpPut]
    [RemoveCachingIntercept(typeof(GetUserOutput), "id:{0}")]
    Task CreateOrUpdateAsync(CreateOrUpdateUserInput input);

    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetUserOutput), "id:{0}")]
    Task DeleteAsync([CacheKey(0)]long id);

    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetUserOutput> GetAsync([CacheKey(0)]long id);

    Task<PagedList<GetUserPageOutput>> GetPageAsync(GetUserPageInput input);
}