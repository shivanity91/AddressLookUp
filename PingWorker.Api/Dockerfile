#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PingWorker.Api/PingWorker.Api.csproj", "PingWorker.Api/"]
COPY ["Api.Common/Api.Common.csproj", "Api.Common/"]
RUN dotnet restore "PingWorker.Api/PingWorker.Api.csproj"
COPY . .
WORKDIR "/src/PingWorker.Api"
RUN dotnet build "PingWorker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PingWorker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PingWorker.Api.dll"]