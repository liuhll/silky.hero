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
COPY Silky.Organization Silky.Organization
COPY Silky.Account/src/Silky.Account.Application.Contracts Silky.Account/src/Silky.Account.Application.Contracts
COPY Silky.Identity/src/Silky.Identity.Domain.Shared Silky.Identity/src/Silky.Identity.Domain.Shared
COPY Silky.Identity/src/Silky.Identity.Application.Contracts Silky.Identity/src/Silky.Identity.Application.Contracts
COPY Silky.Position/src/Silky.Position.Domain.Shared Silky.Position/src/Silky.Position.Domain.Shared
COPY Silky.Position/src/Silky.Position.Application.Contracts Silky.Position/src/Silky.Position.Application.Contracts

RUN dotnet restore /src/Silky.Organization/src/Silky.OrganizationHost/Silky.OrganizationHost.csproj --disable-parallel && \
    dotnet build --no-restore -c Release /src/Silky.Organization/src/Silky.OrganizationHost/Silky.OrganizationHost.csproj

FROM build AS publish
WORKDIR /src/Silky.Organization/src/Silky.OrganizationHost
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Silky.OrganizationHost.dll"]