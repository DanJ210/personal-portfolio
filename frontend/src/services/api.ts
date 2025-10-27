import axios from 'axios';
import type { Profile, Project, Experience, Skill } from '../types/portfolio';

// Determine API base URL (no hardcoded port in production):
// Priority:
// 1. Explicit build-time env var VITE_API_BASE_URL
// 2. Dev localhost: if running Vite on 5173 and no env var, assume API on 8080
// 3. Same origin (protocol + hostname) for deployed environment (container apps)
// 4. Final fallback: http://localhost:8080 (only if nothing else resolved)
const rawEnvUrl = import.meta.env.VITE_API_BASE_URL as string | undefined;

function normalize(url: string): string {
  return url.replace(/\/+$/, '');
}

let resolvedBase: string | undefined = rawEnvUrl ? normalize(rawEnvUrl) : undefined;

if (!resolvedBase && typeof window !== 'undefined') {
  const { protocol, hostname, port } = window.location;
  if (hostname === 'localhost' && port === '5173') {
    resolvedBase = 'http://localhost:8080';
  } else {
    // Use same origin (avoid forcing a port; container app will serve on its own mapped port 443 for https)
    resolvedBase = `${protocol}//${hostname}`;
  }
}

// Absolute fallback (should rarely be used now)
if (!resolvedBase) {
  resolvedBase = 'http://localhost:8080';
}

export const http = axios.create({ baseURL: resolvedBase });

export const api = {
  getProfile: () => http.get<Profile>('/api/profile').then((r) => r.data),
  getProjects: () => http.get<Project[]>('/api/projects').then((r) => r.data),
  getExperience: () => http.get<Experience[]>('/api/experience').then((r) => r.data),
  getSkills: () => http.get<Skill[]>('/api/skills').then((r) => r.data),
};

export type { Profile, Project, Experience, Skill };
