import type { Flight } from "./flights";
import type { SeatData } from "./seats";

export interface PlaneDetailsProps {
  flight: Flight | undefined;
  seatData: SeatData | null | undefined;
}
