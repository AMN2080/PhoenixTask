import axios from "axios";
import { authTokenUpdate, logOut } from "@/logic/store/slices/authSlice";
import store from "@/logic/store/store";

// const baseURL = "https://quera-task-server.vercel.app";
const baseURL = "http://185.8.174.74:8000/";

const getFromLocalStorage = (key: string) => {
  if (typeof window !== "undefined") {
    return JSON.parse(localStorage.getItem(key) as string);
  }
  return null;
};

let authToken = getFromLocalStorage("authToken") || null;

const AXIOS = axios.create({
  baseURL,
});

AXIOS.interceptors.request.use(
  async (config) => {
    if (typeof window !== "undefined" && authToken?.accessToken) {
      authToken = getFromLocalStorage("authToken") || null;
      if (authToken) {
        config.headers["x-auth-token"] = authToken.accessToken;
      }
    }

    return config;
  },
  (error) => Promise.reject(error),
);

AXIOS.interceptors.response.use(
  (response) => {
    return response.data;
  },
  async (error) => {
    const originalRequest = error.config;

    // if the error status code is 401 and it's a refresh token request
    if (
      error.response?.status === 401 &&
      originalRequest.url.endsWith("/refreshtoken")
    ) {
      // Redirect the user to the login page if refresh token is expired or invalid
      if (typeof window !== "undefined") {
        localStorage.removeItem("authToken");
        window.location.href = "/login";
      }
      return Promise.reject(error);
    }

    // if the error status code is 401 and we haven't retried the request yet
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        if (typeof window !== "undefined") {
          // Get a new access token using the refresh token
          const token = getFromLocalStorage("authToken");

          const response = await axios.post(
            `${baseURL}/api/auth/refreshtoken`,
            {
              refreshToken: token.refreshToken,
            },
          );

          // Update the access token in local storage with the new token
          const newAccessToken = response.data.data.accessToken;
          const currentRefreshToken = token.refreshToken;
          authToken = {
            accessToken: newAccessToken,
            refreshToken: currentRefreshToken,
          };

          localStorage.setItem("authToken", JSON.stringify(authToken));

          // Update the access token in store with the new token
          store.dispatch(authTokenUpdate(authToken));

          // Update the authorization header with the new access token and retry the request
          AXIOS.defaults.headers.common["x-auth-token"] = newAccessToken;
          return AXIOS(originalRequest);
        }
      } catch (refreshError) {
        // Log out if both tokens are expired or invalid
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

export default AXIOS;
