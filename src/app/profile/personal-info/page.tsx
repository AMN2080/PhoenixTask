"use client";

import { Button, Input } from "@/components/UI";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { personalInfo, personalInfoType } from "@/logic/schemas/personalInfo";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { toast } from "react-toastify";
import { updateUserById, resetUser } from "@/logic/store/store";

const PersonalInfo = () => {
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

  const errorMsgStyle = "text-FC0733 text-xs absolute py-1";
  const errorInputStyle = "border-FB0606";

  return (
    <div className="w-96  mr-14 dark:text-[#F7F9F9]">
      <h3 className="text-1E1E1E text-2xl font-bold mb-9 dark:text-inherit">
        اطلاعات فردی
      </h3>
      <div className="flex flex-col">
        <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col">
          {/* User Prof img */}
          <div className="flex mb-6 gap-4">
            <span className="w-24 h-24 grid place-content-center overflow-hidden  text-4xl rounded-full bg-EAF562">
              test
            </span>
            <div className="flex flex-col justify-center gap-3 ">
              <label
                className=" border border-208D8E rounded-lg p-2 cursor-pointer grid place-content-center text-xl text-208D8E dark:border-[#F1B127] dark:text-[#F1B127]"
                htmlFor="img"
              >
                ویرایش تصویر پروفایل
              </label>
              <input hidden type="file" id="img" />
              <span className="text-xs text-8A8989">
                این تصویر برای عموم قابل نمایش است.
              </span>
            </div>
          </div>

          {/* User Credentials */}
          <div className="mb-9">
            <Input
              label="نام"
              connectorId="firstName"
              className={errors.firstName?.message && errorInputStyle}
              {...register("firstName")}
            />
            <p className={errorMsgStyle}>{errors.firstName?.message}</p>
            <Input
              label="نام خانوادگی"
              connectorId="lastName"
              className={errors.lastName?.message && errorInputStyle}
              {...register("lastName")}
            />
            <p className={errorMsgStyle}>{errors.lastName?.message}</p>

            <Input
              label="شماره تلفن"
              connectorId="phone"
              className={errors.phone?.message && errorInputStyle}
              {...register("phone")}
            />
            <p className={errorMsgStyle}>{errors.phone?.message}</p>
          </div>

          <div className=" relative">
            <Button
              variant="primary"
              disabled={isLoading}
              type="submit"
              value={`${isLoading ? "" : "ثبت تغییرات"}`}
            ></Button>
            {isLoading && (
              <span className="loading loading-dots loading-lg absolute left-[45%] -bottom-0 text-white"></span>
            )}
          </div>
        </form>
      </div>
    </div>
  );
};

export default PersonalInfo;
