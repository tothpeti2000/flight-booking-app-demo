<script setup lang="ts">
import { handleResponse } from "@/api/useAPI";
import useUser from "@/api/useUser";
import useFeedback from "@/components/useFeedback";
import router from "@/router";
import { genders } from "../../../interfaces/registrationFormData";
import useRegister from "./useRegister";

const { data: userData, v$ } = useRegister();
const { register } = useUser();
const { showSuccess } = useFeedback();
const {
  execute: registerUser,
  data,
  isFetching,
  statusCode,
} = register(userData);

const handleSuccessfulRegistration = () => {
  showSuccess("Account created successfully");
  router.push("/login");
};

const handleSubmit = async () => {
  const isValid = await v$.value.$validate();

  if (isValid) {
    await registerUser();
    handleResponse(statusCode.value, data.value, handleSuccessfulRegistration);
  }
};
</script>

<template>
  <form @submit.prevent="handleSubmit">
    <InputElement
      icon-name="user"
      label="First Name"
      :has-error="v$.firstName.$invalid"
      :errors="v$.firstName.$errors"
    >
      <InputText type="text" id="firstName" v-model="v$.firstName.$model" />
    </InputElement>

    <InputElement
      icon-name="user"
      label="Last Name"
      :has-error="v$.lastName.$invalid"
      :errors="v$.lastName.$errors"
    >
      <InputText type="text" id="lastName" v-model="v$.lastName.$model" />
    </InputElement>

    <InputElement
      icon-name="envelope"
      label="Email"
      :has-error="v$.email.$invalid"
      :errors="v$.email.$errors"
    >
      <InputText type="email" id="email" v-model="v$.email.$model" />
    </InputElement>

    <InputElement
      label="Gender"
      :has-error="v$.gender.$invalid"
      :errors="v$.gender.$errors"
    >
      <Dropdown
        id="gender"
        :options="genders"
        optionLabel="label"
        optionValue="label"
        v-model="v$.gender.$model"
      />
    </InputElement>

    <InputElement
      label="Date of birth"
      :has-error="v$.birthDate.$invalid"
      :errors="v$.birthDate.$errors"
    >
      <Calendar id="birthDate" :showIcon="true" v-model="v$.birthDate.$model" />
    </InputElement>

    <InputElement
      icon-name="phone"
      label="Phone Number"
      :has-error="v$.phone.$invalid"
      :errors="v$.phone.$errors"
    >
      <InputText type="text" id="phone" v-model="v$.phone.$model" />
    </InputElement>

    <InputElement
      icon-name="flag"
      label="Nationality"
      :has-error="v$.nationality.$invalid"
      :errors="v$.nationality.$errors"
    >
      <InputText type="text" id="nationality" v-model="v$.nationality.$model" />
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

    <CheckboxElement
      id="privacyPolicy"
      v-model="v$.privacyPolicyAccepted.$model"
      label="I accept the privacy policy"
      :has-error="v$.privacyPolicyAccepted.$error"
    />

    <CheckboxElement
      id="termsAndUse"
      v-model="v$.termsAndUseAccepted.$model"
      label="I agree to the terms and conditions"
      :has-error="v$.termsAndUseAccepted.$error"
    />

    <CheckboxElement
      id="newsletter"
      v-model="userData.newsletterSubscription"
      label="Subscribe to our newsletter"
    />

    <Button type="submit" label="Submit" :loading="isFetching" />
  </form>
</template>
