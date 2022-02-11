using Silky.Account.Application.Contracts.Account;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Runtime.Session;

namespace Silky.Hero.Common.Session;

public static class SessionExtensions
{
    public static Task<GetCurrentUserDataRange> GetCurrentUserDataRangeAsync(this ISession session)
    {
        Check.NotNull(session, nameof(session));
        if (!session.IsLogin())
        {
            throw new BusinessException("您还没有登陆系统");
        }

        var accountAppService = EngineContext.Current.Resolve<IAccountAppService>();
        return accountAppService.GetCurrentUserDataRangeAsync();
    }
    
    public static GetCurrentUserDataRange GetCurrentUserDataRange(this ISession session)
    {
        Check.NotNull(session, nameof(session));
        if (!session.IsLogin())
        {
            throw new BusinessException("您还没有登陆系统");
        }

        var accountAppService = EngineContext.Current.Resolve<IAccountAppService>();
        return accountAppService.GetCurrentUserDataRangeAsync().GetAwaiter().GetResult();
    }
}