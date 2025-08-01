# Setup
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=cdcc62c277ac7699e53f2c9a7e58709bFFFFNRAL \
NEW_RELIC_APP_NAME="fcg-api"

# ✅ Troca para root para conseguir instalar pacotes
USER root

RUN apt-get update && \
    apt-get install -y wget ca-certificates gnupg && \
    mkdir -p /usr/share/keyrings && \
    wget -O- https://download.newrelic.com/548C16BF.gpg | gpg --dearmor -o /usr/share/keyrings/newrelic-archive-keyring.gpg && \
    echo "deb [signed-by=/usr/share/keyrings/newrelic-archive-keyring.gpg] http://apt.newrelic.com/debian/ newrelic non-free" > /etc/apt/sources.list.d/newrelic.list && \
    apt-get update && \
    apt-get install -y newrelic-dotnet-agent && \
    rm -rf /var/lib/apt/lists/*


# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Corrigido: sem "src/" no caminho
COPY ["FCG.API/FCG.API.csproj", "FCG.API/"]
COPY ["FCG.Application/FCG.Application.csproj", "FCG.Application/"]
COPY ["FCG.Domain/FCG.Domain.csproj", "FCG.Domain/"]
COPY ["FCG.Infra.Data/FCG.Infra.Data.csproj", "FCG.Infra.Data/"]

RUN dotnet restore "FCG.API/FCG.API.csproj"
COPY . .
WORKDIR "/src/FCG.API"
RUN dotnet build "FCG.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Run API
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN echo "📂 Conteúdo da pasta /app/newrelic:" && ls -R /app/newrelic || echo "❌ Pasta /app/newrelic não encontrada"

ENTRYPOINT ["dotnet", "FCG.API.dll"]