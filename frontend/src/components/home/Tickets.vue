<script setup lang="ts">
import { getOrders } from "@/api/home";
import { handleResponse } from "@/api/useAPI";
import { onMounted } from "vue";

const { execute: fetchOrders, data, isFetching, statusCode } = getOrders();

onMounted(async () => {
  await fetchOrders();
  handleResponse(statusCode.value, data.value);
});
</script>

<template>
  <div
    class="flex flex-column p-6 hover:surface-ground transition-duration-300"
  >
    <h1 class="font-bold text-blue-600 mb-3">My Tickets</h1>
    <Spinner v-if="isFetching" />

    <div v-else-if="data" class="mb-3">
      <div v-if="data.orders.length > 0" class="flex flex-wrap">
        <Ticket v-for="order in data.orders" :key="order.id" v-bind="order" />
      </div>

      <div v-else class="text-lg font-semibold">
        You haven't booked any flights yet
      </div>
    </div>

    <div class="align-self-center">
      <RouterButton to="/booking"> Book your flight now </RouterButton>
    </div>
  </div>
</template>
