"use client";

import { useEffect } from "react";
import { FieldError, useForm } from "react-hook-form";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { toast } from "react-toastify";
import { useRouter, useSearchParams } from "next/navigation";
import { loginSchema, loginType } from "@/logic/schemas/loginSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import ErrorMessage from "@/components/templates/AuthError";
import { login as loginUser, reset } from "@/logic/services/auth/authSlice";
import {
  Button,
  Flex,
  Heading,
  Input,
  Link,
  Text,
} from "@/components/modules/UI";

const LoginForm = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<loginType>({ resolver: zodResolver(loginSchema) });

  const router = useRouter();
  const searchParams = useSearchParams;

  const dispatch = useAppDispatch();
  const { isSuccess, isLoading, isError, message } = useAppSelector(
    (state) => state.auth,
  );

  useEffect(() => {
    if (isError) {
      toast.dismiss();
      toast.error(
        `${
          message === "Invalid email/username or password"
            ? "نام کاربری/ایمیل یا رمز عبور نادرست است."
            : message
        } ❗`,
      );
      dispatch(reset());
    }
    if (isSuccess) {
      toast.dismiss();
      toast.success(` خوش آمدید 🎉`, { autoClose: 2000, rtl: true });
      dispatch(reset());
    }

    const redirect = searchParams.toString().includes("redirect")
      ? searchParams.toString().includes("redirect")
      : null;
    if (isSuccess && redirect) {
      const redirectUrl = Array.isArray(redirect) ? redirect[0] : redirect;
      router.push(redirectUrl);
    } else if (isSuccess) {
      router.push("/list");
    }
  }, [isSuccess, isError, isLoading, message, router, dispatch, searchParams]);

  const onSubmit = (data: loginType) => {
    dispatch(
      loginUser({
        username: data.username,
        password: data.password,
      }),
    );
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
            <Button
              disabled={isLoading}
              type="submit"
              size="full"
              className="relative h-11"
            >
              {isLoading ? (
                <span className="loading loading-dots loading-lg absolute left-[45%] -bottom-0 text-white" />
              ) : (
                "ورود"
              )}
            </Button>
            <Flex gap="XS" justifyContent="center" alignItems="center">
              <Text textSize="M" weight="500">
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
