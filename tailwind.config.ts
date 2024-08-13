import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        IranYekan: ["IRANYekan", "sans-serif"],
      },
    },
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      {
        default: {
          primary: "#208D8E",
          "primary-content": "#C2F7FA",
          secondary: "#12B886",
          "secondary-content": "#C3FAE8",
          neutral: "#D3D3D3",
          "neutral-content": "#1E1E1E",
          "base-100": "#fff",
          "base-200": "#f5f5f5",
          "base-300": "#dedede",
          "base-content": "#1E1E1E",
          error: "#FA5252",
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
