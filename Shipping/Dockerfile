FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/Shipping

RUN dotnet restore Shipping.API.csproj
RUN dotnet publish Shipping.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5010
ENTRYPOINT ["dotnet", "MINHAPIKA.dll"]

