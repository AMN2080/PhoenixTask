"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { accountInfo, accountInfoType } from "@/logic/schemas/accountInfo";
import { Button, Flex, Input, ErrorMessage } from "@/components/UI";

const AccountInfoPage = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<accountInfoType>({
    resolver: zodResolver(accountInfo),
  });

  const onSubmit = (data: accountInfoType) => {
    console.log(data);
  };

  const errorInputStyle = "border-FB0606";

  return (
    <div className="w-96 mr-14">
      <h3 className="text-neutral-content text-2xl font-bold mb-9">
        اطلاعات حساب
      </h3>
      <div className="flex flex-col">
        <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-5">
          <Flex direction="col" gap="S">
            <Input
              label="ایمیل"
              connectorId="email"
              className={errors.email?.message && errorInputStyle}
              {...register("email")}
            />
            <ErrorMessage error={errors.email} />

            <Input
              label="نام کاربری"
              connectorId="username"
              className={errors.username?.message && errorInputStyle}
              {...register("username")}
            />
            <ErrorMessage error={errors.username} />

            <Input
              label="رمزعبور فعلی"
              type="password"
              connectorId="oldPassword"
              className={errors.oldPassword?.message && errorInputStyle}
              {...register("oldPassword")}
            />
            <ErrorMessage error={errors.oldPassword} />

            <Input
              label="رمزعبور جدید"
              type="password"
              connectorId="newPassword"
              className={errors.newPassword?.message && errorInputStyle}
              {...register("newPassword")}
            />
            <ErrorMessage error={errors.newPassword} />

            <Input
              label="تکرار رمزعبور جدید"
              type="password"
              connectorId="confirmNewPassword"
              className={errors.confirmNewPassword?.message && errorInputStyle}
              {...register("confirmNewPassword")}
            />
            <ErrorMessage error={errors.confirmNewPassword} />
          </Flex>
          <Button>ثبت تغییرات</Button>
        </form>
      </div>
    </div>
  );
};

export default AccountInfoPage;
