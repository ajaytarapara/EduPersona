import {
  EXAM_ENDPOINT,
  IDPS_ENDPOINT,
  type IApiResponse,
  type IValidateTokenResponse,
} from "../utils";
import { refreshAxios } from "./axiosFactory";
import { examApiInstance, ipdsApiInstance } from "./axiosInstance";

export const getLoggedInUserInfo = async (): Promise<
  IApiResponse<IValidateTokenResponse>
> => {
  const response = await examApiInstance.get<
    IApiResponse<IValidateTokenResponse>
  >(`${EXAM_ENDPOINT.GET_LOGGED_IN_USER_INFO}`);

  return response.data;
};

export const refreshAccessToken = async (): Promise<IApiResponse<null>> => {
  const response = await refreshAxios.get<IApiResponse<null>>(
    `${EXAM_ENDPOINT.REFRESH_ACCESS_TOKEN}`
  );
  return response.data;
};

export const logout = async (userId: number): Promise<IApiResponse<null>> => {
  const response = await ipdsApiInstance.get<IApiResponse<null>>(
    `${IDPS_ENDPOINT.Logout}/${userId}`
  );
  return response.data;
};

export const checkProfileCompleted = async (): Promise<
  IApiResponse<boolean>
> => {
  const response = await examApiInstance.get<IApiResponse<boolean>>("/Auth");
  return response.data;
};
