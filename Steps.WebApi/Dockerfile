FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Steps.WebApi/Steps.WebApi.csproj", "Steps.WebApi/"]
COPY ["Steps.Infrastructure.Data/Steps.Infrastructure.Data.csproj", "Steps.Infrastructure.Data/"]
COPY [" Steps.Domain/Steps.Domain.csproj", " Steps.Domain/"]
COPY ["Steps.Application/Steps.Application.csproj", "Steps.Application/"]
COPY ["Steps.Shared/Steps.Shared.csproj", "Steps.Shared/"]
RUN dotnet restore "Steps.WebApi/Steps.WebApi.csproj"
COPY . .
WORKDIR "/src/Steps.WebApi"
RUN dotnet build "Steps.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Steps.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Steps.Services.WebApi.dll"]
