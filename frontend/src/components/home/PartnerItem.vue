<script setup lang="ts">
interface Partner {
  name: string;
  blobUrl: string;
}

defineProps<Partner>();
</script>

<template>
  <div
    class="flex flex-column justify-content-between p-2 w-1 text-center bg-white shadow-2 hover:shadow-6 transition-duration-300"
  >
    <img :src="blobUrl" alt="Partner" />

    <div>
      <Divider />
      <h3
        class="font-bold hover:bg-primary hover:text-white transition-duration-300"
      >
        {{ name }}
      </h3>
    </div>
  </div>
</template>
