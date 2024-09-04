"use client";

import { z } from "zod";
import axios from "axios";
import { FieldError, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import Button from "@/components/modules/UI/Button";
import Flex from "@/components/modules/UI/Flex";
import Heading from "@/components/modules/UI/Heading";
import Input from "@/components/modules/UI/Input";
import Link from "@/components/modules/UI/Link";
import Text from "@/components/modules/UI/Text";
import { loginSchema } from "@/logic/schemas/AuthSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import useAuth from "@/logic/hooks/useAuth";
import ErrorMessage from "@/components/templates/AuthError";

type FormData = z.infer<typeof loginSchema>;

const LoginForm = () => {
  const router = useRouter();
  const { login, isLoading } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({ resolver: zodResolver(loginSchema) });

  const onSubmit = async (data: FormData) => {
    try {
      const status = await login(data);
      if (status === 200)
        toast.success(
          <Text weight="500" size="M">
            🎉 خوش اومدی!
          </Text>,
        );

      router.push("/:workspaceId/:projectId");
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 401) {
        toast.error(
          <Text weight="500" size="M">
            نام کاربری یا رمز عبور اشتباهه
          </Text>,
        );
      }
    }
  };

  return (
    <div className="bg-white max-w-[640px] w-full shadow-[0_50px_100px_-20px_rgba(50,50,93,0.25)] p-6 rounded-b-[20px]">
      <Heading align="center" className="mb-8" as="h2" size="S">
        خوش برگشتی😄
      </Heading>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Flex gap={`${errors ? "XS" : "L"}`} direction="col">
          <Flex gap={`${errors ? "XS" : "M"}`} direction="col">
            <Input
              className={getErrorStyles(errors.username)}
              connectorId="username"
              label="نام کاربری"
              {...register("username")}
            />
            <ErrorMessage error={errors.username} />

            <Flex direction="col">
              <Input
                {...register("password")}
                className={getErrorStyles(errors.password)}
                connectorId="password"
                type="password"
                label="رمز عبور"
              />
              <ErrorMessage error={errors.password} />
            </Flex>
          </Flex>
          <Flex gap="M" direction="col">
            <Button disabled={isLoading} type="submit" size="full">
              {isLoading ? "در حال بررسی..." : "ورود"}
            </Button>
            <Flex gap="XS" justifyContent="center" alignItems="center">
              <Text size="M" weight="500">
                رمزت رو فراموش کردی؟
              </Text>
              <Link to="/forget-password" weight="800" textSize="M">
                فراموشی رمز
              </Link>
            </Flex>
          </Flex>
        </Flex>
      </form>
    </div>
  );
};

const getErrorStyles = (error: FieldError | undefined) => {
  return error ? "border-red-600 border-2" : "";
};

export default LoginForm;
