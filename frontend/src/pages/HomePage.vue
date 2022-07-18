<script setup lang="ts">
import useToken from "@/api/useToken";

const { token } = useToken();
</script>

<template>
  <Tickets v-if="token" />
  <Banner v-else />
  <Discounts />
  <Partners />
</template>
