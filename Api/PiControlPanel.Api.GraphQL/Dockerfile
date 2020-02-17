# Base Stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get -y install sudo procps
	# Create pi user
RUN useradd -m pi
RUN echo pi:raspberry | chpasswd
RUN usermod -aG sudo pi
RUN echo 'pi ALL=(ALL) NOPASSWD: ALL' | EDITOR='tee -a' visudo
WORKDIR /app
EXPOSE 80

# Build Stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api/PiControlPanel.Api.GraphQL/PiControlPanel.Api.GraphQL.csproj", "Api/PiControlPanel.Api.GraphQL/"]
COPY ["Application/PiControlPanel.Application.BackgroundServices/PiControlPanel.Application.BackgroundServices.csproj", "Application/PiControlPanel.Application.BackgroundServices/"]
COPY ["Application/PiControlPanel.Application.Services/PiControlPanel.Application.Services.csproj", "Application/PiControlPanel.Application.Services/"]
COPY ["Domain/PiControlPanel.Domain.Contracts/PiControlPanel.Domain.Contracts.csproj", "Domain/PiControlPanel.Domain.Contracts/"]
COPY ["Domain/PiControlPanel.Domain.Models/PiControlPanel.Domain.Models.csproj", "Domain/PiControlPanel.Domain.Models/"]
COPY ["Infrastructure/PiControlPanel.Infrastructure.OnDemand/PiControlPanel.Infrastructure.OnDemand.csproj", "Infrastructure/PiControlPanel.Infrastructure.OnDemand/"]
COPY ["Infrastructure/PiControlPanel.Infrastructure.Persistence/PiControlPanel.Infrastructure.Persistence.csproj", "Infrastructure/PiControlPanel.Infrastructure.Persistence/"]
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
