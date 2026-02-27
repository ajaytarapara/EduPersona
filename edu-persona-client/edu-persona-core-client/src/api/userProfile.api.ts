import { coreApiInstance } from "./axiosInstance";

export const isCompleteProfile = async () => {
  const response = await coreApiInstance.get(
    "/UserProfile/check-profile-completed"
  );
  return response.data;
};
