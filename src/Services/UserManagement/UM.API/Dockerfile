#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY "Services/UserManagement/" "Services/UserManagement/"
COPY "Services/Common/" "Services/Common/"
RUN dotnet restore "Services/UserManagement/UM.API/UM.API.csproj"
WORKDIR "/src/Services/UserManagement/UM.API/"
RUN dotnet build "UM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UM.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UM.API.dll"]