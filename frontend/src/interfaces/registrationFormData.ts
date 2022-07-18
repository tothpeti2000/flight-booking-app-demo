type Gender = "Man" | "Woman";
type GenderSelectOption = { label: Gender };

export const genders: GenderSelectOption[] = [
  { label: "Man" },
  { label: "Woman" },
];

export interface RegistrationFormData {
  firstName: string;
  lastName: string;
  email: string;
  gender: Gender | undefined;
  birthDate: Date | undefined;
  phone: string;
  nationality: string;
  password: string;
  privacyPolicyAccepted: boolean | undefined;
  termsAndUseAccepted: boolean | undefined;
  newsletterSubscription: boolean;
}
