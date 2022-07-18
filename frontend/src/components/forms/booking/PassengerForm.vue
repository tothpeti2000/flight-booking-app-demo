<script setup lang="ts">
import { ticketTypes } from "@/interfaces/booking/order";
import { useBookingStore } from "@/store/store";
import usePassengers from "./usePassengers";

const { toFlight, returnFlight } = useBookingStore()!;

const {
  data: passengerData,
  v$,
  dataReturn: passengerDataReturn,
  vReturn$,
} = usePassengers();

const validateForm = v$.value.$validate;
const validateReturnForm = vReturn$.value.$validate;

defineExpose({
  passengerData,
  passengerDataReturn,
  validateForm,
  validateReturnForm,
});
</script>

<template>
  <InputElement
    icon-name="user"
    label="First Name"
    :has-error="v$.firstName.$invalid"
    :errors="v$.firstName.$errors"
  >
    <InputText type="text" id="firstName" v-model="v$.firstName.$model" />
  </InputElement>

  <InputElement
    icon-name="user"
    label="Last Name"
    :has-error="v$.lastName.$invalid"
    :errors="v$.lastName.$errors"
  >
    <InputText type="text" id="lastName" v-model="v$.lastName.$model" />
  </InputElement>

  <div class="flex justify-content-between">
    <div>
      <h3 class="mb-4 font-semibold">
        {{ toFlight?.departureCity }} <i class="pi pi-arrow-right" />
        {{ toFlight?.arrivalCity }}
      </h3>

      <InputElement
        label="Ticket type"
        :has-error="v$.type.$invalid"
        :errors="v$.type.$errors"
      >
        <Dropdown
          id="ticket"
          :options="ticketTypes"
          optionLabel="label"
          optionValue="label"
          v-model="v$.type.$model"
        />
      </InputElement>

      <CheckboxElement
        id="luggage"
        v-model="passengerData.isLuggage"
        label="I want to take a luggage with me"
      />
    </div>

    <Divider v-if="returnFlight" layout="vertical" />

    <div v-if="returnFlight">
      <h3 class="mb-4 font-semibold">
        {{ returnFlight.departureCity }} <i class="pi pi-arrow-right" />
        {{ returnFlight.arrivalCity }}
      </h3>

      <InputElement
        label="Ticket type"
        :has-error="vReturn$.type.$invalid"
        :errors="vReturn$.type.$errors"
      >
        <Dropdown
          id="returnTicket"
          :options="ticketTypes"
          optionLabel="label"
          optionValue="label"
          v-model="vReturn$.type.$model"
        />
      </InputElement>

      <CheckboxElement
        id="luggage"
        v-model="passengerDataReturn.isLuggage"
        label="I want to take a luggage with me"
      />
    </div>
  </div>
</template>
