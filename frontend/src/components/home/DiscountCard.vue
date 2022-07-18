<script setup lang="ts">
import type { Discount } from "@/interfaces/home/discount";

defineProps<Discount>();
</script>

<template>
  <Card
    class="w-2 text-center overflow-hidden shadow-4 hover:shadow-6 transition-duration-500"
  >
    <template #header>
      <div>
        <img :src="imgUrlFrom" alt="Flight" />
        <img :src="imgUrlTo" alt="Flight" />
      </div>
    </template>

    <template #title>
      {{ fromCity }} <i class="pi pi-arrow-right" /> {{ toCity }}
    </template>

    <template #content
      ><div class="flex justify-content-between align-items-center">
        <div class="text-sm font-semibold">
          Only for <span class="text-xl p-tag">{{ price }} Ft</span>
        </div>
      </div>
    </template>
  </Card>
</template>
