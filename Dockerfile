# .NET build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
COPY ./api /src

WORKDIR /src 
RUN dotnet restore *.csproj
RUN dotnet build *.csproj -c Publish -o /app/build

# .NET publish
FROM dotnet-build AS dotnet-publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Node.js build
FROM node AS node-builder
COPY ./ui /node

WORKDIR /node
RUN npm install
RUN npm run build

# Final stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
RUN mkdir /app/wwwroot
COPY --from=dotnet-publish /app/publish .
COPY --from=node-builder /node/build ./wwwroot

EXPOSE 80

ENTRYPOINT ["dotnet", "api.dll"]
