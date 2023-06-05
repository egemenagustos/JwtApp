#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Onion/Presentation/Onion.JwtApp.API/Onion.JwtApp.API.csproj", "Onion/Presentation/Onion.JwtApp.API/"]
COPY ["Onion/Core/Onion.JwpApp.Application/Onion.JwpApp.Application.csproj", "Onion/Core/Onion.JwpApp.Application/"]
COPY ["Onion/Core/Onion.JwtApp.Domain/Onion.JwtApp.Domain.csproj", "Onion/Core/Onion.JwtApp.Domain/"]
COPY ["Onion/Infrastructure/Onion.JwtApp.Persistance/Onion.JwtApp.Persistance.csproj", "Onion/Infrastructure/Onion.JwtApp.Persistance/"]
RUN dotnet restore "Onion/Presentation/Onion.JwtApp.API/Onion.JwtApp.API.csproj"
COPY . .
WORKDIR "/src/Onion/Presentation/Onion.JwtApp.API"
RUN dotnet build "Onion.JwtApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Onion.JwtApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onion.JwtApp.API.dll"]