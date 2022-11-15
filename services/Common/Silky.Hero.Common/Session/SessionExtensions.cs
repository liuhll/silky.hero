using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Runtime.Session;
using Silky.Core.Threading;
using Silky.Rpc.Runtime.Client;

namespace Silky.Hero.Common.Session;

public static class SessionExtensions
{
    private const string getCurrentUserDataRangeServiceEntryId = "Silky.Account.Application.Contracts.Account.IAccountAppService.GetCurrentUserDataRangeAsync_Get";
    public static async Task<CurrentUserDataRange> GetCurrentUserDataRangeAsync(this ISession session)
    {
        Check.NotNull(session, nameof(session));
        if (!session.IsLogin())
        {
            throw new BusinessException("您还没有登陆系统");
        }

        var invokeTemplate = EngineContext.Current.Resolve<IInvokeTemplate>();
        var  result = await invokeTemplate.InvokeForObjectByServiceEntryId<CurrentUserDataRange>(getCurrentUserDataRangeServiceEntryId);
        return result;
    }

    public static CurrentUserDataRange GetCurrentUserDataRange(this ISession session)
    {
        return AsyncHelper.RunSync(() => session.GetCurrentUserDataRangeAsync());
    }
}