import { forwardRef } from "react";

import React from "react";
import Flex from "./Flex";
import { fontSize } from "../../../logic/SharedComponentStyles/sharedStyles";

interface Props extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  labelFontSize?: keyof typeof fontSize;
  labelColorVariant?: "primary" | "secondary";
}

const Input = forwardRef<HTMLInputElement, Props>(
  (
    {
      label,
      className,
      id,
      labelColorVariant = "primary",
      labelFontSize = "S",
      ...rest
    },
    ref,
  ) => {
    return (
      <Flex direction="col" gap="XS">
        <label
          className={`font-IranYekan block ${fontSize[labelFontSize]}`}
          htmlFor={id}
        >
          {label}
        </label>
        <input
          ref={ref}
          id={id}
          className={`outline-none w-full p-1 rounded-[6px] border border-solid border-gray-aaa h-[40px] ${className}`}
          type="text"
          {...rest}
        />
      </Flex>
    );
  },
);

Input.displayName = "Input in /components/modules/ui";
export default Input;
