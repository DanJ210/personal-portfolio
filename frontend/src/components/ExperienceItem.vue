<template>
  <div class="exp-item">
    <div class="exp-head">
      <h3>{{ item.role }} – {{ item.company }}</h3>
      <small>{{ dateRange }}</small>
    </div>
    <p class="tech">{{ item.technologies.join(', ') }}</p>
    <ul>
      <li v-for="a in item.achievements" :key="a">{{ a }}</li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import type { Experience } from '../types/portfolio';
const props = defineProps<{ item: Experience }>();
const dateRange = `${props.item.startDate} – ${props.item.endDate ?? 'Present'}`;
</script>

<style scoped>
.exp-item { padding:1rem 0; border-bottom:1px solid var(--border); }
.exp-item:last-child { border-bottom:none; }
h3 { margin:0 0 0.25rem; font-size:1rem; }
ul { margin:0.25rem 0 0; padding-left:1.15rem; }
.tech { font-size:0.8rem; opacity:0.7; margin:0; }
</style>
