<template>
  <main class="flex justify-content-center align-items-center h-screen p-fluid">
    <Card class="w-3 border-round-lg shadow-5">
      <template #content>
        <div class="flex flex-column">
          <LoginForm class="mb-5" />
          <RouterText to="/recover">Forgot password?</RouterText>
          <RouterButton to="/register"> Create new account </RouterButton>
        </div>
      </template>
    </Card>
  </main>
</template>
