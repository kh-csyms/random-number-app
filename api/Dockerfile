# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy csproj and restore dependencies.
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the files and build the app.
COPY . ./
RUN dotnet publish -c Release -o out

# Generate runtime image.
FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 7059 for the app to listen on.
EXPOSE 7059

# Set environment variable for ASP.NET Core to listen on port 7059.
ENV ASPNETCORE_URLS=http://*:7059

# Configure the entrypoint for the container.
ENTRYPOINT ["dotnet", "api.dll"]