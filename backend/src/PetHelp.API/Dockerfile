FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetHelp.API/PetHelp.API.csproj", "src/PetHelp.API/"]
COPY ["src/PetHelp.Application/PetHelp.Application.csproj", "src/PetHelp.Application/"]
COPY ["src/PetHelp.Domain/PetHelp.Domain.csproj", "src/PetHelp.Domain/"]
COPY ["src/PetHelp.Infrastructure/PetHelp.Infrastructure.csproj", "src/PetHelp.Infrastructure/"]
RUN dotnet restore "src/PetHelp.API/PetHelp.API.csproj"
COPY . .
WORKDIR "/src/src/PetHelp.API"
RUN dotnet build "PetHelp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetHelp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetHelp.API.dll"]
