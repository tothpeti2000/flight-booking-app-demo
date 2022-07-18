<script setup lang="ts">
import { useBookingStore } from "@/store/store";

const { toFlight, returnFlight } = useBookingStore()!;
</script>

<template>
  <h1 class="font-bold text-blue-600 mb-3">Order Summary</h1>

  <section v-if="toFlight">
    <FlightSummary :flight="toFlight" />
  </section>

  <SectionDivider />

  <section v-if="returnFlight">
    <FlightSummary :flight="returnFlight" isReturn />
  </section>
</template>
