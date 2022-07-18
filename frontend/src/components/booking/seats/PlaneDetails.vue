<script setup lang="ts">
import type { PlaneDetailsProps } from "@/interfaces/booking/planeDetailsProps";

defineProps<PlaneDetailsProps>();
</script>

<template>
  <div class="flex flex-column align-items-center">
    <h2 class="font-semibold">
      {{ flight?.departureCity }} <i class="pi pi-arrow-right" />
      {{ flight?.arrivalCity }}
    </h2>

    <h3 class="font-medium mb-4">
      Rows: <span class="p-tag">{{ seatData?.seatRowCount }}</span> Columns:
      <span class="p-tag">{{ seatData?.seatColCount }}</span>
    </h3>

    <div v-if="seatData?.bookedSeats && seatData.bookedSeats.length > 0">
      <h2 class="font-semibold">Booked seats</h2>

      <div v-for="(bookedSeat, idx) in seatData.bookedSeats" :key="idx">
        Row <span class="p-tag"> {{ bookedSeat.rowNum }} </span> Column
        <span class="p-tag">{{ bookedSeat.colNum }} </span>
      </div>
    </div>
  </div>
</template>
