using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Runtime.Session;
using Silky.Core.Threading;
using Silky.Rpc.Runtime.Client;

namespace Silky.Hero.Common.Session;

public static class SessionExtensions
{
    public static Task<CurrentUserDataRange> GetCurrentUserDataRangeAsync(this ISession session)
    {
        Check.NotNull(session, nameof(session));
        if (!session.IsLogin())
        {
            throw new BusinessException("您还没有登陆系统");
        }

        var invokeTemplate = EngineContext.Current.Resolve<IInvokeTemplate>();
        return invokeTemplate.GetForObjectAsync<CurrentUserDataRange>("api/account/currentuserinfo");
    }

    public static CurrentUserDataRange GetCurrentUserDataRange(this ISession session)
    {
        Check.NotNull(session, nameof(session));
        if (!session.IsLogin())
        {
            throw new BusinessException("您还没有登陆系统");
        }

        var invokeTemplate = EngineContext.Current.Resolve<IInvokeTemplate>();
        return AsyncHelper.RunSync(() =>
            invokeTemplate.GetForObjectAsync<CurrentUserDataRange>("api/account/currentuserinfo"));
    }
}