# Build Stage (Compile & Publish)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy published app files
COPY ./bin/Release/net9.0 .

# Expose the service’s port
EXPOSE 5004

# Run the microservice
ENTRYPOINT ["dotnet", "Product.Api.dll"]
