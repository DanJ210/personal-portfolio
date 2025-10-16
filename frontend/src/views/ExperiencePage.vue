<template>
  <LayoutShell title="Tabitha Portfolio">
    <h2>Experience</h2>
    <section v-if="loading">Loading...</section>
    <section v-else-if="error">Error: {{ error }}</section>
    <section v-else>
      <ExperienceItem v-for="e in experience" :key="e.id" :item="e" />
    </section>
  </LayoutShell>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api, type Experience } from '../services/api';
import LayoutShell from '../components/LayoutShell.vue';
import ExperienceItem from '../components/ExperienceItem.vue';

const experience = ref<Experience[]>([]);
const loading = ref(true);
const error = ref('');

onMounted(async () => {
  try { experience.value = await api.getExperience(); }
  catch (e:any) { error.value = e.message || 'Failed'; }
  finally { loading.value = false; }
});
</script>
