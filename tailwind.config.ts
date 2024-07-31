import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      fontSize: {
        lh: ["32px", "46px"], // => Heading L
        mh: ["28px", "34px"], // => Heading M
        /*
         * 2xl => Heading S || Body XL
         * xl => Heading XS || Body L
         * base => Body M
         * sm => Body S
         * xs => Body xs
         */
      },
      fontFamily: {
        IranYekan: ["IRANYekan", "sans-serif"],
      },
    },
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      {
        mytheme: {
          primary: "#ff9800",
          "primary-content": "#fff",
          secondary: "#fbbf24",
          // background: "#333333",
          background: "#f5f5f5",
          accent: "#ff5722",
          neutral: "#757575",
          "base-100": "#f5f5f5",
          info: "#2196f3",
          success: "#4caf50",
          warning: "#ffc107",
          error: "#f44336",
        },
      },
    ],
  },
};

export default config;

/*
colors: {
  "gray-secondary": "#F1F3F5",
  "gray-primary": "#868E96",
  "gray-darker": "#343A40",
  "red-secondary": "#FFE3E3",
  "red-primary": "#FA5252",
  "pink-secondary": "#FFDEEB",
  "pink-primary": "#E64980",
  "grape-secondary": "#F3D9FA",
  "grape-primary": "#BE4BDB",
  "violet-secondary": "#E5DBFF",
  "violet-primary": "#E5DBFF",
  "indigo-secondary": "#DBE4FF",
  "indigo-primary": "#4C6EF5",
  "blue-secondary": "#D0EBFF",
  "blue-primary": "#228BE6",
  "cyan-secondary": "#C5F6FA",
  "cyan-primary": "#15AABF",
  "teal-secondary": "#C3FAE8",
  "teal-primary": "#12B886",
  "brand-secondary": "#C2F7FA",
  "brand-primary": "#208D8E",
  "green-secondary": "#D3F9D8",
  "green-primary": "#40C057",
  "lime-secondary": "#E9FAC8",
  "lime-primary": "#82C91E",
  "yellow-secondary": "#FFF3BF",
  "yellow-primary": "#FAB005",
  "orange-secondary": "#FFE8CC",
  "orange-primary": "#FD7E14",
},
*/
