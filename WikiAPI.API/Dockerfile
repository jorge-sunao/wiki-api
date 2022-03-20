#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WikiAPI.API/WikiAPI.API.csproj", "WikiAPI.API/"]
COPY ["WikiAPI.Infrastructure/WikiAPI.Infrastructure.csproj", "WikiAPI.Infrastructure/"]
COPY ["WikiAPI.Application/WikiAPI.Application.csproj", "WikiAPI.Application/"]
COPY ["WikiAPI.Domain/WikiAPI.Domain.csproj", "WikiAPI.Domain/"]
COPY ["WikiAPI.Persistence/WikiAPI.Persistence.csproj", "WikiAPI.Persistence/"]
RUN dotnet restore "WikiAPI.API/WikiAPI.API.csproj"
COPY . .
WORKDIR "/src/WikiAPI.API"
RUN dotnet build "WikiAPI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WikiAPI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WikiAPI.API.dll"]