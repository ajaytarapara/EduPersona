import * as Yup from "yup";
import { PASSWORD_REGEX, VALIDATION_MESSAGES } from "..";

export const registerSchema = Yup.object({
  firstName: Yup.string()
    .required(VALIDATION_MESSAGES.FIRST_NAME_REQUIRED)
    .max(50, VALIDATION_MESSAGES.NAME_MAX),

  lastName: Yup.string()
    .required(VALIDATION_MESSAGES.LAST_NAME_REQUIRED)
    .max(50, VALIDATION_MESSAGES.NAME_MAX),

  email: Yup.string()
    .required(VALIDATION_MESSAGES.EMAIL_REQUIRED)
    .email(VALIDATION_MESSAGES.INVALID_EMAIL)
    .max(100, VALIDATION_MESSAGES.EMAIL_MAX),

  password: Yup.string()
    .required(VALIDATION_MESSAGES.PASSWORD_REQUIRED)
    .min(6, VALIDATION_MESSAGES.PASSWORD_MIN)
    .matches(PASSWORD_REGEX, VALIDATION_MESSAGES.PASSWORD_PATTERN),

  confirmPassword: Yup.string()
    .required(VALIDATION_MESSAGES.CONFIRM_PASSWORD_REQUIRED)
    .oneOf([Yup.ref("password")], VALIDATION_MESSAGES.PASSWORD_MISMATCH),
});