import { createAxiosInstance } from "./axiosFactory";

export const ipdsApiInstance = createAxiosInstance(
  import.meta.env.VITE_IPDS_API_BASE_URL
);

export const coreApiInstance = createAxiosInstance(
  import.meta.env.VITE_CORE_API_BASE_URL
);
