"use client";
import DashboardHeader from "@/components/modules/DashboardHeader";
import DashboardSidebar from "@/components/modules/DashboardSidebar";
import { usePathname } from "next/navigation";

interface AuthLayoutProps {
  children: React.ReactNode;
}

export default function AuthLayout({ children }: AuthLayoutProps) {
  const location = usePathname();

  return (
    <div>
      <DashboardHeader location={location.slice(1)} />
      <DashboardSidebar />
      {children}
    </div>
  );
}
