<script setup lang="ts">
import type { FlightListProps } from "@/interfaces/booking/flightListProps";
import { useBookingStore } from "@/store/store";
import { computed } from "vue";

const props = withDefaults(defineProps<FlightListProps>(), {
  isReturn: false,
});

const { bookingOptions } = useBookingStore()!;

const hasSearchedReturnFlight = computed(
  () => !!bookingOptions.value?.returnDate
);

const title = `${props.isReturn ? "Return" : ""} Flights available`;
</script>

<template>
  <div v-if="(isReturn && hasSearchedReturnFlight) || !isReturn">
    <h1 class="mb-3 font-bold text-blue-600">{{ title }}</h1>

    <div v-if="flights && flights.length > 0">
      <FlightCard
        v-for="flight in flights"
        :key="flight.flightId"
        :flight="flight"
        :isReturn="isReturn"
        class="shadow-2 mb-3 hover:bg-blue-200 cursor-pointer transition-duration-200"
      />
    </div>

    <div v-else>No flights found</div>
  </div>
</template>
