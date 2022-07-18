<script setup lang="ts">
import type { RouterProps } from "@/interfaces/routerProps";
defineProps<RouterProps>();
</script>

<template>
  <RouterLink :to="to" class="no-underline text-center">
    <slot />
  </RouterLink>
</template>
