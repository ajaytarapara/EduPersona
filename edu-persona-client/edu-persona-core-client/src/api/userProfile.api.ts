import type { ICompleteProfilePayload } from "../utils/types/apiTypes/profile.types";
import { coreApiInstance } from "./axiosInstance";

export const isCompleteProfile = async () => {
  const response = await coreApiInstance.get(
    "/UserProfile/check-profile-completed"
  );
  return response.data;
};

export const completeProfile = async (
  payload: ICompleteProfilePayload
) => {
  const response = await coreApiInstance.post(
    "/UserProfile/complete-profile",
    payload
  );

  return response.data;
};