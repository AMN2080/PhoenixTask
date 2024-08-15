"use client";
import DashboardHeader from "@/components/modules/DashboardHeader";
import DashboardSidebar from "@/components/modules/DashboardSidebar";

interface AuthLayoutProps {
  children: React.ReactNode;
}

export default function AuthLayout({ children }: AuthLayoutProps) {
  return (
    <div>
      <DashboardHeader />
      <DashboardSidebar />
      {children}
    </div>
  );
}
