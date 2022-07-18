<script setup lang="ts">
import { getAirports } from "@/api/booking";
import { handleResponse } from "@/api/useAPI";
import { onActivated } from "vue";

const { execute: fetchAirports, data, isFetching, statusCode } = getAirports();

onActivated(async () => {
  await fetchAirports();
  handleResponse(statusCode.value, data);
});
</script>

<template>
  <KeepAlive>
    <Spinner v-if="isFetching" />
    <SearchFlightsForm v-else :airports="data ?? []" />
  </KeepAlive>
</template>
