import { z } from "zod";

const validationErrors = {
  passwordSize: "رمز عبور باید حداقل ۸ کاراکتر داشته باشد.",
  weekPassword: "رمزعبور باید شامل حرف و عدد باشد.",
  confirmPassword: "رمز عبور مطابقت ندارد.",
};

export const passwordResetSchema = z
  .object({
    password: z
      .string()
      .min(8, validationErrors.passwordSize)
      .regex(/[a-zA-Z]/, validationErrors.weekPassword)
      .regex(/[0-9]/, validationErrors.weekPassword),
    confirmPassword: z.string().min(1, validationErrors.confirmPassword),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"],
    message: validationErrors.confirmPassword,
  });
