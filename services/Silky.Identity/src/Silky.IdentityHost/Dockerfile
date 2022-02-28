FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV TZ=Asia/Shanghai 
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY common.props .
COPY pfx.props .
COPY NuGet.Config .
COPY Common Common
COPY Shared Shared
COPY Silky.Identity Silky.Identity
COPY Silky.Account/src/Silky.Account.Application.Contracts Silky.Account/src/Silky.Account.Application.Contracts
COPY Silky.Organization/src/Silky.Organization.Application.Contracts Silky.Organization/src/Silky.Organization.Application.Contracts
COPY Silky.Organization/src/Silky.Organization.Domain.Shared Silky.Organization/src/Silky.Organization.Domain.Shared
COPY Silky.Permission/src/Silky.Permission.Application.Contracts Silky.Permission/src/Silky.Permission.Application.Contracts
COPY Silky.Permission/src/Silky.Permission.Domain.Shared Silky.Permission/src/Silky.Permission.Domain.Shared
COPY Silky.Position/src/Silky.Position.Application.Contracts Silky.Position/src/Silky.Position.Application.Contracts
COPY Silky.Position/src/Silky.Position.Domain.Shared Silky.Position/src/Silky.Position.Domain.Shared
COPY Silky.Saas/src/Silky.Saas.Application.Contracts Silky.Saas/src/Silky.Saas.Application.Contracts
COPY Silky.Saas/src/Silky.Saas.Domain.Shared Silky.Saas/src/Silky.Saas.Domain.Shared
RUN dotnet restore /src/Silky.Identity/src/Silky.IdentityHost/Silky.IdentityHost.csproj --disable-parallel && \
    dotnet build --no-restore -c Release /src/Silky.Identity/src/Silky.IdentityHost/Silky.IdentityHost.csproj

FROM build AS publish
WORKDIR /src/Silky.Identity/src/Silky.IdentityHost
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Silky.IdentityHost.dll"]