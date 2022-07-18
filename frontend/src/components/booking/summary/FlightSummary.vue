<script setup lang="ts">
import type { FlightSummaryProps } from "@/interfaces/booking/flightSummaryProps";
import { useBookingStore } from "@/store/store";
import { computed } from "vue";

const props = withDefaults(defineProps<FlightSummaryProps>(), {
  isReturn: false,
});

const { order } = useBookingStore()!;

const tickets = computed(() => {
  return props.isReturn
    ? order.value.returnFlight?.tickets ?? []
    : order.value.toFlight.tickets;
});
</script>

<template>
  <FlightCard :flight="flight" />
  <TicketsSummary :tickets="tickets" />
</template>
