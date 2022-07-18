import type { Seat, SeatFormData } from "@/interfaces/booking/seats";
import {
  email,
  helpers,
  maxLength,
  minLength,
  required,
} from "@vuelidate/validators";
import { intersectionWith, isEqual, uniqWith } from "lodash-es";
import moment from "moment";
import useFeedback from "../useFeedback";

const hasCapitalLetter = (value: string) => value.match(/[A-Z]/g) !== null;
const hasNumber = (value: string) => value.match(/[0-9]/g) !== null;

export const requiredField = helpers.withMessage(
  "This field is required",
  required
);

export const emailField = helpers.withMessage(
  "Please, enter a valid email address",
  email
);

export const containsCapital = helpers.withMessage(
  "At least one capital letter is required",
  hasCapitalLetter
);

export const containsNumber = helpers.withMessage(
  "At least one number is required",
  hasNumber
);

export const minCharacters = (minCharacters: number) =>
  helpers.withMessage(
    `Please, enter at least ${minCharacters} characters`,
    minLength(minCharacters)
  );

export const maxCharacters = (maxCharacters: number) =>
  helpers.withMessage(
    `Please, enter at most ${maxCharacters} characters`,
    maxLength(maxCharacters)
  );

export const validateMultipleForms = async (
  forms: any,
  returnFlightChosen: boolean
) => {
  const validationResults = await Promise.all(
    forms.value?.map(async (form: any) => {
      const formValid = await form.validateForm();
      const returnFormValid = await form.validateReturnForm();

      return (
        formValid &&
        ((returnFormValid && returnFlightChosen) || !returnFlightChosen)
      );
    }) as []
  );

  return validationResults.every((result) => result === true);
};

export const validateReturnTickets = (passengerForms: any) => {
  const validationResults = passengerForms.value?.map((form: any) =>
    form.validateReturnTicketType()
  ) as [];

  return validationResults.every((result) => result === true);
};

const maxValueExceeded = (
  reservations: SeatFormData[],
  rows: number,
  columns: number
) => {
  return reservations.some((reservation) => {
    if (!reservation.rowNum || !reservation.colNum) {
      return true;
    }

    return reservation.rowNum > rows || reservation.colNum > columns;
  });
};

const reservationsUnique = (reservations: SeatFormData[]) => {
  const uniqueReservations = uniqWith(reservations, isEqual);

  return isEqual(uniqueReservations, reservations);
};

const invalidSeats = (reservations: SeatFormData[], bookedSeats: Seat[]) => {
  const invalidReservations = intersectionWith(
    reservations,
    bookedSeats,
    isEqual
  );

  const seatInfo = invalidReservations.map(
    (reservation) => `Row ${reservation.rowNum} - Column ${reservation.colNum}`
  );

  return seatInfo;
};

export const validateSeatReservations = (
  reservations: SeatFormData[] | undefined
) => {
  const { showError } = useFeedback();

  if (!reservations) {
    showError("Error while trying to book the seats");
    return false;
  }

  const validationResults = reservations.map(
    (reservation) =>
      reservation.rowNum !== undefined && reservation.colNum !== undefined
  );

  const isValid = validationResults.every((result) => result === true);
  !isValid && showError("You must choose a seat for all passengers");

  return isValid;
};

export const datesValid = (
  departureDate: Date,
  returnDate: Date | undefined
) => {
  const { showError } = useFeedback();

  const nextDay = moment().add(1, "day");

  if (nextDay.isAfter(departureDate, "day")) {
    showError("You can only book flights for tomorrow or later");
    return false;
  }

  if (returnDate && returnDate < departureDate) {
    showError("The return date must follow the departure date");
    return false;
  }

  return true;
};
