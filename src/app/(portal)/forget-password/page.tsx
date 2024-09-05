"use client";

import { z } from "zod";
import axios from "axios";
import { FieldError, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import { sendResetLinkSchema } from "@/logic/schemas/AuthSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import useAuth from "@/logic/hooks/useAuth";
import ErrorMessage from "@/components/templates/AuthError";
import {
  Button,
  Flex,
  Heading,
  Input,
  Text,
  Link,
} from "@/components/modules/UI";

type FormData = z.infer<typeof sendResetLinkSchema>;

const ForgetForm = () => {
  const router = useRouter();
  const { login, isLoading } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({ resolver: zodResolver(sendResetLinkSchema) });

  const onSubmit = async (data: FormData) => {
    try {
      const status = await login(data);
      if (status === 200)
        toast.success(
          <Text weight="500" textSize="M">
            🎉 خوش اومدی!
          </Text>,
        );

      router.push("/:workspaceId/:projectId");
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 401) {
        toast.error(
          <Text weight="500" textSize="M">
            نام کاربری یا رمز عبور اشتباهه
          </Text>,
        );
      }
    }
  };

  return (
    <div className="bg-white max-w-[640px] w-full shadow-[0_50px_100px_-20px_rgba(50,50,93,0.25)] p-6 rounded-b-[20px]">
      <Heading align="center" className="mb-8" as="h2" size="L">
        آدم فراموش‌کاره پیش میاد😊
      </Heading>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Flex gap={`${errors ? "XS" : "L"}`} direction="col">
          <Flex gap={`${errors ? "XS" : "M"}`} direction="col">
            <Input
              className={getErrorStyles(errors.email)}
              connectorId="email"
              label="ایمیل"
              type="email"
              {...register("email")}
            />
            <ErrorMessage error={errors.email} />
          </Flex>
          <Flex gap="M" direction="col">
            <Button disabled={isLoading} type="submit" size="full">
              {isLoading ? "در حال بررسی..." : "ارسال"}
            </Button>
          </Flex>
        </Flex>
      </form>
    </div>
  );
};

const getErrorStyles = (error: FieldError | undefined) => {
  return error ? "border-red-600 border-2" : "";
};

export default ForgetForm;
