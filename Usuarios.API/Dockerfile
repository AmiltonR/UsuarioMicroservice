FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Usuarios.API/Usuarios.API.csproj", "Usuarios.API/"]
COPY ["UserDbContext.Infrastructure/UserDbContext.Infrastructure.csproj", "UserDbContext.Infrastructure/"]
COPY ["UserDbContext.Domain/UserDbContext.Domain.csproj", "UserDbContext.Domain/"]
RUN dotnet restore "Usuarios.API/Usuarios.API.csproj"
COPY . .
WORKDIR "/src/Usuarios.API"
RUN dotnet build "Usuarios.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Usuarios.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Usuarios.API.dll"]