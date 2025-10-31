# Portfolio Documents

This folder is published verbatim at `/docs/*` by the frontend build (Vite `public/` passthrough). Any file you place here becomes publicly accessible at `https://<your-site>/docs/<filename>`.

## Current Files
- `cover-letter-notable.docx` – Cover letter (Notable variation) – Added 2025-10-29
- `letter-of-recommendation.pdf` – Professional recommendation letter – Added 2025-10-29

## Conventions
- Prefer lowercase, hyphen-separated filenames.
- Avoid spaces; they make URLs harder to share.
- Do NOT store private / sensitive personal data here (these files are public once deployed).

## Updating
1. Replace or add documents as needed.
2. Ensure references in `src/views/HomePage.vue` (documents array) match the exact filenames.
3. Commit and deploy.

## Removal / Expiry
If a document should no longer be accessible, remove it from this folder and invalidate any CDN caches if your host uses one.

## Robots Exclusion
`robots.txt` is configured to disallow crawling of `/docs/` to reduce casual indexing. This is NOT security—files remain publicly reachable.

---
_Last updated: 2025-10-29_
