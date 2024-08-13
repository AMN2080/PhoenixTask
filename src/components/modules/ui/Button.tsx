import React from "react";
import {
  fontWeight,
  BodySize,
} from "@/logic/SharedComponentStyles/sharedStyles";

interface Colors {
  [key: string]: string;
}

const colors: Colors = {
  primary: "bg-primary text-primary-content",
  secondary: "bg-secondary text-secondary-content",
  outline: "border-2 border-primary text-primary",
};

enum Size {
  default = "p-3",
  full = "w-full",
  small = "w-[100px] h-[9px]",
}

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  asChild?: boolean;
  variant?: "primary" | "secondary" | "darker" | "outline";
  color?: string;
  fontSize?: keyof typeof BodySize;
  weight?: keyof typeof fontWeight;
  size?: keyof typeof Size;
}

const Button = React.forwardRef<HTMLButtonElement, Props>(
  (
    {
      asChild,
      className,
      children,
      size = "default",
      variant = "primary",
      fontSize = "S",
      weight = "800",
      type = "button",
      ...props
    },
    ref,
  ) => {
    if (asChild)
      return (
        <button className={className} type={type} {...props}>
          {children}
        </button>
      );

    return (
      <button
        ref={ref}
        className={`${variant === "primary" && "text-white"} ${colors[variant]} ${BodySize[fontSize]} ${fontWeight[weight]} ${Size[size]} font-black rounded-[6px] p-[10px] ${className}`}
        type={type}
        {...props}
      >
        {children}
      </button>
    );
  },
);

Button.displayName = "Button in /components/modules/ui";
export default Button;
