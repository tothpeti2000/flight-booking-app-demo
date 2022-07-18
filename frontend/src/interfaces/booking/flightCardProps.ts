import type { Flight } from "./flights";

export interface FlightCardProps {
  flight: Flight;
  isReturn?: boolean;
}
