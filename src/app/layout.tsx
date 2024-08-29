import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import store, { persistor } from "@/logic/stores/authStore";
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
    <html dir="rtl" lang="fa" data-theme="default">
      <body className="font-IranYekan">
        <Provider store={store}>
          <PersistGate loading={null} persistor={persistor}>
            {children}
          </PersistGate>
        </Provider>
      </body>
      ,
    </html>
  );
}
