import * as Yup from "yup";
import { VALIDATION_MESSAGES } from "../constants";
import type { Dayjs } from "dayjs";

export const profileSchema = Yup.object({
    birthdate: Yup.mixed<Dayjs>()
    .nullable(),

  address: Yup.string().max(50, VALIDATION_MESSAGES.ADDRESS_MAX).nullable(),

  phoneNo: Yup.string()
    .required(VALIDATION_MESSAGES.PHONE_REQUIRED)
    .matches(/^[0-9]{10}$/, VALIDATION_MESSAGES.PHONE_INVALID),

  professionId: Yup.number()
    .min(1, VALIDATION_MESSAGES.PROFESSION_REQUIRED)
    .required(VALIDATION_MESSAGES.PROFESSION_REQUIRED),

  currentDesignationId: Yup.number()
    .min(1, VALIDATION_MESSAGES.CURRENT_DESIGNATION_REQUIRED)
    .required(VALIDATION_MESSAGES.CURRENT_DESIGNATION_REQUIRED),

  targetDesignationId: Yup.number()
    .min(1, VALIDATION_MESSAGES.TARGET_DESIGNATION_REQUIRED)
    .notOneOf(
      [Yup.ref("currentDesignationId")],
      "Target designation cannot be same as current designation"
    )
    .required(VALIDATION_MESSAGES.TARGET_DESIGNATION_REQUIRED),

  skillIds: Yup.array()
    .of(Yup.number().required())
    .min(1, VALIDATION_MESSAGES.SKILL_REQUIRED)
    .required(),
});

export type IProfileFormValues = Yup.InferType<typeof profileSchema>;