FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 7059

ENV ASPNETCORE_URLS=http://*:7059

ENTRYPOINT ["dotnet", "api.dll"]