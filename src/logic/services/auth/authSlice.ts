import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import authService from "./authService";
import { registerType } from "@/logic/schemas/registerSchema";
import { AxiosError } from "axios";
import { loginType } from "@/logic/schemas/loginSchema";

type User = {
  _id: string;
  username: string;
  email: string;
  settings: [];
};

type initialStateType = {
  authToken: { accessToken: string; refreshToken: string } | null;
  user: User | null;
  isLoading: boolean;
  isError: boolean;
  isSuccess: boolean;
  message: unknown;
};

const getFromLocalStorage = (key: string) => {
  if (typeof window !== "undefined") {
    return JSON.parse(localStorage.getItem(key) as string);
  }
  return null;
};

const initialState: initialStateType = {
  user: getFromLocalStorage("user") || null,
  authToken: getFromLocalStorage("authToken") || null,
  isLoading: false,
  isError: false,
  isSuccess: false,
  message: "",
};

// Register user
export const register = createAsyncThunk(
  "Auth/register",
  async (userData: registerType, thunkAPI) => {
    try {
      return await authService.register(userData);
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        const message =
          error?.response?.data?.message || error.message || error.toString();
        return thunkAPI.rejectWithValue(message);
      }
    }
  },
);

// Login user
export const login = createAsyncThunk(
  "Auth/login",
  async (userData: loginType, thunkAPI) => {
    try {
      return await authService.login(userData);
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        const message =
          error?.response?.data?.message || error.message || error.toString();

        return thunkAPI.rejectWithValue(message);
      }
    }
  },
);

// Forgot Password
export const forgot = createAsyncThunk(
  "Auth/forget-password",
  async (userEmail: registerType, thunkAPI) => {
    try {
      return await authService.forgot(userEmail);
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        const message =
          error?.response?.data?.message || error.message || error.toString();

        return thunkAPI.rejectWithValue(message);
      }
    }
  },
);

const authSlice = createSlice({
  name: "Auth",
  initialState,
  reducers: {
    // Reset helper flags
    reset: (state) => {
      state.isLoading = false;
      state.isSuccess = false;
      state.isError = false;
      state.message = "";
    },

    // Logout
    logOut: (state) => {
      if (typeof window !== "undefined") {
        localStorage.removeItem("authToken");
        localStorage.removeItem("user");
      }
      state.user = null;
      state.authToken = null;
    },
    // Update access token
    authTokenUpdate: (state, action) => {
      state.authToken = action.payload;
    },
    // Update user
    updateUser: (state, action) => {
      state.user = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      // Register
      .addCase(register.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(register.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        state.authToken = null;
        state.message = action.payload.message;
      })
      .addCase(register.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      })

      // Login
      .addCase(login.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        const { accessToken, refreshToken } = action.payload.data;
        state.authToken = { accessToken, refreshToken };
        state.user = action.payload.data.toBeSendUserData;
        state.message = action.payload.message;
      })
      .addCase(login.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      })
      // Forgot
      .addCase(forgot.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(forgot.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        state.message = action.payload.message;
      })
      .addCase(forgot.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      });
  },
});

export const { reset, logOut, authTokenUpdate, updateUser } = authSlice.actions;
export default authSlice.reducer;
