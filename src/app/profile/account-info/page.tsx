"use client";

// import Input from "../../components/ui/Input";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { accountInfo, accountInfoType } from "@/logic/schemas/accountInfo";
import { Button, Flex, Input } from "@/components/UI";
// import Button from "../../components/ui/Button";

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

  const errorMsgStyle = "text-FC0733 text-xs absolute py-1";
  const errorInputStyle = "border-FB0606";

  return (
    <div className="w-96 mr-14 dark:text-[#F7F9F9]">
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
            <p className={errorMsgStyle}>{errors.email?.message}</p>

            <Input
              label="نام کاربری"
              connectorId="username"
              className={errors.username?.message && errorInputStyle}
              {...register("username")}
            />
            <p className={errorMsgStyle}>{errors.username?.message}</p>

            <Input
              label="رمزعبور فعلی"
              type="password"
              connectorId="oldPassword"
              className={errors.oldPassword?.message && errorInputStyle}
              {...register("oldPassword")}
            />
            <p className={errorMsgStyle}>{errors.oldPassword?.message}</p>

            <Input
              label="رمزعبور جدید"
              type="password"
              connectorId="newPassword"
              className={errors.newPassword?.message && errorInputStyle}
              {...register("newPassword")}
            />
            <p className={errorMsgStyle}>{errors.newPassword?.message}</p>

            <Input
              label="تکرار رمزعبور جدید"
              type="password"
              connectorId="confirmNewPassword"
              className={errors.confirmNewPassword?.message && errorInputStyle}
              {...register("confirmNewPassword")}
            />
            <p className={errorMsgStyle}>
              {errors.confirmNewPassword?.message}
            </p>
          </Flex>
          <Button variant="primary">ثبت تغییرات</Button>
        </form>
      </div>
    </div>
  );
};

export default AccountInfoPage;
