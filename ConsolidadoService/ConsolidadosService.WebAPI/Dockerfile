#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConsolidadoService/ConsolidadosService.WebAPI/ConsolidadosService.WebAPI.csproj", "ConsolidadoService/ConsolidadosService.WebAPI/"]
COPY ["ConsolidadoService/ConsolidadoService.Business/ConsolidadoService.Business.csproj", "ConsolidadoService/ConsolidadoService.Business/"]
COPY ["ConsolidadoService/ConsolidadoService.Data/ConsolidadoService.Data.csproj", "ConsolidadoService/ConsolidadoService.Data/"]
COPY ["ConsolidadoService/ConsolidadoService.Domain/ConsolidadoService.Domain.csproj", "ConsolidadoService/ConsolidadoService.Domain/"]
RUN dotnet restore "./ConsolidadoService/ConsolidadosService.WebAPI/ConsolidadosService.WebAPI.csproj"
COPY . .
WORKDIR "/src/ConsolidadoService/ConsolidadosService.WebAPI"
RUN dotnet build "./ConsolidadosService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConsolidadosService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsolidadosService.WebAPI.dll"]