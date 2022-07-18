import type { LoginFormData } from "@/interfaces/loginFormData";
import useVuelidate from "@vuelidate/core";
import { reactive } from "vue";
import { emailField, requiredField } from "../validationUtils";

const useLogin = () => {
  const data = reactive<LoginFormData>({
    email: "",
    password: "",
  });

  const schema = {
    email: { requiredField, emailField },
    password: { requiredField },
  };

  const v$ = useVuelidate(schema, data);

  return {
    data,
    v$,
  };
};

export default useLogin;
