interface Flight {
  from: string;
  fromCity: string;
  to: string;
  toCity: string;
  departure: Date;
  isReturn: boolean;
}

export interface Order {
  id: string;
  passengerCount: number;
  price: number;
  toFlight: Flight;
  toFlightStatus: string;
  returnFlight?: Flight;
  returnFlightStatus: string;
}

export interface OrderData {
  orders: Order[];
}
