export interface Seat {
  colNum: number;
  rowNum: number;
}

export interface SeatFormData {
  colNum: number | undefined;
  rowNum: number | undefined;
}

export interface ColoredSeatFormData extends SeatFormData {
  color: string;
}

export interface SeatData {
  planeType: string;
  seatColCount: number;
  seatRowCount: number;
  bookedSeats: Seat[];
}
