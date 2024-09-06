import { z } from "zod";

const validationErrors = {
  emailRequired: "وارد کردن ایمیل الزامی است.",
  invalidEmail: "ایمیل نامعتبر است.",
};

const forgotPasswordSchema = z.object({
  email: z
    .string()
    .min(1, validationErrors.emailRequired)
    .email(validationErrors.invalidEmail),
});

type forgotPasswordType = z.infer<typeof forgotPasswordSchema>;

export { forgotPasswordSchema, type forgotPasswordType };
