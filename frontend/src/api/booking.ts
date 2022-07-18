import type {
  FlightChoice,
  FlightData,
  FlightResponse,
} from "@/interfaces/booking/flights";
import type { Order } from "@/interfaces/booking/order";
import type { SeatData } from "@/interfaces/booking/seats";
import type { Airport, BookingOptions } from "@/interfaces/booking/start";
import { isNil, omitBy } from "lodash-es";
import { useAPI } from "./useAPI";

export const getAirports = () => {
  return useAPI("/airport").get().json<Airport[]>();
};

export const getFlights = (data: BookingOptions | undefined) => {
  let parsedOptions = {};

  if (data) {
    parsedOptions = {
      from: data.from?.name ?? "",
      to: data.to?.name ?? "",
      departureDate: data.departureDate.toLocaleDateString(),
      returnDate: data.returnDate?.toLocaleDateString(),
      passengerCount: data.passengerCount,
    };
  }

  const queryString = new URLSearchParams(
    omitBy(parsedOptions, isNil)
  ).toString();

  return useAPI(`/ticketordering/flights?${queryString}`, {
    afterFetch(ctx) {
      ctx.data.toFlights = ctx.data.toFlights.map(postProcessFlightResponse);
      ctx.data.returnFlights = ctx.data.returnFlights.map(
        postProcessFlightResponse
      );
      return ctx;
    },
  })
    .get()
    .json<FlightData>();
};

export const getSeats = (data?: FlightChoice) => {
  const queryString = new URLSearchParams(omitBy(data, isNil)).toString();

  return useAPI(`/ticketordering/seats?${queryString}`).get().json<SeatData>();
};

export const placeOrder = (data: Order) => {
  return useAPI("ticketordering/order").post(data).json();
};

const postProcessFlightResponse = (flight: FlightResponse) => {
  return {
    ...flight,
    arrivalTime: new Date(flight.arrivalTime),
    departureTime: new Date(flight.departureTime),
  };
};
