<script setup lang="ts">
import type { SeatProps } from "@/interfaces/booking/seatProps";
import type { ColoredSeatFormData } from "@/interfaces/booking/seats";
import { computed, ref } from "vue";

const props = defineProps<SeatProps>();

const isSelected = ref(false);
const passengerColor = ref("");

const bgColor = computed(() => {
  return props.isBooked
    ? "p-button-danger"
    : isSelected.value
    ? passengerColor.value
    : "bg-white";
});

const isDisabled = computed(() => props.isBooked);

const handleClick = (passengers: ColoredSeatFormData[]) => {
  isSelected.value = false;

  passengers.forEach((passenger) => {
    if (passenger.rowNum === props.row && passenger.colNum === props.col) {
      isSelected.value = true;
      passengerColor.value = passenger.color;
    }
  });
};

defineExpose({ handleClick });
</script>

<template>
  <Button class="m-1" :class="bgColor" :disabled="isDisabled" />
</template>
