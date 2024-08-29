import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { User } from "../../hooks/useAuth";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

type AuthState = {
  user: User | null;
  accessToken: string;
  refreshToken: string;
};

const initialState: AuthState = {
  user: null,
  accessToken: "",
  refreshToken: "",
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User | null>) => {
      state.user = action.payload;
    },
    setAccessToken: (state, action: PayloadAction<string>) => {
      state.accessToken = action.payload;
    },
    setRefreshToken: (state, action: PayloadAction<string>) => {
      state.refreshToken = action.payload;
    },
    clearAuth: (state) => {
      state.user = null;
      state.accessToken = "";
      state.refreshToken = "";
    },
  },
});

export const { setUser, setAccessToken, setRefreshToken, clearAuth } =
  authSlice.actions;

const persistConfig = {
  key: "authStore",
  storage,
};

export default persistReducer(persistConfig, authSlice.reducer);
