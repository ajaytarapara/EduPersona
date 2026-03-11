import { createAxiosInstance } from "./axiosFactory";

export const ipdsApiInstance = createAxiosInstance(
  import.meta.env.VITE_IPDS_API_BASE_URL
);

export const examApiInstance = createAxiosInstance(
  import.meta.env.VITE_EXAM_API_BASE_URL
);
