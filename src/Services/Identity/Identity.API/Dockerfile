FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY "Services/Identity" "Services/Identity"
#COPY "Services/Common" "Services/Common"
#RUN dotnet restore "Services/Identity/Identity.API/Identity.API.csproj"
#
#WORKDIR "/src/Services/Identity/Identity.API"
#RUN dotnet build "Identity.API.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Identity.API.dll"]
#


WORKDIR /src
COPY "Services/Identity/" "Services/Identity/"
COPY "Services/Common/" "Services/Common/"
RUN dotnet restore "Services/Identity/Identity.API/Identity.API.csproj"
WORKDIR "/src/Services/Identity/Identity.API/"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]