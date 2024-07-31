import Image from "next/image";
import React, { ReactNode } from "react";
import Flex from "./Flex";
import { TiTick as Tick } from "react-icons/ti";

interface Props extends React.InputHTMLAttributes<HTMLInputElement> {
  label: ReactNode;
}

const CheckBox = React.forwardRef<HTMLInputElement, Props>(
  ({ id, label, className, ...rest }, ref) => {
    return (
      <Flex gap="XS" alignItems="center">
        <div className="w-[20px] h-[20px] relative">
          <input
            id={id}
            ref={ref}
            className={`peer appearance-none w-full h-full checked:bg-brand-secondary checked:border-brand-primary transition-all duration-300 border border-solid border-gray-999 rounded-[4px] ${className}`}
            type="checkbox"
            {...rest}
          />
          <span className="absolute hidden peer-checked:flex pointer-events-none inset-0 justify-center items-center">
            <Tick size={20} color="#228B22" />
          </span>
        </div>
        <label className="font-iranyekan" htmlFor={id}>
          {label}
        </label>
      </Flex>
    );
  },
);

CheckBox.displayName = "CheckBox in /components/modules/ui";
export default CheckBox;
