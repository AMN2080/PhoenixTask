import type { Metadata } from "next";
import "./globals.css";

interface RootLayoutProps {
  children: React.ReactNode;
}

export const metadata: Metadata = {
  title: "Phoenix Task",
  description: "a web-app for everything",
};

export default function RootLayout({ children }: Readonly<RootLayoutProps>) {
  return (
    <html dir="rtl" lang="fa" data-theme="Cyan">
      <body>{children}</body>
    </html>
  );
}
