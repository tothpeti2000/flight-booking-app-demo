<script setup lang="ts">
import type { StepButtonsProps } from "@/interfaces/booking/stepButtonsProps";
import { computed } from "vue";

const props = defineProps<StepButtonsProps>();

const justify = computed(() => {
  const option =
    props.direction === "next"
      ? "end"
      : props.direction === "back"
      ? "start"
      : "between";

  return `justify-content-${option}`;
});
</script>

<template>
  <div class="flex flex-1" :class="justify">
    <Button
      v-if="direction !== 'next'"
      label="Back"
      @click="$emit('prev-page', { pageIdx: idx })"
    />

    <Button
      v-if="direction !== 'back'"
      label="Next"
      @click="$emit('next-page', { pageIdx: idx })"
    />
  </div>
</template>
