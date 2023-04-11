FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["Services/ApiGateways/ApiGateway.Web/ApiGateway.Web.csproj", "Services/ApiGateways/ApiGateway.Web/"]
#RUN dotnet restore "Services/ApiGateways/ApiGateway.Web/ApiGateway.Web.csproj"
#COPY . .
#WORKDIR "/Services/ApiGateways/ApiGateway.Web"
#RUN dotnet build "ApiGateway.Web.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ApiGateway.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ApiGateway.Web.dll"]


WORKDIR /src
COPY "Services/ApiGateways/ApiGateway.Web/" "Services/ApiGateways/ApiGateway.Web/"
RUN dotnet restore "Services/ApiGateways/ApiGateway.Web/ApiGateway.Web.csproj"
WORKDIR "/src/Services/ApiGateways/ApiGateway.Web"
RUN dotnet build "ApiGateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Web.dll"]