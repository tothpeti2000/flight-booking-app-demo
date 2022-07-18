<script setup lang="ts">
import { handleResponse } from "@/api/useAPI";
import useToken from "@/api/useToken";
import useUser from "@/api/useUser";
import useFeedback from "@/components/useFeedback";
import router from "@/router";
import useLogin from "./useLogin";

const { data: userCredentials, v$ } = useLogin();
const { saveToken } = useToken();
const { login } = useUser();
const { showSuccess } = useFeedback();
const {
  execute: loginUser,
  data,
  isFetching,
  statusCode,
} = login(userCredentials);

const handleSuccessfulLogin = () => {
  showSuccess("Sucessfully logged in");
  data.value?.token && saveToken(data.value.token);

  router.push(router.currentRoute.value.redirectedFrom ?? "/");
};

const handleSubmit = async () => {
  const isValid = await v$.value.$validate();

  if (isValid) {
    await loginUser();
    handleResponse(statusCode.value, data.value, handleSuccessfulLogin);
  }
};
</script>

<template>
  <form @submit.prevent="handleSubmit">
    <InputElement
      icon-name="envelope"
      label="Email"
      :has-error="v$.email.$invalid"
      :errors="v$.email.$errors"
    >
      <InputText type="email" id="email" v-model="v$.email.$model" />
    </InputElement>

    <InputElement
      label="Password"
      :has-error="v$.password.$invalid"
      :errors="v$.password.$errors"
    >
      <Password
        id="password"
        v-model="v$.password.$model"
        :feedback="false"
        toggleMask
      />
    </InputElement>

    <Button type="submit" label="Log In" :loading="isFetching" />
  </form>
</template>
