import type {
  PassengerDetails,
  PassengerReturnDetails,
} from "@/interfaces/booking/passengers";
import useVuelidate from "@vuelidate/core";
import { reactive } from "vue";
import { requiredField } from "../validationUtils";

const usePassengers = () => {
  const data = reactive<PassengerDetails>({
    firstName: "",
    lastName: "",
    type: undefined,
    isLuggage: false,
  });

  const dataReturn = reactive<PassengerReturnDetails>({
    type: undefined,
    isLuggage: false,
  });

  const schema = {
    firstName: { requiredField },
    lastName: { requiredField },
    type: { requiredField },
  };

  const schemaReturn = {
    type: { requiredField },
  };

  const v$ = useVuelidate(schema, data);
  const vReturn$ = useVuelidate(schemaReturn, dataReturn);

  return {
    data,
    v$,
    dataReturn,
    vReturn$,
  };
};

export default usePassengers;
