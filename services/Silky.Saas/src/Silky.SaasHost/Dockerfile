ARG NET_IMAGE_BASE=7.0-bullseye-slim
ARG NET_IMAGE_BUILD=7.0
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE_BASE} AS base
WORKDIR /app
ARG rpc_port=2200
ARG ws_port=3000
ENV TZ=Asia/Shanghai 
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone 
EXPOSE ${rpc_port} ${ws_port}

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE_BUILD} AS build
WORKDIR /src
COPY common.props .
COPY pfx.props .
COPY NuGet.Config .
COPY Common Common
COPY Shared Shared
COPY Silky.Saas Silky.Saas
COPY Silky.Account/src/Silky.Account.Application.Contracts Silky.Account/src/Silky.Account.Application.Contracts
COPY Silky.Identity/src/Silky.Identity.Domain.Shared Silky.Identity/src/Silky.Identity.Domain.Shared
COPY Silky.Identity/src/Silky.Identity.Application.Contracts Silky.Identity/src/Silky.Identity.Application.Contracts
COPY Silky.Organization/src/Silky.Organization.Application.Contracts Silky.Organization/src/Silky.Organization.Application.Contracts
COPY Silky.Organization/src/Silky.Organization.Domain.Shared Silky.Organization/src/Silky.Organization.Domain.Shared
COPY Silky.Position/src/Silky.Position.Application.Contracts Silky.Position/src/Silky.Position.Application.Contracts
COPY Silky.Position/src/Silky.Position.Domain.Shared Silky.Position/src/Silky.Position.Domain.Shared
RUN dotnet restore /src/Silky.Saas/src/Silky.SaasHost/Silky.SaasHost.csproj --disable-parallel && \
    dotnet build --no-restore -c Release /src/Silky.Saas/src/Silky.SaasHost/Silky.SaasHost.csproj

FROM build AS publish
WORKDIR /src/Silky.Saas/src/Silky.SaasHost
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Silky.SaasHost.dll"]