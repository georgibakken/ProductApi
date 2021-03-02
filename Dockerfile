# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ProductApi/*.csproj ./ProductApi/
COPY ProductApi.Tests/*.csproj ./ProductApi.Tests/

RUN dotnet restore

# copy everything else and build app
COPY ProductApi/. ./ProductApi/
COPY ProductApi.Tests/. ./ProductApi.Tests/

WORKDIR /source/ProductApi
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ProductApi.dll"]
