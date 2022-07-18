import type { Flight } from "./flights";

export interface FlightListProps {
  flights: Flight[] | undefined;
  isReturn?: boolean;
}
