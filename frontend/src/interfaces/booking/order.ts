import type { BookingOptions } from "./start";

type TicketType = "Tourist" | "Premium" | "SuperPremium";
export type TicketSelectOption = { label: TicketType };

export const ticketTypes: TicketSelectOption[] = [
  { label: "Tourist" },
  { label: "Premium" },
  { label: "SuperPremium" },
];

export interface Ticket {
  firstName: string;
  lastName: string;
  type: TicketType | undefined;
  isLuggage: boolean;
  colNum: number;
  rowNum: number;
}

interface Flight {
  flightId: string;
  isReturn: boolean;
  tickets: Ticket[];
}

export interface Order {
  toFlight: Flight;
  returnFlight?: Flight;
}

export interface BookingStoreData {
  order?: Order;
  bookingOptions?: BookingOptions;
}
