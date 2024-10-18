import { AxiosError } from "axios";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import userService from "@/logic/services/userService";
import { personalInfoType } from "@/logic/schemas/personalInfo";
import { logOut } from "./authSlice";

type User = {
  id: string;
  username: string;
  email: string;
  workspaces: [];
  workspaceMember: [];
  taskAssignees: [];
  comments: [];
  settings: [];
  projectMember: [];
  firstname: string;
  lastname: string;
};

type initialStateType = {
  user: User | null;
  isLoading: boolean;
  isError: boolean;
  isSuccess: boolean;
  message: unknown;
  theme: string;
};

// Helper function to safely access localStorage
const getFromLocalStorage = (key: string) => {
  if (typeof window !== "undefined") {
    return JSON.parse(localStorage.getItem(key) as string);
  }
  return null;
};

const getThemeFromLocalStorage = () => {
  if (typeof window !== "undefined") {
    return localStorage.getItem("theme") || "light"; // Default theme
  }
  return "light"; // Default theme when localStorage is unavailable (SSR)
};

const initialState: initialStateType = {
  user: getFromLocalStorage("user") || null,
  isLoading: false,
  isError: false,
  isSuccess: false,
  theme: getThemeFromLocalStorage(),
  message: "",
};

// updateUserById
export const updateUserById = createAsyncThunk(
  "user/updateUserById",
  async (userData: personalInfoType, thunkAPI) => {
    try {
      return await userService.updateUserById(userData);
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        const message =
          error?.response?.data?.message || error.message || error.toString();
        return thunkAPI.rejectWithValue(message);
      }
    }
  },
);

export const fetchAddedMemberWorkspace = createAsyncThunk(
  "user/fetchAddedMemberWorkspace",
  async (memberId: string | undefined, thunkAPI) => {
    try {
      return await userService.fetchAddedMemberWorkspace(memberId);
    } catch (error: unknown) {
      if (error instanceof AxiosError) {
        const message =
          error?.response?.data?.message || error.message || error.toString();
        return thunkAPI.rejectWithValue(message);
      }
    }
  },
);

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    // Reset helper flags
    resetUser: (state) => {
      state.isLoading = false;
      state.isSuccess = false;
      state.isError = false;
      state.message = "";
    },
    setTheme: (state, action) => {
      state.theme = action.payload;
      if (typeof window !== "undefined") {
        localStorage.setItem("theme", action.payload); // Save theme to localStorage
      }
    },
  },
  extraReducers: (builder) => {
    builder
      // updateUserById
      .addCase(updateUserById.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(updateUserById.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        state.user = action.payload;
        state.message = action.payload.message;
      })
      .addCase(updateUserById.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      })
      .addCase(logOut, (state) => {
        state.user = null;
      });
  },
});

export const { resetUser, setTheme } = userSlice.actions;
export default userSlice.reducer;
