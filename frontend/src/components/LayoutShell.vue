<template>
  <div class="app-shell">
    <header class="app-header">
      <h1 class="logo"><router-link to="/">{{ title }}</router-link></h1>
      <nav>
        <RouterLink to="/projects">Projects</RouterLink>
        <RouterLink to="/experience">Experience</RouterLink>
        <RouterLink to="/contact">Contact</RouterLink>
      </nav>
    </header>
    <main class="app-main">
      <slot />
    </main>
    <footer class="app-footer">© {{ new Date().getFullYear() }} {{ title }}</footer>
  </div>
</template>

<script setup lang="ts">
defineProps<{ title: string }>();
</script>

<style scoped>
.app-shell { min-height: 100dvh; display: flex; flex-direction: column; }
.app-header { display:flex; gap:1rem; align-items:center; padding:0.75rem 1.25rem; border-bottom:1px solid var(--border); }
nav { display:flex; gap:1rem; }
nav a { text-decoration:none; font-weight:500; }
nav a.router-link-active { color: var(--accent); }
.app-main { flex:1; width:min(1100px,100%); margin-inline:auto; padding:1.5rem; }
.app-footer { padding:1rem; text-align:center; font-size:0.85rem; opacity:0.7; }
</style>
