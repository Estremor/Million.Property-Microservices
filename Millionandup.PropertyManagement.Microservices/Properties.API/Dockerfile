FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Properties.API/Properties.API.csproj", "Properties.API/"]
COPY ["Properties.Application/Properties.Application.csproj", "Properties.Application/"]
COPY ["Properties.Domain/Properties.Domain.csproj", "Properties.Domain/"]
COPY ["Properties.Infrastructure/Properties.Infrastructure.csproj", "Properties.Infrastructure/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared.Infrastructure/"]
RUN dotnet restore "./Properties.API/Properties.API.csproj"
COPY . .
WORKDIR "/src/Properties.API"
RUN dotnet build "./Properties.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Properties.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sh", "-c", "sleep 35 && dotnet Properties.API.dll"]