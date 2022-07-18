import type { Flight } from "./flights";

export interface FlightSummaryProps {
  flight: Flight;
  isReturn?: boolean;
}
