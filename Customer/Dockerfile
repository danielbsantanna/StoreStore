FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/Customer
RUN dotnet restore Customer.API.csproj
RUN dotnet publish Customer.API.csproj -c Release

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /src/Customer/bin/Release/net9.0/publish .
EXPOSE 5002
ENTRYPOINT ["dotnet", "Customer.API.dll"]


