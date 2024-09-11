import axios from "axios";
import { registerType } from "@/logic/schemas/registerSchema";
import { loginType } from "@/logic/schemas/loginSchema";
import { forgotPasswordType } from "@/logic/schemas/forgotPasswordSchema";

// const API_URL = "https://quera-task-server.vercel.app/api/auth/";
const API_URL =
  "https://ip172-18-0-76-crglac2im2rg00fhbqh0-44388.direct.labs.play-with-docker.com/api/authentication/";

// Regiter user
const register = async (userData: registerType) => {
  // const response = await axios.post(API_URL + "register", userData);
  const response = await axios.post(API_URL + "create", userData);
  return response.data;
};

// Login user
const login = async (userData: loginType) => {
  // const response = await axios.post(API_URL + "login", userData);
  const response = await axios.post(API_URL + "login", userData);

  console.log(response);

  if (response.data) {
    localStorage.setItem(
      "authToken",
      JSON.stringify({
        accessToken: response.data.data.accessToken,
        refreshToken: response.data.data.refreshToken,
      }),
    );
    localStorage.setItem(
      "user",
      JSON.stringify(response.data.data.toBeSendUserData),
    );
  }
  return response.data;
};

// Forgot password
const forgot = async (userEmail: forgotPasswordType) => {
  const response = await axios.post(API_URL + "forget-password", userEmail);

  return response.data;
};

const authService = {
  register,
  login,
  forgot,
};

export default authService;
