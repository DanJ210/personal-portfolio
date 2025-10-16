export interface Profile {
  fullName: string;
  title: string;
  summary: string;
  email: string;
  location: string;
  links: string[];
  skills: string[];
}

export interface Project {
  id: number;
  name: string;
  description: string;
  technologies: string[];
  repoUrl?: string | null;
  demoUrl?: string | null;
  highlights: string[];
}

export interface Experience {
  id: number;
  company: string;
  role: string;
  location: string;
  startDate: string; // ISO
  endDate?: string | null; // ISO
  achievements: string[];
  technologies: string[];
}

export interface Skill {
  name: string;
  category: string;
  proficiency: number;
}
