<template>
  <main>
    <img
      src="../../assets/registration.jpg"
      alt="Register"
      class="w-screen h-screen"
    />

    <div class="flex align-items-center min-h-screen p-5 text-white">
      <section class="text-center flex-1">
        <h1 class="text-7xl">LOOKING FOR FLIGHTS AT AFFORDABLE PRICES?</h1>
        <h2 class="text-5xl">JOIN US NOW</h2>
      </section>

      <section class="flex-1 px-8 p-fluid">
        <RegistrationForm />
      </section>
    </div>
  </main>
</template>

<style scoped>
main img {
  object-fit: cover;
  position: fixed;
  filter: brightness(0.4);
}
</style>
