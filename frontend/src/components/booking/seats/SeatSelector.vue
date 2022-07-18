<script setup lang="ts">
import { generateRandomColor } from "@/components/forms/booking/seatReservationUtils";
import type { SeatSelectorProps } from "@/interfaces/booking/seatSelectorProps";
import { useBookingStore } from "@/store/store";
import Seat from "./Seat.vue";
import { computed, ref } from "vue";
import type { ColoredSeatFormData } from "@/interfaces/booking/seats";

const props = defineProps<SeatSelectorProps>();

const { passengerDetails } = useBookingStore()!;

const seats = ref<InstanceType<typeof Seat>[] | null>(null);

const passengers = ref<ColoredSeatFormData[]>(
  passengerDetails.value.map((passenger, idx) => {
    return {
      rowNum: undefined,
      colNum: undefined,
      color: generateRandomColor(idx),
    };
  })
);

const currentPassengerColor = computed(() => {
  return currentPassengerIdx.value === undefined
    ? ""
    : passengers.value[currentPassengerIdx.value].color;
});

const currentPassengerIdx = ref<number | undefined>(undefined);

const isSeatBooked = computed(() => (row: number, col: number) => {
  const matches = props.seatData.bookedSeats.filter(
    (bookedSeat) => bookedSeat.rowNum === row && bookedSeat.colNum === col
  );

  return matches.length > 0;
});

const style = computed(() => (idx: number) => {
  const bgColor = generateRandomColor(idx);
  const isSelecting = currentPassengerIdx.value === idx;

  return `${isSelecting && bgColor} ${
    isSelecting && "text-white"
  } hover:text-white hover:${bgColor}`;
});

const handleSeatSelection = (row: number, col: number) => {
  if (currentPassengerIdx.value !== undefined) {
    passengers.value.forEach((passenger) => {
      if (passenger.rowNum === row && passenger.colNum === col) {
        passenger.rowNum = undefined;
        passenger.colNum = undefined;
      }
    });

    passengers.value[currentPassengerIdx.value].rowNum = row;
    passengers.value[currentPassengerIdx.value].colNum = col;

    seats.value?.map((seat) => seat.handleClick(passengers.value));
  }
};

const handlePassengerSelection = (idx: number) => {
  currentPassengerIdx.value =
    currentPassengerIdx.value === idx ? undefined : idx;
};

defineExpose({ passengers });
</script>

<template>
  <div class="flex flex-column">
    <div class="flex justify-content-around">
      <div
        v-for="col in seatData.seatColCount"
        :key="col"
        class="flex flex-column"
      >
        <Seat
          v-for="row in seatData.seatRowCount"
          :key="row"
          :is-booked="isSeatBooked(row, col)"
          :row="row"
          :col="col"
          @click="handleSeatSelection(row, col)"
          ref="seats"
        />
      </div>
    </div>

    <h2
      v-for="(passenger, idx) in passengerDetails"
      :key="idx"
      class="p-2 mb-2 transition-duration-300 cursor-pointer"
      :class="style(idx)"
      @click="handlePassengerSelection(idx)"
    >
      {{ passenger.firstName }} {{ passenger.lastName }}
    </h2>
  </div>
</template>
