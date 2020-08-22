#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["RealEstate.MVC/RealEstate.MVC.csproj", "RealEstate.MVC/"]
COPY ["RealEstate.ExternalServices/RealEstate.ExternalServices.csproj", "RealEstate.ExternalServices/"]
COPY ["RealEstate.Interfaces/RealEstate.Core.Interfaces.csproj", "RealEstate.Interfaces/"]
COPY ["RealEstate.Entities/RealEstate.Core.Entities.csproj", "RealEstate.Entities/"]
COPY ["RealEstate.Repository/RealEstate.Repositories.csproj", "RealEstate.Repository/"]
COPY ["RealEstate/RealEstate.Core.Services.csproj", "RealEstate/"]
RUN dotnet restore "RealEstate.MVC/RealEstate.MVC.csproj"
COPY . .
WORKDIR "/src/RealEstate.MVC"
RUN dotnet build "RealEstate.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealEstate.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealEstate.MVC.dll"]