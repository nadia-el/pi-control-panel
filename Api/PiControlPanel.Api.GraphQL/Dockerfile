FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

# Build Stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api/PiControlPanel.Api.GraphQL/PiControlPanel.Api.GraphQL.csproj", "Api/PiControlPanel.Api.GraphQL/"]
RUN dotnet restore "Api/PiControlPanel.Api.GraphQL/PiControlPanel.Api.GraphQL.csproj"
COPY . .
WORKDIR "/src/Api/PiControlPanel.Api.GraphQL"
RUN dotnet build "PiControlPanel.Api.GraphQL.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "PiControlPanel.Api.GraphQL.csproj" -c Release -o /app/publish

# Runtime Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PiControlPanel.Api.GraphQL.dll"]
HEALTHCHECK --interval=10s --timeout=20s --retries=3 CMD curl --fail http://localhost:80/healthcheck || exit 1
