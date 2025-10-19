# PersonalPortfolio

A full-stack personal portfolio SPA built with ASP.NET Core (Minimal API) and Vue 3 + Vite + TypeScript.

## Features
- Minimal API backend serving profile, projects, experience, and skills (in-memory seed data)
- Vue 3 frontend with router, typed API client, reusable components
- Dark themed responsive layout
- Integration + unit tests (xUnit + WebApplicationFactory)
- Type-safe shared data contracts (mirrored interfaces)

## Tech Stack
- Backend: .NET 9, Minimal APIs, In-memory data service
- Frontend: Vue 3, Vite, TypeScript, vue-router, axios
- Testing: xUnit, Microsoft.AspNetCore.Mvc.Testing

## Project Structure
```
PersonalPortfolio.sln
Server/                # ASP.NET Core minimal API
  Models/              # Record types for domain
  Services/            # In-memory data provider
  Program.cs           # Endpoint definitions
  ProgramMarker.cs     # Marker for tests
Server.Tests/          # xUnit tests
frontend/              # Vite + Vue 3 app
  src/
    components/        # Reusable UI parts
    views/             # Page views (routed)
    router/            # Vue Router setup
    services/          # API client
    types/             # TypeScript interfaces
    main.ts            # App bootstrap
```

## Running (Development)
Open two terminals or run concurrently with a tool of your choice.

Backend (default Kestrel on dynamic port, we will pin to 5000 for simplicity):
```powershell
cd Server
set ASPNETCORE_URLS=http://localhost:5000
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

Frontend (served at http://localhost:5173):
```powershell
cd frontend
npm run dev
```

Then browse http://localhost:5173. The frontend uses `VITE_API_BASE_URL` (.env.development) pointing to http://localhost:5000.

## Testing
```powershell
dotnet test
```

## Building Production Frontend
```powershell
cd frontend
npm run build
```
Outputs to `frontend/dist`.

## Adding / Changing Data
Currently data lives in `PortfolioData` (in-memory). Extend or replace with a database (EF Core, Dapper, etc.).

## Deployment Ideas
- Containerize: multi-stage Dockerfile (backend + copy built frontend into wwwroot or serve separately via static hosting)
- Azure App Service or Azure Container Apps for backend
- Static frontend hosting (Azure Static Web Apps / GitHub Pages) hitting hosted API

## Docker / Containerization

Two supported approaches:

### 1. Multi-Container (Recommended for local dev clarity)

Services: `api` (.NET) + `frontend` (nginx serving built Vue app)

Build & run (requires Docker):
```powershell
docker compose up --build
```
Then:
- API: http://localhost:8080 (health: `/api/status`)
- Frontend: http://localhost:5173 (reverse-proxy not used; frontend calls API directly)

Edit code & rebuild changed service:
```powershell
docker compose build api
docker compose up -d api
```

### 2. Single Container (API serves static SPA)

The backend `Server/Dockerfile` builds frontend in an earlier stage and copies its `dist` output to `wwwroot`.

Build:
```powershell
docker build -t personal-portfolio-all-in-one ./Server
```
Run:
```powershell
docker run -p 8080:8080 personal-portfolio-all-in-one
```
Browse http://localhost:8080 (API JSON status at `/api/status`).

Skip frontend build (API only) by targeting the `final-no-frontend` stage:
```powershell
docker build -t personal-portfolio-api --target final-no-frontend ./Server
```

### Environment Overrides
- Change API port: set `ASPNETCORE_URLS` env or map a different host port.
  ```powershell
  docker run -e ASPNETCORE_URLS=http://+:5000 -p 5000:5000 personal-portfolio-all-in-one
  ```

### Development Hot Reload (Optional)
For iterative backend development without rebuilding the image each time, you can:
```powershell
docker run --rm -it -v ${PWD}/Server:/src -w /src -p 8080:8080 mcr.microsoft.com/dotnet/sdk:9.0 bash -c "dotnet watch run --urls http://0.0.0.0:8080"
```
Similarly for the frontend:
```powershell
docker run --rm -it -v ${PWD}/frontend:/app -w /app -p 5173:5173 node:22-alpine sh -c "npm install && npm run dev -- --host"
```

### Image Size Notes
- Separate build + runtime stages reduce final size.
- Using `frontend` multi-stage avoids shipping build tooling in nginx image.

### Health Checks
- API container: root GET (or `/api/status`).
- Frontend container: `/healthz` simple endpoint configured in nginx.

### Production Deployment Options
- Push both images to a registry (e.g., GHCR / Docker Hub) and deploy via orchestrator (Azure Container Apps, Kubernetes, App Service for Containers).
- Or use single container for simplest hosting where only one port is allowed.

### Security Hardening (Next Steps)
- Add non-root user layers.
- Apply `readonly` filesystem + `no-new-privileges` in runtime.
- Configure caching headers more granularly.
- Add reverse proxy (nginx / YARP) if adding SSR or auth.

---

## Next Step Suggestions
1. SEO & Metadata: Add `<head>` meta tags, OpenGraph, sitemap.xml, robots.txt.
2. Content Editing: Move data to JSON or a lightweight CMS (e.g., static JSON in blob storage, headless CMS, or YAML in repo).
3. Contact Form: Add POST endpoint + email using Azure Communication Services or SendGrid.
4. Caching: MemoryCache for expensive calls or external fetches once dynamic.
5. Auth (Optional): Simple admin area with GitHub / Microsoft OAuth to manage content.
6. Images / Assets: Add optimized project screenshots and lazy loading.
7. Accessibility: Run automated axe tests & manual keyboard review.
8. CI/CD: GitHub Actions building backend & frontend, running tests, deploying.
9. Analytics: Add privacy-friendly analytics (Plausible / Azure App Insights custom events).
10. Light Theme Toggle: Add CSS variables + toggle persisted in localStorage.
11. Error Boundary / Logging: Global error handler + structured logging.
12. OpenAPI Client Generation: Use NSwag to generate TypeScript client if contracts grow.
13. Docker Hardening: Add non-root user and stricter permissions.
14. CI Docker Cache: Use GitHub Actions buildx with layer caching.

## Environment Variables
Frontend: `.env.development` sets `VITE_API_BASE_URL`.
Backend: Use `ASPNETCORE_URLS` to force port locally.

## License
MIT (add a LICENSE file if you want to open source).

---
Feel free to ask for any enhancements or deployment automation next.
