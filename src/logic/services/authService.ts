import axios from "axios";
import { registerType, loginType } from "@/logic/types/authType";
import { forgotPasswordType } from "@/logic/schemas/forgotPasswordSchema";

// const API_URL = "https://quera-task-server.vercel.app/api/auth/";
const API_URL = "http://185.8.174.74:8000/accounts/";
// const API_URL =
// "https://ip172-18-0-76-crglac2im2rg00fhbqh0-44388.direct.labs.play-with-docker.com/api/authentication/";

// Regiter user
const register = async (userData: registerType) => {
  // const response = await axios.post(API_URL + "register", userData);
  // const response = await axios.post(API_URL + "create", userData);
  const response = await axios.post(API_URL, userData);
  console.log(response);

  return response.data;
};

// Login user
const login = async (userData: loginType) => {
  // const response = await axios.post(API_URL + "login", userData);
  console.log(userData);
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
    // localStorage.setItem(
    //   "user",
    //   JSON.stringify(response.data.data.toBeSendUserData),
    // );
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
