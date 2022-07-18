import useVuelidate from "@vuelidate/core";
import { reactive } from "vue";
import type { RegistrationFormData } from "../../../interfaces/registrationFormData";
import {
  containsCapital,
  containsNumber,
  emailField,
  maxCharacters,
  minCharacters,
  requiredField,
} from "../validationUtils";

const useRegister = () => {
  const data = reactive<RegistrationFormData>({
    firstName: "",
    lastName: "",
    email: "",
    gender: undefined,
    birthDate: undefined,
    phone: "",
    nationality: "",
    password: "",
    privacyPolicyAccepted: undefined,
    termsAndUseAccepted: undefined,
    newsletterSubscription: false,
  });

  const schema = {
    firstName: { requiredField },
    lastName: { requiredField },
    email: { requiredField, emailField },
    gender: { requiredField },
    birthDate: { requiredField },
    phone: { requiredField },
    nationality: { requiredField },
    password: {
      requiredField,
      minLength: minCharacters(7),
      maxLength: maxCharacters(16),
      containsCapital,
      containsNumber,
    },
    privacyPolicyAccepted: { requiredField },
    termsAndUseAccepted: { requiredField },
  };

  const v$ = useVuelidate(schema, data);

  return {
    data,
    v$,
  };
};

export default useRegister;
