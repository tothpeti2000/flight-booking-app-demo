<script setup lang="ts">
import { getDiscounts } from "@/api/home";
import { handleResponse } from "@/api/useAPI";
import { onMounted } from "vue";

const { execute, data, isFetching, statusCode } = getDiscounts();

onMounted(async () => {
  await execute();
  handleResponse(statusCode.value, data.value);
  console.log(data.value);
});
</script>

<template>
  <div class="p-6 hover:surface-card transition-duration-300">
    <header class="mb-5">
      <h1 class="font-bold text-blue-600">LOOKING FOR CHEAPER FLIGHTS?</h1>
      <h1 class="font-bold text-blue-600">We've got you covered</h1>
    </header>

    <Spinner v-if="isFetching" />

    <div v-else class="flex flex-wrap gap-5">
      <DiscountCard
        v-for="discount in data"
        :key="discount.flightId"
        v-bind="discount"
      />
    </div>
  </div>
</template>
