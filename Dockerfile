# Build the React App
FROM node:14 as build
WORKDIR /app
COPY ./ui/package.json ./ui/package-lock.json ./
RUN npm install
COPY ./ui/ ./
RUN npm run build

# Build the .NET App
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./api/*.csproj ./
RUN dotnet restore

# Copy the React build files to wwwroot
COPY --from=build /app/build ./wwwroot

# Copy everything else and build
COPY ./api/ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 7059
ENV ASPNETCORE_URLS=http://*:7059
ENTRYPOINT ["dotnet", "api.dll"]
