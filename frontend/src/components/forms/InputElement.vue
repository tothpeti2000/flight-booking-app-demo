<script setup lang="ts">
import type { InputElementProps } from "@/interfaces/inputElementProps";

withDefaults(defineProps<InputElementProps>(), {
  iconName: "",
  hasError: false,
  errors: undefined,
});
</script>

<template>
  <div class="mb-4">
    <div class="p-float-label p-input-icon-left">
      <slot />
      <i :class="`pi pi-${iconName}`" />
      <label>{{ label }}</label>
    </div>

    <div v-if="hasError">
      <div v-for="(error, index) of errors" :key="index">
        <p class="p-error">{{ error.$message }}</p>
      </div>
    </div>
  </div>
</template>
