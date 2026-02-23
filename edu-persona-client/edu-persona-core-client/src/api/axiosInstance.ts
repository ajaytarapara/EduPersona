import axios from "axios";
import type { AxiosError } from "axios";
import { toast } from "react-toastify";
import type { IApiResponse } from "../utils";

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

axiosInstance.interceptors.response.use(
  (response) => {
    const apiResponse = response.data as IApiResponse<unknown>;

    if (apiResponse.success && apiResponse.message) {
      toast.success(apiResponse.message);
    }

    return response;
  },
  (error: AxiosError<IApiResponse<unknown>>) => {
    const apiError = error.response?.data;

    if (apiError) {
      toast.error(apiError.message);

      apiError.errors?.forEach((err) => toast.error(err));
    } else {
      toast.error("Network error. Please try again.");
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;