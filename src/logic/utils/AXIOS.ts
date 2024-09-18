import axios, {
  AxiosRequestConfig,
  AxiosResponse,
  InternalAxiosRequestConfig,
} from "axios";
import { authTokenUpdate, logOut } from "@/logic/store/slices/authSlice";
import store from "@/logic/store/store";

// Base URL for API requests
const baseURL = "http://185.8.174.74:8000";

// Utility function to get token from local storage
const getFromLocalStorage = (key: string) => {
  if (typeof window !== "undefined") {
    return JSON.parse(localStorage.getItem(key) as string);
  }
  return null;
};

const axiosInstance = axios.create({
  baseURL,
});

// Request interceptor
axiosInstance.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const authToken = getFromLocalStorage("authToken");
    if (!authToken) return config;

    if (config.url === "/accounts/login/") return config;

    try {
      const { accessToken, refreshToken } = authToken;

      const decodedAccessToken = JSON.parse(atob(accessToken.split(".")[1]));
      const isExpired = new Date(decodedAccessToken.exp * 1000) < new Date();

      if (isExpired && refreshToken) {
        const {
          data: { access },
        } = await axios.post<{ access: string }>(
          `${baseURL}/accounts/refresh/`,
          { refresh: refreshToken },
        );

        // Update the access token in local storage and Redux store
        const newAuthToken = { accessToken: access, refreshToken };
        localStorage.setItem("authToken", JSON.stringify(newAuthToken));
        store.dispatch(authTokenUpdate(newAuthToken));

        config.headers.Authorization = `Bearer ${access}`;
      } else {
        config.headers.Authorization = `Bearer ${accessToken}`;
      }
    } catch (error) {
      store.dispatch(logOut());
      if (typeof window !== "undefined") {
        window.location.href = "/login";
      }
    }

    return config;
  },
  (error) => Promise.reject(error),
);

// Response interceptor
axiosInstance.interceptors.response.use(
  (response: AxiosResponse) => response,
  async (error) => {
    const originalRequest = error.config;

    // Check if the error status code is 401 and handle token refresh
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const authToken = getFromLocalStorage("authToken");
        if (!authToken) throw new Error("Refresh token not found");

        const { refreshToken } = authToken;

        const response = await axios.post(`${baseURL}/accounts/refresh/`, {
          refresh: refreshToken,
        });

        const newAccessToken = response.data.access;
        const newAuthToken = { accessToken: newAccessToken, refreshToken };
        localStorage.setItem("authToken", JSON.stringify(newAuthToken));
        store.dispatch(authTokenUpdate(newAuthToken));

        axiosInstance.defaults.headers.common["Authorization"] =
          `Bearer ${newAccessToken}`;
        return axiosInstance(originalRequest);
      } catch (refreshError) {
        store.dispatch(logOut());
        if (typeof window !== "undefined") {
          window.location.href = "/login";
        }
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  },
);

export default axiosInstance;
