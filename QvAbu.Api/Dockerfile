FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY QvAbu.Api/QvAbu.Api.csproj QvAbu.Api/
COPY QvAbu.Data/QvAbu.Data.csproj QvAbu.Data/
RUN dotnet restore QvAbu.Api/
RUN dotnet restore QvAbu.Data/
COPY . .
WORKDIR /src/QvAbu.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QvAbu.Api.dll"]
