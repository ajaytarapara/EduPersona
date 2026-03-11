import axios from "axios";
import type { AxiosError, AxiosInstance } from "axios";
import { toast } from "react-toastify";
import { redirectToLogin, type IApiResponse } from "../utils";
import { refreshAccessToken } from "./auth.api";
import type { AppStore } from "../store";
import { hideLoader, showLoader } from "../store/features";

let storeRef: AppStore | null = null;

export const injectStore = (store: AppStore) => {
  storeRef = store;
};

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
  baseURL: import.meta.env.VITE_EXAM_API_BASE_URL,
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
      storeRef?.dispatch(showLoader());
      return config;
    },
    (error) => {
      toast.error("Failed to send request!");
      storeRef?.dispatch(hideLoader());
      return Promise.reject(error);
    }
  );

  axiosInstance.interceptors.response.use(
    (response) => {
      const apiResponse = response.data as IApiResponse<unknown>;
      storeRef?.dispatch(hideLoader());

      if (
        apiResponse.success &&
        apiResponse.message &&
        apiResponse.message !== "Request successful."
      ) {
        toast.success(apiResponse.message);
      }

      return response;
    },
    async (error) => {
      storeRef?.dispatch(hideLoader());

      const apiError = error.response?.data;
      const status = error.response?.status;
      const originalRequest = error.config;

      const errorCode = apiError?.error;

      // 🔐 ACCESS TOKEN EXPIRED → REFRESH
      if (
        status === 401 &&
        (errorCode === "Access token invalid." || "Access token expired.") &&
        !originalRequest?._retry
      ) {
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
          processQueue(refreshError as Error);
          if (storeRef) redirectToLogin(storeRef);

          return Promise.reject(refreshError);
        } finally {
          isRefreshing = false;
        }
      }

      // ❌ OTHER ERRORS
      if (apiError?.message) {
        toast.error(apiError.message);
      } else {
        toast.error("Network error. Please try again.");
      }

      return Promise.reject(error);
    }
  );
  return axiosInstance;
};
