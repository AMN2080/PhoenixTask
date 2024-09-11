"use client";

import { useEffect } from "react";
import { FieldError, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import {
  forgotPasswordSchema,
  forgotPasswordType,
} from "@/logic/schemas/forgotPasswordSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import ErrorMessage from "@/components/templates/AuthError";
import {
  Button,
  Flex,
  Heading,
  Input,
} from "@/components/modules/UI";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { forgotPassword, reset } from "@/logic/services/auth/authSlice";

const ForgetForm = () => {
  const router = useRouter();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<forgotPasswordType>({
    resolver: zodResolver(forgotPasswordSchema),
  });

  const dispatch = useAppDispatch();
  const { isSuccess, isLoading, isError, message } = useAppSelector(
    (state) => state.auth,
  );

  useEffect(() => {
    if (isError) {
      toast.dismiss();
      toast.error(message as string);
      dispatch(reset());
    }
    if (isSuccess) {
      toast.dismiss();
      toast.success(`ایمیل خود را بررسی کنید 🎉`, {
        rtl: true,
      });
      router.push("/forget-password");
      dispatch(reset());
    }
  }, [isSuccess, isError, message, isLoading, router, dispatch]);

  const onSubmit = async ({ email }: forgotPasswordType) => {
    dispatch(forgotPassword({ email }));
    // if (status === 200)
    //   toast.success(
    //     <Text weight="500" textSize="M">
    //       🎉 خوش اومدی!
    //     </Text>,
    //   );
    //   if (axios.isAxiosError(error) && error.response?.status === 401) {
    //     toast.error(
    //       <Text weight="500" textSize="M">
    //         نام کاربری یا رمز عبور اشتباهه
    //       </Text>,
    //     );
    //   }
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
