import { z } from "zod";

const validationErrors = {
  invalidPhone: "شماره تلفن نامعتبر است.",
};

const personalInfo = z.object({
  firstName: z.optional(z.string()),
  lastName: z.optional(z.string()),
  phone: z.optional(
    z.string().regex(/^0?[1-9][0-9]{9}$/, validationErrors.invalidPhone),
  ),
});

type personalInfoType = z.infer<typeof personalInfo>;

export { personalInfo, type personalInfoType };
