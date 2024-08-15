"use client";

import { z } from "zod";
import axios from "axios";
import { FieldError, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import Button from "@/components/modules/ui/Button";
import Card from "@/components/modules/ui/Card";
import Flex from "@/components/modules/ui/Flex";
import Heading from "@/components/modules/ui/Heading";
import Input from "@/components/modules/ui/Input";
import Link from "@/components/modules/ui/Link";
import Text from "@/components/modules/ui/Text";
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
    <Card>
      <Heading align="center" className="mb-8" as="h2" size="S">
        خوش برگشتی😄
      </Heading>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Flex gap={`${errors ? "XS" : "L"}`} direction="col">
          <Flex gap={`${errors ? "XS" : "M"}`} direction="col">
            <Input
              {...register("username")}
              className={getErrorStyles(errors.username)}
              id="username"
              label="نام کاربری"
            />
            <ErrorMessage error={errors.username} />

            <Flex direction="col">
              <Input
                {...register("password")}
                className={getErrorStyles(errors.password)}
                type="password"
                id="password"
                label="رمز عبور"
              />
              <ErrorMessage error={errors.password} />
            </Flex>
          </Flex>
          <Flex gap="M" direction="col">
            <Button
              disabled={isLoading}
              type="submit"
              color="brand"
              size="full"
            >
              {isLoading ? "در حال بررسی..." : "ورود"}
            </Button>
            <Flex gap="XS" justifyContent="center" alignItems="center">
              <Text size="M" weight="500">
                رمزت رو فراموش کردی؟
              </Text>
              <Link to="/forget-password" color="brand" weight="800" size="M">
                فراموشی رمز
              </Link>
            </Flex>
          </Flex>
        </Flex>
      </form>
    </Card>
  );
};

const getErrorStyles = (error: FieldError | undefined) => {
  return error ? "border-red-600 border-2" : "";
};

export default LoginForm;
