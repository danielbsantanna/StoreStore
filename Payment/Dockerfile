FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/Payment

RUN dotnet restore Payment.API.csproj
RUN dotnet publish Payment.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5003
ENTRYPOINT ["dotnet", "Payment.API.dll"]
