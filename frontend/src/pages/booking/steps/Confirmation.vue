<script setup lang="ts">
import { placeOrder } from "@/api/booking";
import { handleResponse } from "@/api/useAPI";
import useFeedback from "@/components/useFeedback";
import router from "@/router";
import { useBookingStore } from "@/store/store";

const { order } = useBookingStore()!;
const { showSuccess } = useFeedback();
const {
  execute: submitOrder,
  isFetching,
  data,
  statusCode,
} = placeOrder(order.value);

const handleSuccessfulOrder = () => {
  showSuccess("Order completed");
  router.push("/#tickets");
};

const handlePlaceOrder = async () => {
  await submitOrder();
  handleResponse(statusCode.value, data.value, handleSuccessfulOrder);
};
</script>

<template>
  <OrderSummary />

  <Button
    label="Place Order"
    class="p-button-success w-full my-5"
    @click="handlePlaceOrder"
    :loading="isFetching"
  />

  <StepButtons
    @prevPage="$emit('prev-page', { pageIdx: 4 })"
    :idx="4"
    direction="back"
  />
</template>
