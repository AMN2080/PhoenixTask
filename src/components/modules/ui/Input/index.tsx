import { forwardRef, InputHTMLAttributes } from "react";
import { fontSize } from "../sharedStyles";

interface Props extends InputHTMLAttributes<HTMLInputElement> {
  connectorId: string;
  label: string;
  type?: "text" | "email" | "password";
  textSize?: keyof typeof fontSize;
}

const Input = forwardRef<HTMLInputElement, Props>(
  (
    {
      className = "",
      connectorId,
      type = "text",
      label,
      textSize = "S",
      ...rest
    },
    ref,
  ) => {
    return (
      <div className="w-full flex flex-col gap-2">
        <label
          className={`
            font-IranYekan block text-neutral-content
            ${fontSize[textSize]}
          `}
          htmlFor={connectorId}
        >
          {label}
        </label>
        <input
          ref={ref}
          className={`
            outline-none w-full p-1 rounded-md border h-10
            ${className}
          `}
          id={connectorId}
          type={type}
          {...rest}
        />
      </div>
    );
  },
);

Input.displayName = "Input in /components/modules/UI";
export default Input;
