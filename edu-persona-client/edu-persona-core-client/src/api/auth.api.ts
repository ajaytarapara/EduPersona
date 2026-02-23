import type { IApiResponse, IRegisterPayload } from "../utils";
import axiosInstance from "./axiosInstance";

export const registerUser = async (
  payload: IRegisterPayload
): Promise<IApiResponse<null>> => {
  const response = await axiosInstance.post<
    IApiResponse<null>
  >("/Register", payload);

  return response.data;
};