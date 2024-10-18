import axios from "axios";
import { registerType, loginType } from "@/logic/types/authType";
import { forgotPasswordType } from "@/logic/schemas/forgotPasswordSchema";

const API_URL = "http://185.8.174.74:8000/accounts/";

// Regiter user
const register = async (userData: registerType) => {
  const response = await axios.post(API_URL, userData);
  return response.data;
};

// Login user
const login = async (userData: loginType) => {
  const response = await axios.post(API_URL + "login/", userData);

  if (response.data && typeof window !== "undefined") {
    localStorage.setItem(
      "authToken",
      JSON.stringify({
        accessToken: response.data.access,
        refreshToken: response.data.refresh,
      }),
    );
    localStorage.setItem("user", JSON.stringify(response.data));
  }
  return response.data;
};

// Forgot password
const forgotPassword = async (userEmail: forgotPasswordType) => {
  const response = await axios.post(API_URL + "forget-password", userEmail);

  return response.data;
};

const authService = {
  register,
  login,
  forgotPassword,
};
export default authService;
