import { createRouter, createWebHistory } from 'vue-router';
import type { RouteRecordRaw } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import ProjectsPage from '../views/ProjectsPage.vue';
import ExperiencePage from '../views/ExperiencePage.vue';
import ContactPage from '../views/ContactPage.vue';

const routes: RouteRecordRaw[] = [
  { path: '/', name: 'home', component: HomePage },
  { path: '/projects', name: 'projects', component: ProjectsPage },
  { path: '/experience', name: 'experience', component: ExperiencePage },
  { path: '/contact', name: 'contact', component: ContactPage },
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() { return { top: 0 }; }
});

export default router;
