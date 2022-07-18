<script setup lang="ts">
import type { CheckboxElementProps } from "@/interfaces/checkboxElementProps";

withDefaults(defineProps<CheckboxElementProps>(), {
  hasError: false,
});
defineEmits(["update:modelValue"]);
</script>

<template>
  <div class="field-checkbox">
    <Checkbox
      :id="id"
      :model-value="modelValue"
      @input="$emit('update:modelValue', $event)"
      :binary="true"
      :class="{ 'p-invalid': hasError }"
    />

    <label :for="id" :class="{ 'p-error': hasError }">{{ label }}</label>
  </div>
</template>
