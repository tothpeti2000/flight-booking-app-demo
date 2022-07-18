<script setup lang="ts">
import { getFlights } from "@/api/booking";
import { handleResponse } from "@/api/useAPI";
import useFeedback from "@/components/useFeedback";
import type { FlightData } from "@/interfaces/booking/flights";
import { useBookingStore } from "@/store/store";
import { onActivated, ref } from "vue";

const { toFlight, bookingOptions } = useBookingStore()!;
const { showError } = useFeedback();

const flightData = ref<FlightData | null>();
const fetching = ref(true);

const emit = defineEmits(["prev-page", "next-page"]);

const validate = () => {
  if (!toFlight.value) {
    showError("A flight must be chosen");
  } else {
    emit("next-page", { pageIdx: 1 });
  }
};

onActivated(async () => {
  const {
    execute: fetchFlights,
    data,
    statusCode,
  } = getFlights(bookingOptions.value);

  await fetchFlights();
  handleResponse(statusCode.value, data.value);

  fetching.value = false;
  flightData.value = data.value;
});
</script>

<template>
  <Spinner v-if="fetching" />

  <div v-else class="mb-5">
    <FlightList :flights="flightData?.toFlights" />
    <SectionDivider v-if="flightData?.returnFlights" />
    <FlightList :flights="flightData?.returnFlights" isReturn />
  </div>

  <StepButtons
    @prevPage="$emit('prev-page', { pageIdx: 1 })"
    @nextPage="validate"
    :idx="1"
    direction="both"
  />
</template>
