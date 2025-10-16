<template>
  <LayoutShell title="Tabitha Portfolio">
    <h2>Contact</h2>
    <p>You can reach me at <a :href="`mailto:${profile?.email}`">{{ profile?.email }}</a>.</p>
    <p>Links:</p>
    <ul>
      <li v-for="l in profile?.links" :key="l"><a :href="l" target="_blank" rel="noopener">{{ l }}</a></li>
    </ul>
  </LayoutShell>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api, type Profile } from '../services/api';
import LayoutShell from '../components/LayoutShell.vue';
const profile = ref<Profile | null>(null);
onMounted(async () => { profile.value = await api.getProfile(); });
</script>
