# syntax=docker/dockerfile:1
# Multi-stage build for .NET 9 API (and optional integrated frontend)

ARG DOTNET_VERSION=9.0
ARG NODE_VERSION=22-alpine

#############################
# Frontend build (optional) #
#############################
FROM node:${NODE_VERSION} AS frontend-build
WORKDIR /app/frontend
# Copy only package manifests first for caching
COPY frontend/package*.json ./
RUN --mount=type=cache,target=/root/.npm npm install --legacy-peer-deps || npm install
# Copy rest of frontend source
COPY frontend/. .
# Build (outputs to dist/)
RUN npm run build

#############################
# Backend restore/build     #
#############################
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src
# Copy csproj and restore as distinct layers for caching
COPY Server/Server.csproj Server/
RUN dotnet restore Server/Server.csproj
# Copy the rest of the source
COPY Server/. Server/
WORKDIR /src/Server
RUN dotnet build -c Release -o /app/build

#############################
# Publish                    #
#############################
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

#############################################
# Final runtime image (aspnet base runtime) #
#############################################
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app
# Install curl for healthchecks (apk available on alpine; aspnet base is Debian/Ubuntu depending on tag)
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
# Copy published backend
COPY --from=publish /app/publish .
# Optionally copy frontend build into wwwroot if present (single container approach)
# We copy from the stage unconditionally; if you don't want frontend assets, you can build with target=final-no-frontend later.
COPY --from=frontend-build /app/frontend/dist ./wwwroot

# Environment / minimal config
ENV ASPNETCORE_URLS=http://0.0.0.0:8080 \
    DOTNET_EnableDiagnostics=0
EXPOSE 8080

# Health check (basic) use explicit API status endpoint (root may serve static or 404)
HEALTHCHECK --interval=30s --timeout=3s CMD curl -fsS http://127.0.0.1:8080/api/status || exit 1

ENTRYPOINT ["dotnet", "Server.dll"]

#############################################################
# Alternative target without frontend (docker build --target final-no-frontend -t api .)
#############################################################
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final-no-frontend
WORKDIR /app
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080 DOTNET_EnableDiagnostics=0
# Add healthcheck for api-only target as well
HEALTHCHECK --interval=30s --timeout=3s CMD curl -fsS http://127.0.0.1:8080/api/status || exit 1
EXPOSE 8080
ENTRYPOINT ["dotnet", "Server.dll"]
