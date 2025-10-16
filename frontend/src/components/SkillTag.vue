<template>
  <span class="skill" :title="`Proficiency: ${skill.proficiency}`">{{ skill.name }}</span>
</template>

<script setup lang="ts">
import type { Skill } from '../types/portfolio';
defineProps<{ skill: Skill }>();
</script>

<style scoped>
.skill { background:var(--accent-soft); color:var(--accent-text); padding:0.35rem 0.6rem; border-radius:999px; font-size:0.7rem; letter-spacing:0.5px; text-transform:uppercase; }
</style>
