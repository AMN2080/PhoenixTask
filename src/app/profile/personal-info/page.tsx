"use client";

import { Button, Input, Flex, Heading } from "@/components/UI";
import ErrorMessage from "@/components/modules/AuthError";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { personalInfo, personalInfoType } from "@/logic/schemas/personalInfo";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { toast } from "react-toastify";
import { updateUserById, resetUser } from "@/logic/store/store";

function PersonalInfoPage() {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<personalInfoType>({
    resolver: zodResolver(personalInfo),
  });

  const dispatch = useAppDispatch();
  const { isSuccess, isLoading, isError, message } = useAppSelector(
    (state) => state.user,
  );

  const { user } = useAppSelector((state) => state.auth);

  useEffect(() => {
    if (isError) {
      toast.dismiss();
      toast.error(`${message}`);
      dispatch(resetUser());
    }
    if (isSuccess) {
      toast.dismiss();
      toast.success(`اطلاعات شما با موفقیت تغییر کرد`, {
        autoClose: 1000,
        rtl: true,
      });
      dispatch(resetUser());
      reset();
    }
  }, [isSuccess, isError, message, isLoading, dispatch, reset]);

  const onSubmit = (data: personalInfoType) => {
    dispatch(
      updateUserById({
        firstName: data.firstName,
        lastName: data.lastName,
        phone: data.phone,
      }),
    );
  };

  const errorInputStyle = "border-FB0606";

  return (
    <div className="w-96 mr-14">
      <Heading
        as="h3"
        size="S"
        weight="600"
        className="text-neutral-content mb-9"
      >
        اطلاعات فردی
      </Heading>
      <Flex direction="col">
        <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col">
          <Flex gap="S" className="mb-6">
            <div>
              <Flex
                justifyContent="center"
                alignItems="center"
                className="overflow-hidden bg-base-300 text-base-content rounded-full text-2xl w-20 h-20"
              >
                {user?.username.slice(0, 2).toUpperCase()}
              </Flex>
            </div>
            <Flex direction="col" justifyContent="center" gap="S">
              <label
                className="border border-[#208D8E] rounded-lg p-2 cursor-pointer grid place-content-center text-xl text-[#208D8E]"
                htmlFor="img"
              >
                ویرایش تصویر پروفایل
              </label>
              <input hidden type="file" id="img" />
              <span className="text-xs text-[#8A8989]">
                این تصویر برای عموم قابل نمایش است.
              </span>
            </Flex>
          </Flex>

          <div className="mb-9">
            <Input
              label="نام"
              connectorId="firstName"
              className={errors.firstName?.message && errorInputStyle}
              {...register("firstName")}
            />
            <ErrorMessage error={errors.firstName} />

            <Input
              label="نام خانوادگی"
              connectorId="lastName"
              className={errors.lastName?.message && errorInputStyle}
              {...register("lastName")}
            />
            <ErrorMessage error={errors.lastName} />

            <Input
              label="شماره تلفن"
              connectorId="phone"
              className={errors.phone?.message && errorInputStyle}
              {...register("phone")}
            />
            <ErrorMessage error={errors.phone} />
          </div>

          <div className="relative">
            <Button disabled={isLoading} type="submit" size="full">
              {isLoading ? (
                <span className="loading loading-dots loading-lg absolute left-[45%] -bottom-0 text-white" />
              ) : (
                "ثبت تغییرات"
              )}
            </Button>
          </div>
        </form>
      </Flex>
    </div>
  );
}

export default PersonalInfoPage;
