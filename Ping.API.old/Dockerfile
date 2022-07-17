#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ping.Api/Ping.Api.csproj", "Ping.Api/"]
COPY ["AddressLookUp.Aggregator.Api/AddressLookUp.Aggregator.Api.csproj", "AddressLookUp.Aggregator.Api/"]
COPY ["Api.Common/Api.Common.csproj", "Api.Common/"]
RUN dotnet restore "Ping.Api/Ping.Api.csproj"
COPY . .
WORKDIR "/src/Ping.Api"
RUN dotnet build "Ping.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ping.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ping.Api.dll"]