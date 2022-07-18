interface Discount {
  name: string;
  value: number;
}

export interface Flight {
  flightId: string;
  departureAirport: string;
  departureCity: string;
  arrivalAirport: string;
  arrivalCity: string;
  departureTime: Date;
  arrivalTime: Date;
  flightTimeMinutes: number;
  price: number;
  planeId: string;
  discounts: Discount[];
}

export interface FlightResponse {
  flightId: string;
  departureAirport: string;
  departureCity: string;
  arrivalAirport: string;
  arrivalCity: string;
  departureTime: string;
  arrivalTime: string;
  flightTimeMinutes: number;
  price: number;
  planeId: string;
  discounts: Discount[];
}

export interface FlightData {
  toFlights: Flight[];
  returnFlights: Flight[];
}

export interface FlightChoice {
  planeId: string | undefined;
  flightId: string | undefined;
}
