# syntax=docker/dockerfile:1
# Multi-stage build: Node build -> Nginx serve
ARG NODE_VERSION=22-alpine
ARG NGINX_VERSION=stable-alpine
ARG VITE_API_BASE_URL=http://localhost:8080

####################
# Build stage      #
####################
FROM node:${NODE_VERSION} AS build
WORKDIR /app
COPY package*.json ./
RUN --mount=type=cache,target=/root/.npm npm install --legacy-peer-deps || npm install
COPY . .
# Inject API base URL into Vite env before build
RUN echo "VITE_API_BASE_URL=${VITE_API_BASE_URL}" > .env.production && npm run build

####################
# Runtime stage    #
####################
FROM nginx:${NGINX_VERSION} AS runtime
# Remove default nginx static assets
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/ /usr/share/nginx/html/
# Basic health endpoint
RUN printf 'server {\n  listen 80;\n  root /usr/share/nginx/html;\n  add_header Cache-Control "public, max-age=31536000";\n  location /healthz { return 200 "ok"; }\n  try_files $uri $uri/ /index.html;\n}\n' > /etc/nginx/conf.d/default.conf
EXPOSE 80
HEALTHCHECK --interval=30s --timeout=3s CMD wget -qO- http://127.0.0.1/healthz || exit 1
