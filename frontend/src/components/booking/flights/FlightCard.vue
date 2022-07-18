<script setup lang="ts">
import type { FlightCardProps } from "@/interfaces/booking/flightCardProps";
import { useBookingStore } from "@/store/store";
import moment from "moment";
import { computed } from "vue";

const props = withDefaults(defineProps<FlightCardProps>(), {
  isReturn: false,
});

const { toFlight, returnFlight, saveFlightChoice } = useBookingStore()!;

const isChosen = computed(() => {
  return (
    props.flight.flightId === toFlight.value?.flightId ||
    props.flight.flightId === returnFlight.value?.flightId
  );
});

const bgColor = computed(() => {
  return isChosen.value ? "bg-primary" : "surface-card";
});

const flightDate = moment(props.flight.departureTime).format("LLLL");
const departureDate = moment(props.flight.departureTime).format("LT");
const arrivalDate = moment(props.flight.arrivalTime).format("LT");

const flightTime = computed(() => {
  const hours = Math.floor(props.flight.flightTimeMinutes / 60);
  const minutes = props.flight.flightTimeMinutes % 60;

  return `${hours}h ${minutes}m`;
});

const handleClick = () => {
  const toSave = isChosen.value ? undefined : props.flight;

  saveFlightChoice(toSave, props.isReturn);
};
</script>

<template>
  <Card @click="handleClick" :class="bgColor">
    <template #content>
      <div>{{ flightDate }}</div>

      <div class="flex">
        <div class="flex flex-column">
          <div class="text-5xl font-semibold">
            {{ departureDate }}
          </div>
          <div class="text-xl align-self-start">
            {{ flight.departureCity }}
          </div>
        </div>

        <div
          class="flex flex-column justify-content-center align-items-center flex-1"
        >
          {{ flightTime }}
          <i class="pi pi-arrow-right" />
        </div>

        <div class="flex flex-column">
          <div class="text-5xl font-semibold">{{ arrivalDate }}</div>
          <div class="text-xl align-self-end">
            {{ flight.arrivalCity }}
          </div>
        </div>

        <div class="flex flex-column justify-content-center">
          <div
            class="bg-blue-400 text-white ml-4 p-2 text-2xl font-semibold border-round-md"
          >
            {{ flight.price }} Ft
          </div>
        </div>
      </div>
    </template>
  </Card>
</template>
