import { z } from "zod";

const validationErrors = {
  username: "نام کاربری باید حداقل ۳ کاراکتر داشته باشد.",
  passwordRequired: "رمز عبور را وارد کنید.",
  invalidEmail: "ایمیل نامعتبر است.",
  passwordSize: "رمز عبور باید حداقل ۸ کاراکتر داشته باشد.",
  weekPassword: "رمزعبور باید شامل حرف و عدد باشد.",
  confirmPassword: "تکرار رمزعبور با رمزعبور جدید مطابقت ندارد.",
  confirmRules: "لطفا تایید کنید که قوانین را خوانده و با آن موافقید.",
};

const accountInfo = z
  .object({
    email: z.string().email(validationErrors.invalidEmail),
    username: z.string().min(3, validationErrors.username),
    oldPassword: z.string().min(1, validationErrors.passwordRequired),
    newPassword: z
      .string()
      .min(8, validationErrors.passwordSize)
      .regex(/[a-zA-Z]/, validationErrors.weekPassword)
      .regex(/[0-9]/, validationErrors.weekPassword),
    confirmNewPassword: z.string().min(1, validationErrors.confirmPassword),
  })
  .refine((data) => data.newPassword === data.confirmNewPassword, {
    path: ["confirmPassword"],
    message: validationErrors.confirmPassword,
  });

type accountInfoType = z.infer<typeof accountInfo>;

export { accountInfo, type accountInfoType };
