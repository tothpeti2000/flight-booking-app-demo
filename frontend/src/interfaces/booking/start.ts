export interface BookingOptions {
  from: Airport | undefined;
  to: Airport | undefined;
  departureDate: Date;
  returnDate?: Date;
  passengerCount: number;
}

export interface Airport {
  id: string;
  name: string;
  cityName: string;
  latitude: number;
  longitude: number;
  blobUrl: string;
}
