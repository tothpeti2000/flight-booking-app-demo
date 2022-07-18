<template>
  <header
    class="flex flex-column h-screen justify-content-center align-items-center text-white"
  >
    <h1 class="text-8xl font-bold">FlyTonight</h1>
    <h2 class="text-6xl font-semibold">RISE ABOVE THE CLOUDS WITH US</h2>

    <RouterButton class="mt-3" to="/login"> Sign in / Sign up </RouterButton>
  </header>
</template>

<style scoped>
header {
  background-image: url("../../assets/banner.jpg");
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
