<script setup lang="ts">
import type { RouterProps } from "@/interfaces/routerProps";
defineProps<RouterProps>();
</script>

<template>
  <RouterLink :to="to" class="no-underline">
    <Button class="p-button-lg justify-content-center">
      <slot />
    </Button>
  </RouterLink>
</template>
