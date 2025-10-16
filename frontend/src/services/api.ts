import axios from 'axios';
import type { Profile, Project, Experience, Skill } from '../types/portfolio';

const baseURL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000';

export const http = axios.create({ baseURL });

export const api = {
  getProfile: () => http.get<Profile>('/api/profile').then(r => r.data),
  getProjects: () => http.get<Project[]>('/api/projects').then(r => r.data),
  getExperience: () => http.get<Experience[]>('/api/experience').then(r => r.data),
  getSkills: () => http.get<Skill[]>('/api/skills').then(r => r.data),
};

export type { Profile, Project, Experience, Skill };
