# Tabitha Portfolio

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
TabithaPortfolio.sln
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

## Environment Variables
Frontend: `.env.development` sets `VITE_API_BASE_URL`.
Backend: Use `ASPNETCORE_URLS` to force port locally.

## License
MIT (add a LICENSE file if you want to open source).

---
Feel free to ask for any enhancements or deployment automation next.
