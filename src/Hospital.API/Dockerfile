# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy all source code to /src inside container
COPY ../../ ./

# Restore only the main project
RUN dotnet restore src/Hospital.API/Hospital.API.csproj

# Publish
RUN dotnet publish src/Hospital.API/Hospital.API.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Hospital.API.dll"]
