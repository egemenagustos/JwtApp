FROM mcr.microsoft.com/dotnet/core/sdk:6.0 as build
WORKDIR /app
COPY ./JwtAp.Front/*.csproj ./JwtAp.Front/
COPY .Onion/Core/Onion.JwpApp.Application/ *.csproj ./Onion/Core/Onion.JwpApp.Application/
COPY .Onion/Core/Onion.JwtApp.Domain/ *.csproj ./Onion/Core/Onion.JwtApp.Domain/
COPY .Onion/Infrastructure/Onion.JwtApp.Persistance/ *.csproj .Onion/Infrastructure/Onion.JwtApp.Persistance/
COPY .Onion/Presentation/Onion.JwtApp.API/ *.csproj .Onion/Presentation/Onion.JwtApp.API/
COPY *.sln .
RUN dotnet Restore
COPY . .
RUN dotnet publish ./Onion/Presentation/Onion.JwtApp.API/*.csproj -0 /publish/
RUN dotnet publish ./JwtAp.Front/*.csproj ./JwtAp.Front/*.csproj -0 /publish/
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet","Onion.JwtApp.API.dll" ]
