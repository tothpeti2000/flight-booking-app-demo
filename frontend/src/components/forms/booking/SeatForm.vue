<script setup lang="ts">
import { useBookingStore } from "@/store/store";
import useSeats from "./useSeats";

const { returnFlight } = useBookingStore()!;
const { data, v$, dataReturn, vReturn$ } = useSeats();

const validateForm = v$.value.$validate;
const validateReturnForm = vReturn$.value.$validate;

defineExpose({ data, dataReturn, validateForm, validateReturnForm });
</script>

<template>
  <div class="flex justify-content-between">
    <div>
      <InputElement
        label="Row"
        :has-error="v$.rowNum.$invalid"
        :errors="v$.rowNum.$errors"
      >
        <InputNumber id="seatRow" v-model="v$.rowNum.$model" />
      </InputElement>

      <InputElement
        label="Column"
        :has-error="v$.colNum.$invalid"
        :errors="v$.colNum.$errors"
      >
        <InputNumber id="seatCol" v-model="v$.colNum.$model" />
      </InputElement>
    </div>

    <Divider v-if="returnFlight" layout="vertical" />

    <div v-if="returnFlight">
      <InputElement
        label="Row"
        :has-error="vReturn$.rowNum.$invalid"
        :errors="vReturn$.rowNum.$errors"
      >
        <InputNumber id="seatRowReturn" v-model="vReturn$.rowNum.$model" />
      </InputElement>

      <InputElement
        label="Column"
        :has-error="vReturn$.colNum.$invalid"
        :errors="vReturn$.colNum.$errors"
      >
        <InputNumber id="seatColReturn" v-model="vReturn$.colNum.$model" />
      </InputElement>
    </div>
  </div>
</template>
