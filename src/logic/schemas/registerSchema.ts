import { z } from "zod";

const validationErrors = {
  username: "نام کاربری باید حداقل ۳ کاراکتر داشته باشد.",
  emailRequired: "وارد کردن ایمیل الزامی است.",
  invalidEmail: "ایمیل نامعتبر است.",
  passwordSize: "رمز عبور باید حداقل ۸ کاراکتر داشته باشد.",
  weekPassword: "رمزعبور باید شامل حرف و عدد باشد.",
  confirmPassword: "رمز عبور مطابقت ندارد.",
  confirmRules: "لطفا تایید کنید که قوانین را خوانده و با آن موافقید.",
};

const registerSchema = z
  .object({
    username: z.string().min(3, validationErrors.username),
    email: z
      .string()
      .min(1, validationErrors.emailRequired)
      .email(validationErrors.invalidEmail),
    password: z
      .string()
      .min(8, validationErrors.passwordSize)
      .regex(/[a-zA-Z]/, validationErrors.weekPassword)
      .regex(/[0-9]/, validationErrors.weekPassword),
    confirmPassword: z.string().min(1, validationErrors.confirmPassword),
    rules: z
      .boolean()
      .refine((value) => value === true, validationErrors.confirmRules),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"],
    message: validationErrors.confirmPassword,
  });

type registerType = z.infer<typeof registerSchema>;

export { registerSchema, type registerType };
