FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ARG rpc_port=2200
ARG ws_port=3000
ENV TZ=Asia/Shanghai 
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone 
EXPOSE ${rpc_port} ${ws_port}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY common.props .
COPY pfx.props .
COPY NuGet.Config .
COPY Common Common
COPY Shared Shared
COPY Silky.Account Silky.Account
COPY Silky.Identity/src/Silky.Identity.Database.Migrations Silky.Identity/src/Silky.Identity.Database.Migrations
COPY Silky.Identity/src/Silky.Identity.EntityFrameworkCore Silky.Identity/src/Silky.Identity.EntityFrameworkCore
COPY Silky.Identity/src/Silky.Identity.Domain Silky.Identity/src/Silky.Identity.Domain
COPY Silky.Identity/src/Silky.Identity.Application.Contracts Silky.Identity/src/Silky.Identity.Application.Contracts
COPY Silky.Identity/src/Silky.Identity.Domain.Shared Silky.Identity/src/Silky.Identity.Domain.Shared
COPY Silky.Organization/src/Silky.Organization.Application.Contracts Silky.Organization/src/Silky.Organization.Application.Contracts
COPY Silky.Organization/src/Silky.Organization.Domain.Shared Silky.Organization/src/Silky.Organization.Domain.Shared
COPY Silky.Permission/src/Silky.Permission.Application.Contracts Silky.Permission/src/Silky.Permission.Application.Contracts
COPY Silky.Permission/src/Silky.Permission.Domain.Shared Silky.Permission/src/Silky.Permission.Domain.Shared
COPY Silky.Position/src/Silky.Position.Application.Contracts Silky.Position/src/Silky.Position.Application.Contracts
COPY Silky.Position/src/Silky.Position.Domain.Shared Silky.Position/src/Silky.Position.Domain.Shared
COPY Silky.Saas/src/Silky.Saas.Application.Contracts Silky.Saas/src/Silky.Saas.Application.Contracts
COPY Silky.Saas/src/Silky.Saas.Domain.Shared Silky.Saas/src/Silky.Saas.Domain.Shared

RUN dotnet restore /src/Silky.Account/src/Silky.AccountHost/Silky.AccountHost.csproj --disable-parallel && \
    dotnet build --no-restore -c Release /src/Silky.Account/src/Silky.AccountHost/Silky.AccountHost.csproj

FROM build AS publish
WORKDIR /src/Silky.Account/src/Silky.AccountHost
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Silky.AccountHost.dll"]