<script setup lang="ts">
import type { TicketsProps } from "@/interfaces/booking/confirmation";
import { computed } from "vue";

const props = defineProps<TicketsProps>();

const hasLuggages = computed(() =>
  props.tickets.map((ticket) => (ticket.isLuggage ? "yes" : "no"))
);
</script>

<template>
  <div class="p-5">
    <h2 class="font-bold text-blue-600 mb-3">Passengers</h2>

    <div v-for="(ticket, idx) in tickets" :key="idx" class="mb-2">
      <h3 class="font-bold">{{ ticket.firstName }} {{ ticket.lastName }}</h3>

      <div class="flex justify-content-around">
        <div>
          <span class="font-semibold">Seat:</span> Row
          <span class="p-tag"> {{ ticket.rowNum }}</span> Column
          <span class="p-tag">
            {{ ticket.colNum }}
          </span>
        </div>

        <Divider layout="vertical" />

        <div><span class="font-semibold">Ticket:</span> {{ ticket.type }}</div>

        <Divider layout="vertical" />

        <div>
          <span class="font-semibold">Luggage:</span> {{ hasLuggages[idx] }}
        </div>
      </div>
    </div>
  </div>
</template>
