import type { Flight } from "@/interfaces/booking/flights";
import type { Order } from "@/interfaces/booking/order";
import type {
  PassengerDetails,
  PassengerReturnDetails,
} from "@/interfaces/booking/passengers";
import type { SeatFormData } from "@/interfaces/booking/seats";
import type { BookingOptions } from "@/interfaces/booking/start";
import { createGlobalState, useSessionStorage } from "@vueuse/core";
import { createInjectionState } from "@vueuse/shared";
import { map, merge, partialRight, pick } from "lodash-es";
import { computed, ref } from "vue";

export const useTokenStore = createGlobalState(() =>
  useSessionStorage<string | null>("token", null)
);

const [useProvideBookingStore, useBookingStore] = createInjectionState(() => {
  const bookingOptions = ref<BookingOptions>();
  const toFlight = ref<Flight>();
  const returnFlight = ref<Flight>();
  const passengerDetails = ref<PassengerDetails[]>([]);
  const passengerReturnDetails = ref<PassengerReturnDetails[]>([]);
  const seatReservations = ref<SeatFormData[]>([]);
  const seatReturnReservations = ref<SeatFormData[]>([]);
  const order = computed(() => getOrder());

  const saveBookingOptions = (options: BookingOptions) => {
    bookingOptions.value = options;
  };

  const saveFlightChoice = (flight: Flight | undefined, isReturn: boolean) => {
    isReturn ? (returnFlight.value = flight) : (toFlight.value = flight);
  };

  const savePassengerDetails = (details: PassengerDetails[] | undefined) => {
    details && (passengerDetails.value = details);
  };

  const savePassengerReturnDetails = (
    details: PassengerReturnDetails[] | undefined
  ) => {
    details && (passengerReturnDetails.value = details);
  };

  const saveSeatReservations = (
    reservations: SeatFormData[] | undefined,
    isReturn: boolean
  ) => {
    if (reservations) {
      console.log(reservations);
      isReturn
        ? (seatReturnReservations.value = reservations)
        : (seatReservations.value = reservations);
    }
  };

  const getOrder = () => {
    const toTickets = merge(passengerDetails.value, seatReservations.value);

    const returnTickets = merge(
      map(
        passengerDetails.value,
        partialRight(pick, ["firstName", "lastName"])
      ),
      passengerReturnDetails.value,
      seatReturnReservations.value
    );

    let orderToFlight = {};

    if (toFlight.value?.flightId) {
      orderToFlight = {
        toFlight: {
          flightId: toFlight.value.flightId,
          isReturn: false,
          tickets: toTickets,
        },
      };
    }

    let orderReturnFlight = {};

    if (returnFlight.value?.flightId) {
      orderReturnFlight = {
        returnFlight: {
          flightId: returnFlight.value.flightId,
          isReturn: true,
          tickets: returnTickets,
        },
      };
    }

    const order = merge(orderToFlight, orderReturnFlight);

    return order as Order;
  };

  return {
    bookingOptions,
    toFlight,
    returnFlight,
    passengerDetails,
    passengerReturnDetails,
    seatReservations,
    order,
    saveBookingOptions,
    saveFlightChoice,
    savePassengerDetails,
    savePassengerReturnDetails,
    saveSeatReservations,
  };
});

export { useProvideBookingStore };
export { useBookingStore };
