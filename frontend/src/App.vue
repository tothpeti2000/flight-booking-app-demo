<script setup lang="ts">
import useToken from "./api/useToken";

const { token } = useToken();
</script>

<template>
  <NavBar v-if="token" />
  <RouterView />
</template>
