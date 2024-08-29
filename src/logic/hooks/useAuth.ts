import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import APIClient from "../services/api-client";
import TokenManager from "../services/token-service";
import { RootState } from "../stores/authStore";
import {
  setUser,
  setAccessToken,
  setRefreshToken,
  clearAuth,
} from "../stores/slices/authSlice";
import { AxiosError } from "axios";

interface LoginData {
  username: string;
  password: string;
}

interface RegisterData extends LoginData {
  email: string;
}

interface RegisterResponse {
  id: number;
  username: string;
  email: string;
}

export interface LoginResponse {
  access: string;
  email: string;
  first_name: string;
  last_name: string;
  phone_number: string;
  refresh: string;
  user_id: string;
  username: string;
}

interface RegisterErrorResponse {
  email?: string[];
  username?: string[];
}
export interface User extends Omit<LoginResponse, "access" | "refresh"> {}

const useAuth = () => {
  const [isLoading, setIsLoading] = useState(false);
  const dispatch = useDispatch();

  const login = async (data: LoginData) => {
    try {
      setIsLoading(true);
      const {
        status,
        data: { access, refresh, ...userData },
      } = await new APIClient<LoginData, LoginResponse>(
        "/accounts/login/",
      ).create(data);

      TokenManager.setAccessToken(access);
      TokenManager.setRefreshToken(refresh);

      dispatch(setUser(userData));
      dispatch(setAccessToken(access));
      dispatch(setRefreshToken(refresh));

      return status;
    } catch (error) {
      throw error;
    } finally {
      setIsLoading(false);
    }
  };

  const signUp = async (data: RegisterData) => {
    try {
      setIsLoading(true);
      const res = await new APIClient<RegisterData, RegisterResponse>(
        "/accounts/",
      ).create(data);
      if (res.status === 201) {
        return res.data;
      }
    } catch (error) {
      const errorMessages = (error as AxiosError).response
        ?.data as RegisterErrorResponse;

      throw {
        emailError: errorMessages?.email?.[0] ?? "",
        usernameError: errorMessages?.username?.[0] ?? "",
      };
    } finally {
      setIsLoading(false);
    }
  };

  const logout = () => {
    dispatch(clearAuth());
    TokenManager.clearTokens();
  };

  const user = useSelector((state: RootState) => state.auth.user);

  return {
    login,
    isLoading,
    signUp,
    logout,
    user,
  };
};

export default useAuth;
