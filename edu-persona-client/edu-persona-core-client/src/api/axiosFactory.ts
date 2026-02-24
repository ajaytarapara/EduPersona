import axios from "axios";
import type { AxiosError, AxiosInstance } from "axios";
import { toast } from "react-toastify";
import { CORE_ENDPOINT, IDPS_ENDPOINT, type IApiResponse } from "../utils";
import { refreshAccessToken } from "./auth.api";

//Refresh token
type FailedRequest = {
  resolve: (value?: unknown) => void;
  reject: (reason?: AxiosError | Error) => void;
};

let isRefreshing = false;
let failedQueue: FailedRequest[] = [];

const processQueue = (
  error: AxiosError | Error | null,
  value?: unknown
): void => {
  failedQueue.forEach(({ resolve, reject }) => {
    if (error) {
      reject(error);
    } else {
      resolve(value);
    }
  });

  failedQueue = [];
};

export const refreshAxios = axios.create({
  baseURL: import.meta.env.VITE_CORE_API_BASE_URL,
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  },
});

export const createAxiosInstance = (baseURL: string): AxiosInstance => {
  const axiosInstance = axios.create({
    baseURL,
    withCredentials: true,
    headers: {
      "Content-Type": "application/json",
    },
  });

  // Request Interceptor
  axiosInstance.interceptors.request.use(
    (config) => {
      return config;
    },
    (error) => {
      toast.error("Failed to send request!");
      return Promise.reject(error);
    }
  );

  axiosInstance.interceptors.response.use(
    (response) => {
      const apiResponse = response.data as IApiResponse<unknown>;

      if (
        apiResponse.success &&
        apiResponse.message &&
        apiResponse.message != "Request successful."
      ) {
        toast.success(apiResponse.message);
      }

      return response;
    },
    async (error) => {
      const apiError = error.response?.data;

      const originalRequest = error.config;

      if (error.response?.status === 401 && !originalRequest?._retry) {
        if (isRefreshing) {
          return new Promise((resolve, reject) => {
            failedQueue.push({ resolve, reject });
          })
            .then(() => axiosInstance(originalRequest))
            .catch((err) => Promise.reject(err));
        }
        originalRequest._retry = true;
        isRefreshing = true;
        try {
          await refreshAccessToken();
          processQueue(null);
          return axiosInstance(originalRequest);
        } catch (refreshError) {
          const error =
            refreshError instanceof Error
              ? refreshError
              : new Error("Unknown refresh error");
          processQueue(error, null);
          toast.error("Session expired. Please login again.");
          return Promise.reject(error);
        } finally {
          isRefreshing = false;
        }
      }
      if (apiError) {
        toast.error(apiError.message);
        // apiError.errors?.forEach((err) => toast.error(err));
      } else {
        toast.error("Network error. Please try again.");
      }

      return Promise.reject(error);
    }
  );
  return axiosInstance;
};
