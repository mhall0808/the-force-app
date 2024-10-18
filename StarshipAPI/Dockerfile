# Use the official .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy the csproj and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build it in Release mode
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/out .

# Set environment variable to Production
# NOTE HERE - the expectation for this application is to run it locally on a docker container.  
#   The connection string in Dev is to allow us to perform migrations.
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port that the application runs on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "StarshipAPI.dll"]
