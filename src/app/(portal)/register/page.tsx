"use client";

import { useState } from "react";
import { FieldError, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import ErrorMessage from "@/components/templates/AuthError";
import RulesModal from "@/components/templates/RulesModal";
import { registrationSchema } from "@/logic/schemas/AuthSchema";
import useAuth from "@/logic/hooks/useAuth";
import { toast } from "react-toastify";
import {
  Button,
  CheckBox,
  Flex,
  Heading,
  Input,
  Text,
  Link,
} from "@/components/modules/UI";

type FormData = z.infer<typeof registrationSchema>;

export default function RegisterPage() {
  const { signUp, isLoading } = useAuth();
  const [showModal, setShowModal] = useState(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({ resolver: zodResolver(registrationSchema) });

  const onSubmit = async (data: FormData) => {
    try {
      await signUp(data);
      toast.success(
        <Text weight="500" textSize="M">
          👌حساب کاربری با موفقیت ایجاد شد
        </Text>,
      );
    } catch (error) {
      const { usernameError, emailError } = error as {
        emailError: string;
        usernameError: string;
      };
      toast.error(
        <Text weight="500" textSize="M">
          {usernameError || emailError}
        </Text>,
      );
    }
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

          <CheckBox
            {...register("termsAndCondition")}
            className={getErrorStyles(errors.termsAndCondition)}
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
          <ErrorMessage error={errors.termsAndCondition} />

          <RulesModal visible={showModal} onClose={handleCloseModal} />
          <Button disabled={isLoading} type="submit" size="full">
            {isLoading ? "در حال ارسال اطلاعات..." : "ثبت‌نام"}
          </Button>
        </Flex>
      </form>
    </div>
  );
}

const getErrorStyles = (error: FieldError | undefined) => {
  return error ? "border-red-600 border-2" : "";
};
