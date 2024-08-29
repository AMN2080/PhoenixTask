import store from "../stores/authStore";
import { setAccessToken, setRefreshToken } from "../stores/slices/authSlice";

class TokenManager {
  static getAccessToken = () => store.getState().auth.accessToken;

  static setAccessToken = (token: string) => {
    store.dispatch(setAccessToken(token));
  };

  static getRefreshToken = () => store.getState().auth.refreshToken;

  static setRefreshToken = (token: string) => {
    store.dispatch(setRefreshToken(token));
  };

  static clearTokens = () => {
    store.dispatch(setAccessToken(""));
    store.dispatch(setRefreshToken(""));
  };
}

export default TokenManager;
