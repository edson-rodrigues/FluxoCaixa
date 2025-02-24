#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LancamentosService/LancamentosWebAPI/LancamentosService.WebAPI.csproj", "LancamentosService/LancamentosWebAPI/"]
COPY ["LancamentosService.Business/LancamentosService.Business.csproj", "LancamentosService.Business/"]
COPY ["LancamentosService.Data/LancamentosService.Data.csproj", "LancamentosService.Data/"]
COPY ["LancamentosService.Domain/LancamentosService.Domain.csproj", "LancamentosService.Domain/"]
COPY ["LancamentosService/LancamentosService.MessageBroker/LancamentosService.MessageBroker.csproj", "LancamentosService/LancamentosService.MessageBroker/"]
COPY ["LancamentosService.IoC/LancamentosService.IoC.csproj", "LancamentosService.IoC/"]
RUN dotnet restore "./LancamentosService/LancamentosWebAPI/LancamentosService.WebAPI.csproj"
COPY . .
WORKDIR "/src/LancamentosService/LancamentosWebAPI"
RUN dotnet build "./LancamentosService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LancamentosService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LancamentosService.WebAPI.dll"]