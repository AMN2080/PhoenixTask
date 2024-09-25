"use client";
import { Flex } from "@/components/UI";
import ProfileSidebar from "@/components/modules/profile/Sidebar";

interface ProfileLayoutProps {
  children: React.ReactNode;
}
export default function ProfileLayout({ children }: ProfileLayoutProps) {
  return (
    <Flex className="w-screen h-screen select-none">
      <ProfileSidebar />
      <Flex direction="col" justifyContent="center" className="w-4/5 h-screen">
        {children}
      </Flex>
    </Flex>
  );
}
