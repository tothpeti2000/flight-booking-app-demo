import type { ErrorObject } from "@vuelidate/core";

export interface InputElementProps {
  iconName?: string;
  label: string;
  hasError?: boolean;
  errors?: ErrorObject[];
}
