import { z } from "zod";

const validationErrors = {
  username: "نام‌کاربری نامعتبر است.",
  passwordRequired: "رمز عبور را وارد کنید.",
};

const loginSchema = z.object({
  username: z.string().min(3, validationErrors.username),
  password: z.string().min(1, validationErrors.passwordRequired),
});

type loginType = z.infer<typeof loginSchema>;

export { loginSchema, type loginType };
