<template>
  <article class="project-card">
    <header>
      <h3>{{ project.name }}</h3>
      <small>{{ techList }}</small>
    </header>
    <p>{{ project.description }}</p>
    <ul>
      <li v-for="h in project.highlights" :key="h">{{ h }}</li>
    </ul>
    <p class="links">
      <a v-if="project.repoUrl" :href="project.repoUrl" target="_blank" rel="noopener">Repo</a>
      <a v-if="project.demoUrl" :href="project.demoUrl" target="_blank" rel="noopener">Demo</a>
    </p>
  </article>
</template>

<script setup lang="ts">
import type { Project } from '../types/portfolio';
const props = defineProps<{ project: Project }>();
const techList = props.project.technologies.join(', ');
</script>

<style scoped>
.project-card { border:1px solid var(--border); padding:1rem 1.25rem; border-radius:8px; background:var(--surface); display:flex; flex-direction:column; gap:0.5rem; }
h3 { margin:0; font-size:1.1rem; }
ul { margin:0; padding-left:1.1rem; }
.links a { margin-right:0.75rem; font-size:0.875rem; }
</style>
