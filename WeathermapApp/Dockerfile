#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeathermapApp/WeathermapApp.csproj", "WeathermapApp/"]
COPY ["WeathermapApp.DAL/WeathermapApp.DAL.csproj", "WeathermapApp.DAL/"]
RUN dotnet restore "WeathermapApp/WeathermapApp.csproj"
COPY . .
WORKDIR "/src/WeathermapApp"
RUN dotnet build "WeathermapApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeathermapApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeathermapApp.dll"]
