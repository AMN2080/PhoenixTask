import { forwardRef, InputHTMLAttributes } from "react";
import Flex from "./Flex";
import { fontSize } from "@/logic/SharedComponentStyles/sharedStyles";

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
      <Flex direction="col" gap="XS">
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
      </Flex>
    );
  },
);

Input.displayName = "Input in /components/modules/UI";
export default Input;
