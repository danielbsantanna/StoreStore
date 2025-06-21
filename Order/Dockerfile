FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/Order

RUN dotnet restore Order.API.csproj
RUN dotnet publish Order.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5001
RUN echo "🗂 Published files:" && ls -la /app/publish
ENTRYPOINT ["dotnet", "Order.API.dll"]
