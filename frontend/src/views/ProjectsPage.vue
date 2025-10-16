<template>
  <LayoutShell title="Tabitha Portfolio">
    <h2>Projects</h2>
    <section v-if="loading">Loading...</section>
    <section v-else-if="error">Error: {{ error }}</section>
    <section v-else class="projects-grid">
      <ProjectCard v-for="p in projects" :key="p.id" :project="p" />
    </section>
  </LayoutShell>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api, type Project } from '../services/api';
import LayoutShell from '../components/LayoutShell.vue';
import ProjectCard from '../components/ProjectCard.vue';

const projects = ref<Project[]>([]);
const loading = ref(true);
const error = ref('');

onMounted(async () => {
  try {
    projects.value = await api.getProjects();
  } catch (e:any) { error.value = e.message || 'Failed'; }
  finally { loading.value = false; }
});
</script>

<style scoped>
.projects-grid { display:grid; gap:1rem; grid-template-columns: repeat(auto-fill,minmax(260px,1fr)); }
</style>
