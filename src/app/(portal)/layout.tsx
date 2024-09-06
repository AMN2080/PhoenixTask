"use client";

import Link from "@/components/modules/UI/Link";
import { usePathname } from "next/navigation";
interface AuthLayoutProps {
  children: React.ReactNode;
}

export default function AuthLayout({ children }: AuthLayoutProps) {
  const currentPath = usePathname();

  return (
    <main className="m-0 p-0 h-screen w-full overflow-hidden flex flex-col font-IranYekan relative">
      <header className="flex justify-between items-center mx-20 mt-10 h-11">
        <h1 className="logo font-IranYekan h-fit">Phoenix Task</h1>
        <div className="flex gap-2 items-center">
          <h6 className="font-medium">
            {currentPath === "/login" || currentPath === "/forget-password"
              ? "ثبت‌نام نکردی؟"
              : "قبلا ثبت‌نام کردی؟"}
          </h6>
          <Link
            weight="800"
            textSize="S"
            to={
              currentPath === "/login" || currentPath === "/forget-password"
                ? "/register"
                : "/login"
            }
          >
            {currentPath === "/login" || currentPath === "/forget-password"
              ? "ثبت‌نام"
              : "ورود"}
          </Link>
        </div>
      </header>
      <section className="flex justify-center items-center mt-32 z-50 -m-4">
        {children}
      </section>
      <div className="absolute top-[450px] w-full h-screen z-[-1] skew-y-[-6deg] bg-gradient-to-l from-primary to-secondary"></div>
    </main>
  );
}
