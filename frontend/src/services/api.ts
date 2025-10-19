import axios from 'axios';
import type { Profile, Project, Experience, Skill } from '../types/portfolio';

// Determine API base URL:
// 1. Explicit env var VITE_API_BASE_URL (set at build time)
// 2. If running in browser and host includes a port, reuse host and assume API is same port when single container.
// 3. Fallback to http://localhost:8080.
const envUrl = import.meta.env.VITE_API_BASE_URL as string | undefined;
let dynamicUrl: string | undefined = undefined;
if (typeof window !== 'undefined') {
  const { protocol, hostname } = window.location;
  // If frontend served on 5173 (dev) still want 5000 or override; in production we mapped API to 8080.
  // You could refine by checking a custom window.__API_PORT injected at runtime.
  dynamicUrl = `${protocol}//${hostname}:8080`;
}
const baseURL = envUrl || dynamicUrl || 'http://localhost:8080';

export const http = axios.create({ baseURL });

export const api = {
  getProfile: () => http.get<Profile>('/api/profile').then((r) => r.data),
  getProjects: () => http.get<Project[]>('/api/projects').then((r) => r.data),
  getExperience: () => http.get<Experience[]>('/api/experience').then((r) => r.data),
  getSkills: () => http.get<Skill[]>('/api/skills').then((r) => r.data),
};

export type { Profile, Project, Experience, Skill };
