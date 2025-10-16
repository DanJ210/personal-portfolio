<template>
  <LayoutShell title="Tabitha Ryan">
    <section v-if="loading">Loading...</section>
    <section v-else-if="error">Error: {{ error }}</section>
    <section v-else class="home">
      <!-- Hero -->
      <header class="hero">
        <div class="hero-text">
          <h1 class="name">Tabitha Ryan</h1>
          <h2 class="role">Senior Software Engineer</h2>
          <p class="tagline">Building resilient, user‑focused web platforms with modern .NET and Vue ecosystems.</p>
          <div class="contact-buttons">
            <button type="button" class="btn outline" @click="copyPhone">📞 Phone</button>
            <a :href="`mailto:${profile?.email}`" class="btn primary">✉️ Email</a>
            <a v-for="l in linkedInLink" :key="l" :href="l" target="_blank" rel="noopener" class="btn subtle">in LinkedIn</a>
          </div>
        </div>
        <div class="hero-side">
          <div class="about-box">
            <h3>About Me</h3>
            <p>{{ aboutSummary }}</p>
          </div>
          <div class="docs-box">
            <h3>Important Documents</h3>
            <ul class="docs-list">
              <li v-for="d in documents" :key="d.file">
                <a :href="d.href" download>{{ d.label }}</a>
              </li>
            </ul>
          </div>
        </div>
      </header>

      <!-- Skills -->
      <section class="skills-section">
        <h3 class="section-title">Core Skills</h3>
        <div class="skill-cloud">
          <SkillTag v-for="s in topSkills" :key="s.name" :skill="s" />
        </div>
      </section>
    </section>
  </LayoutShell>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { api, type Profile, type Skill } from '../services/api';
import LayoutShell from '../components/LayoutShell.vue';
import SkillTag from '../components/SkillTag.vue';

const profile = ref<Profile | null>(null);
const skills = ref<Skill[]>([]);
const loading = ref(true);
const error = ref('');

onMounted(async () => {
  try {
    const [p, sk] = await Promise.all([api.getProfile(), api.getSkills()]);
    profile.value = p;
    skills.value = sk;
  } catch (e:any) {
    error.value = e.message || 'Failed';
  } finally {
    loading.value = false;
  }
});

const topSkills = computed(() => skills.value.slice(0, 16));

// Placeholder About summary (replace later via API or CMS)
const aboutSummary = 'Seasoned engineer with a focus on crafting maintainable architectures, enabling teams, and delivering measurable business impact across cloud-native and frontend ecosystems.';

// Placeholder documents list (will map to /public/docs later)
const documents = [
  { label: 'Resume (PDF)', file: 'resume.pdf', href: '/docs/resume.pdf' },
  { label: 'Case Study: System Redesign', file: 'case-study.pdf', href: '/docs/case-study.pdf' },
  { label: 'Reference Letters', file: 'references.pdf', href: '/docs/references.pdf' }
];

// Extract LinkedIn link if present
const linkedInLink = computed(() => (profile.value?.links || []).filter(l => /linkedin/i.test(l)));

const copyPhone = () => {
  const phone = '+1 (555) 123-4567'; // placeholder
  navigator.clipboard?.writeText(phone);
  copied.value = true;
  setTimeout(() => copied.value = false, 2000);
};
const copied = ref(false);
</script>

<style scoped>
/* Layout */
.home { display:flex; flex-direction:column; gap:3rem; }
.hero { display:grid; grid-template-columns: 1fr 380px; gap:2.5rem; align-items:start; }
@media (max-width: 1000px){ .hero { grid-template-columns:1fr; } }

/* Hero */
.name { margin:0 0 .25rem; font-size:clamp(2rem,5vw,3rem); line-height:1.05; }
.role { margin:.25rem 0 1rem; font-weight:500; color:var(--accent); font-size:clamp(1.25rem,2.5vw,1.75rem); }
.tagline { max-width:55ch; line-height:1.4; }

.contact-buttons { display:flex; flex-wrap:wrap; gap:.75rem; margin-top:1.25rem; }
.btn { cursor:pointer; border:1px solid var(--border); background:var(--surface); color:var(--text); padding:.6rem 1rem; border-radius:6px; font-size:.9rem; display:inline-flex; align-items:center; gap:.35rem; text-decoration:none; }
.btn.primary { background:var(--accent); color:#03182b; border-color:var(--accent); font-weight:600; }
.btn.outline { background:transparent; }
.btn.subtle { background:#19232d; }
.btn:hover { filter:brightness(1.15); }

/* Side boxes */
.hero-side { display:flex; flex-direction:column; gap:1.5rem; }
.about-box, .docs-box { background:var(--surface); padding:1.25rem 1.25rem 1.4rem; border:1px solid var(--border); border-radius:10px; }
.about-box h3, .docs-box h3 { margin:0 0 .75rem; font-size:1rem; letter-spacing:.5px; text-transform:uppercase; opacity:.85; }
.docs-list { list-style:none; margin:0; padding:0; display:flex; flex-direction:column; gap:.5rem; }
.docs-list a { text-decoration:none; font-size:.9rem; }
.docs-list a::before { content:'📄 '; }

/* Skills */
.skills-section { display:flex; flex-direction:column; gap:1rem; }
.section-title { margin:0; font-size:1rem; letter-spacing:.5px; text-transform:uppercase; opacity:.8; }
.skill-cloud { display:flex; flex-wrap:wrap; gap:0.5rem; }

/* Feedback */
.copied-toast { position:fixed; bottom:1rem; right:1rem; background:var(--accent); color:#03182b; padding:.6rem 1rem; border-radius:6px; font-weight:600; box-shadow:0 4px 16px -4px rgba(0,0,0,.4); }
</style>
