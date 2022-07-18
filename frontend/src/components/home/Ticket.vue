<script setup lang="ts">
import type { Order } from "@/interfaces/home/orders";
import { computed } from "vue";

const props = defineProps<Order>();

const toFlightDeparture = computed(() => {
  return new Date(props.toFlight.departure).toLocaleString();
});

const returnFlightDeparture = computed(() => {
  return props.returnFlight
    ? new Date(props.returnFlight.departure).toLocaleString()
    : null;
});
</script>

<template>
  <div class="flex flex-column p-2 m-5 hover:shadow-5 transition-duration-300">
    <div class="flex align-items-center mb-2">
      <div
        class="p-3 bg-white hover:bg-primary hover:text-white transition-duration-300"
      >
        <div class="text-2xl">
          <span class="font-bold">{{ toFlight.fromCity }}</span>
          <i class="pi pi-arrow-right mx-1" />
          <span class="font-bold">{{ toFlight.toCity }}</span>
        </div>

        <div class="text-xl">{{ toFlightDeparture }}</div>

        <div v-if="toFlightStatus" class="text-xl">
          Status: <span class="text-red-500">{{ toFlightStatus }}</span>
        </div>
      </div>

      <i v-if="returnFlight" class="pi pi-arrows-h mx-2" />

      <div
        v-if="returnFlight"
        class="p-3 bg-white hover:bg-primary hover:text-white transition-duration-300"
      >
        <div class="text-2xl">
          <span class="font-bold">{{ returnFlight.fromCity }}</span>
          <i class="pi pi-arrow-right mx-1" />
          <span class="font-bold">{{ returnFlight.toCity }}</span>
        </div>

        <div class="text-xl">{{ returnFlightDeparture }}</div>

        <div v-if="toFlightStatus" class="text-xl">
          Status: <span class="text-red-500">{{ toFlightStatus }}</span>
        </div>
      </div>
    </div>

    <div class="align-self-center">
      <div class="text-lg font-semibold mb-2">
        Passengers: {{ passengerCount }}
      </div>

      <span class="text-xl p-tag">{{ price }} Ft</span>
    </div>
  </div>
</template>
