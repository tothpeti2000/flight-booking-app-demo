import type { BookingOptions } from "@/interfaces/booking/start";
import useVuelidate from "@vuelidate/core";
import { minValue } from "@vuelidate/validators";
import moment from "moment";
import { reactive } from "vue";
import { requiredField } from "../validationUtils";

const useSearchFlights = () => {
  const data = reactive<BookingOptions>({
    from: undefined,
    to: undefined,
    departureDate: moment().add(1, "days").toDate(),
    returnDate: undefined,
    passengerCount: 1,
  });

  const schema = {
    from: { requiredField },
    to: { requiredField },
    departureDate: { requiredField },
    passengerCount: { requiredField, minValue: minValue(1) },
  };

  const v$ = useVuelidate(schema, data);

  return {
    data,
    v$,
  };
};

export default useSearchFlights;
