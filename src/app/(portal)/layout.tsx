"use client";

import Link from "@/components/modules/ui/Link";
import { usePathname } from "next/navigation";
interface AuthLayoutProps {
  children: React.ReactNode;
}

export default function AuthLayout({ children }: AuthLayoutProps) {
  const currentPath = usePathname();

  return (
    <main className="m-0 p-0 h-screen w-screen overflow-hidden flex flex-col font-IranYekan relative">
      <header className="flex justify-between items-center m-20 h-[45px]">
        <h1 className="logo font-IranYekan h-fit">Phoenix Task</h1>
        <div className="flex items-center">
          <h6 className="text-base font-medium ml-[7px]">
            {currentPath === "/login"
              ? "ثبت نام نکرده ای؟"
              : "قبلا ثبت نام کرده ای؟"}
          </h6>
          <Link
            className="text-center p-[10px] bg-brand-primary w-[95px] h-[40px] rounded-[6px]"
            weight="800"
            size="S"
            to={currentPath === "/login" ? "/register" : "/login"}
          >
            {currentPath === "/login" ? "ثبت‌نام" : "ورود"}
          </Link>
        </div>
      </header>
      <section className="flex justify-center items-center z-50 -m-4">
        {children}
      </section>
      <div className="authentication_bg absolute h-3/4 w-full bottom-0"></div>
    </main>
  );
}
