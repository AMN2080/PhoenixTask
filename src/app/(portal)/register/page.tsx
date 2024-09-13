"use client";

import { useEffect, useState } from "react";
import { FieldError, useForm } from "react-hook-form";
import { useRouter } from "next/navigation";
import { zodResolver } from "@hookform/resolvers/zod";
import ErrorMessage from "@/components/modules/AuthError";
import RulesModal from "@/components/templates/RulesModal";
import { registerSchema, registerType } from "@/logic/schemas/registerSchema";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { toast } from "react-toastify";
import {
  register as registerUser,
  reset,
} from "@/logic/store/slices/authSlice";
import { Button, CheckBox, Flex, Heading, Input, Text } from "@/components/UI";

export default function RegisterForm() {
  const [showModal, setShowModal] = useState(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<registerType>({ resolver: zodResolver(registerSchema) });

  const { isSuccess, isLoading, isError, message } = useAppSelector(
    (state) => state.auth,
  );

  const dispatch = useAppDispatch();
  const router = useRouter();

  useEffect(() => {
    if (isError) {
      toast.dismiss();
      toast.error(`${message}`);
      dispatch(reset());
    }
    if (isSuccess) {
      toast.dismiss();
      toast.success(`ثبت نام با موفقیت انجام شد 🎉`, {
        autoClose: 1000,
        rtl: true,
      });
      router.push("/login");
      dispatch(reset());
    }
  }, [isSuccess, isError, message, isLoading, router, dispatch]);

  const onSubmit = async ({ username, email, password }: registerType) => {
    dispatch(
      registerUser({
        username,
        email,
        password,
      }),
    );
  };

  const handleOpenModal = () => {
    setShowModal(true);
  };
  const handleCloseModal = () => {
    setShowModal(false);
  };

  return (
    <div className="bg-white max-w-[640px] w-full shadow-[0_50px_100px_-20px_rgba(50,50,93,0.25)] p-6 rounded-b-[20px]">
      <Heading align="center" className="mb-8" as="h2" size="L">
        به جمع فینیکس تسکی‌ها بپیوند😎
      </Heading>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Flex gap={`${errors ? "XS" : "M"}`} direction="col">
          <Input
            className={getErrorStyles(errors.username)}
            connectorId="username"
            label="نام کاربری"
            {...register("username")}
          />
          <ErrorMessage error={errors.username} />

          <Input
            className={getErrorStyles(errors.email)}
            connectorId="email"
            label="ایمیل"
            type="email"
            {...register("email")}
          />
          <ErrorMessage error={errors.email} />

          <Input
            className={getErrorStyles(errors.password)}
            connectorId="password"
            label="رمز عبور"
            type="password"
            {...register("password")}
          />
          <ErrorMessage error={errors.password} />

          <Input
            className={getErrorStyles(errors.password)}
            connectorId="confirmPassword"
            label="تکرار رمز عبور"
            type="password"
            {...register("confirmPassword")}
          />
          <ErrorMessage error={errors.confirmPassword} />

          <CheckBox
            {...register("rules")}
            className={getErrorStyles(errors.rules)}
            connectorId="rules"
          >
            <span>
              <Button onClick={handleOpenModal} asChild>
                <Text className="underline underline-offset-8" textSize="M">
                  قوانین و مقررات
                </Text>
              </Button>{" "}
              را می‌پذیرم.
            </span>
          </CheckBox>
          <ErrorMessage error={errors.rules} />

          <RulesModal visible={showModal} onClose={handleCloseModal} />
          <Button
            disabled={isLoading}
            type="submit"
            size="full"
            className="relative h-11"
          >
            {isLoading ? (
              <span className="loading loading-dots loading-lg absolute left-[45%] -bottom-0 text-white" />
            ) : (
              "ثبت‌نام"
            )}
          </Button>
        </Flex>
      </form>
    </div>
  );
}

const getErrorStyles = (error: FieldError | undefined) => {
  return error ? "border-red-600 border-2" : "";
};
