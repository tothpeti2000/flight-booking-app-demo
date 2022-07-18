<script setup lang="ts">
import type { StepEvent } from "@/interfaces/booking/steps";
import router from "@/router";
import { useProvideBookingStore } from "@/store/store";
import { ref } from "vue";

useProvideBookingStore();

const steps = ref([
  {
    label: "Start",
    to: "/booking/start",
  },
  {
    label: "Flights",
    to: "/booking/flights",
  },
  {
    label: "Passengers",
    to: "/booking/passengers",
  },
  {
    label: "Seats",
    to: "/booking/seats",
  },
  {
    label: "Confirm",
    to: "/booking/confirm",
  },
]);

const nextPage = (event: StepEvent) => {
  console.log(event);
  router.push(steps.value[event.pageIdx + 1].to);
};

const prevPage = (event: StepEvent) => {
  router.push(steps.value[event.pageIdx - 1].to);
};
</script>

<template>
  <main class="p-5">
    <Card class="mb-5">
      <template #content>
        <Steps :model="steps" />
      </template>
    </Card>

    <Card class="w-6 p-3 mx-auto">
      <template #content>
        <RouterView
          v-slot="{ Component }"
          @prevPage="prevPage($event)"
          @nextPage="nextPage($event)"
        >
          <KeepAlive>
            <component :is="Component" />
          </KeepAlive>
        </RouterView>
      </template>
    </Card>
  </main>
</template>
