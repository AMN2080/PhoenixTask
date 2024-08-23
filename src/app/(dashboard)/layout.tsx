"use client";
import DashboardHeader from "@/components/modules/DashboardHeader";
import DashboardSidebar from "@/components/modules/DashboardSidebar";
import { usePathname } from "next/navigation";

interface AuthLayoutProps {
  children: React.ReactNode;
}

export default function AuthLayout({ children }: AuthLayoutProps) {
  const location = usePathname();

  const commonStyle =
    "max-w-[85vw] scrollbar-thin scrollbar-thumb-gray-400 scrollbar-thumb-rounded-full scrollbar-track-white gap-5 h-[calc(100%-12rem)]";

  let wraperClasses = "";

  if (location === "/column") {
    wraperClasses = `overflow-x-auto my-4 overflow-y-hidden flex ${commonStyle}`;
  } else if (location === "/list") {
    wraperClasses = `overflow-y-auto mt-4 flex ${commonStyle}`;
  } else if (location === "/calendar") {
    wraperClasses = "overflow-hidden mt-2 h-[calc(100%-14rem)]";
  }

  return (
    <div className="flex flex-row w-full max-h-screen overflow-hidden select-none">
      <DashboardSidebar />
      <div className="w-4/5 pr-4 pl-10 min-h-screen">
        <DashboardHeader location={location.slice(1)} />
        <div className={`${wraperClasses} relative`}>{children}</div>
      </div>
    </div>
  );
}
