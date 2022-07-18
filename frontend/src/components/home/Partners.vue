<script setup lang="ts">
import { getPartners } from "@/api/home";
import { handleResponse } from "@/api/useAPI";
import { onMounted } from "vue";

const { execute, data, isFetching, statusCode } = getPartners();

onMounted(async () => {
  await execute();
  handleResponse(statusCode.value, data.value);
});
</script>

<template>
  <div class="p-6 hover:surface-card transition-duration-300">
    <h1 class="font-bold text-blue-600 mb-5">
      Huge thanks to our supportive partners
    </h1>

    <Spinner v-if="isFetching" />

    <div v-else class="flex gap-5 flex-wrap">
      <PartnerItem
        v-for="partner in data"
        :key="partner.partnerID"
        v-bind="partner"
      />
    </div>
  </div>
</template>
