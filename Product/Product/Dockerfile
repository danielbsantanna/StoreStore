# Stage 1: Build and publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/Product
RUN dotnet restore Product.Api.csproj
RUN dotnet publish Product.Api.csproj -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5004
ENTRYPOINT ["dotnet", "Product.Api.dll"]