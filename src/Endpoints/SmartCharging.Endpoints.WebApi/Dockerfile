#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Endpoints/SmartCharging.Endpoints.WebApi/SmartCharging.Endpoints.WebApi.csproj", "src/Endpoints/SmartCharging.Endpoints.WebApi/"]
COPY ["src/Core/SmartCharging.Core.ApplicationService/SmartCharging.Core.ApplicationService.csproj", "src/Core/SmartCharging.Core.ApplicationService/"]
COPY ["src/Core/SmartCharging.Core.Domain/SmartCharging.Core.Domain.csproj", "src/Core/SmartCharging.Core.Domain/"]
COPY ["src/Infrastructures/SmartCharging.Infrastructures.Data/SmartCharging.Infrastructures.Data.csproj", "src/Infrastructures/SmartCharging.Infrastructures.Data/"]
RUN dotnet restore "src/Endpoints/SmartCharging.Endpoints.WebApi/SmartCharging.Endpoints.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Endpoints/SmartCharging.Endpoints.WebApi"
RUN dotnet build "SmartCharging.Endpoints.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartCharging.Endpoints.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartCharging.Endpoints.WebApi.dll"]