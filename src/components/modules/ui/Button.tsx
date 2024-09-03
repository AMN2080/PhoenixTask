import { ButtonHTMLAttributes, forwardRef } from "react";
import {
  fontWeight,
  fontSize,
} from "@/logic/SharedComponentStyles/sharedStyles";

// variant types of button
const variants = {
  primary: "bg-primary text-primary-content",
  secondary: "bg-secondary text-secondary-content",
  outline: "border-2 border-primary text-primary",
};

// button size
const Size = {
  default: "p-3",
  full: "w-full",
  small: "w-[100px] h-[9px]",
};

interface Props extends ButtonHTMLAttributes<HTMLButtonElement> {
  asChild?: boolean;
  variant?: "primary" | "secondary" | "outline";
  textSize?: keyof typeof fontSize;
  weight?: keyof typeof fontWeight;
  size?: keyof typeof Size;
}

const Button = forwardRef<HTMLButtonElement, Props>(
  (
    {
      asChild, // without this prop, button will be a default button + className
      className,
      children,
      size = "default",
      variant = "primary",
      textSize = "S",
      weight = "800",
      type = "button",
      ...props // onClick, onChange, etc.
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
        className={`
          ${variants[variant]}
          ${fontSize[textSize]}
          ${fontWeight[weight]}
          ${Size[size]}
          font-IranYekan font-black rounded-md p-[10px]
          ${className}
        `}
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
