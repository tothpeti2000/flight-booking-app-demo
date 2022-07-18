<script setup lang="ts">
import useUser from "@/api/useUser";

const { logout } = useUser();
</script>

<template>
  <nav
    class="flex justify-content-between align-items-center sticky top-0 z-5 bg-white px-5 py-2 shadow-2"
  >
    <RouterText to="/"
      ><div class="text-4xl text-blue-600 font-bold">
        FlyTonight
      </div></RouterText
    >

    <div class="flex align-items-center gap-5">
      <RouterText to="/#tickets"
        ><div class="text-xl text-color">My Tickets</div></RouterText
      >
      <Button @click="logout">Log out</Button>
    </div>
  </nav>
</template>
