FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

# 🔍 Sanity check: what's inside /src?
RUN echo "📁 Inside /src:" && ls -la /src

WORKDIR /src/Product

# 🔍 Check contents of this working directory
RUN echo "📁 Inside /src/Product/Product:" && ls -la

RUN dotnet restore Product.csproj
RUN dotnet publish Product.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5004
ENTRYPOINT ["dotnet", "Product.Api.dll"]
