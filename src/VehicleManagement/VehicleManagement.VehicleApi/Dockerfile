#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["VehicleManagement.VehicleApi/VehicleManagement.VehicleApi.csproj", "VehicleManagement.VehicleApi/"]
RUN dotnet restore "VehicleManagement.VehicleApi/VehicleManagement.VehicleApi.csproj"
COPY . .
WORKDIR "/src/VehicleManagement.VehicleApi"
RUN dotnet build "VehicleManagement.VehicleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleManagement.VehicleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleManagement.VehicleApi.dll"]