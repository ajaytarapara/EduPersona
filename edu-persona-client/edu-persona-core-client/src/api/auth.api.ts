import {
  CORE_ENDPOINT,
  IDPS_ENDPOINT,
  type IApiResponse,
  type ILoginPayload,
  type ILoginResponse,
  type IRegisterPayload,
  type IValidateTokenResponse,
} from "../utils";
import { refreshAxios } from "./axiosFactory";
import { coreApiInstance, ipdsApiInstance } from "./axiosInstance";

export const registerUser = async (
  payload: IRegisterPayload
): Promise<IApiResponse<null>> => {
  const response = await ipdsApiInstance.post<IApiResponse<null>>(
    IDPS_ENDPOINT.Register,
    payload
  );

  return response.data;
};

export const loginUser = async (
  payload: ILoginPayload
): Promise<IApiResponse<ILoginResponse>> => {
  const response = await ipdsApiInstance.post<IApiResponse<ILoginResponse>>(
    IDPS_ENDPOINT.Login,
    payload
  );

  return response.data;
};

export const validateSession = async (
  sessionId: number
): Promise<IApiResponse<IValidateTokenResponse>> => {
  const response = await coreApiInstance.get<
    IApiResponse<IValidateTokenResponse>
  >(`${CORE_ENDPOINT.VALIDATE_SESSION}${sessionId}`);

  return response.data;
};

export const refreshAccessToken = async (): Promise<IApiResponse<null>> => {
  const response = await refreshAxios.get<IApiResponse<null>>(
    `${CORE_ENDPOINT.REFRESH_ACCESS_TOKEN}`
  );
  return response.data;
};

export const googleLogin = async (code: string) => {
  const response = await ipdsApiInstance.post("/login/google-login", { code });
  return response.data;
};