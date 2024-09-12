type loginType = {
  username: string;
  password: string;
};

type registerType = {
  username: string;
  email: string;
  password: string;
};

type forgetPasswordType = {
  email: string;
};

export { type loginType, type registerType, type forgetPasswordType };
