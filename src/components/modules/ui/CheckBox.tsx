import { forwardRef, InputHTMLAttributes } from "react";
import Icon from "../Icon";
import Flex from "./Flex";

interface Props extends InputHTMLAttributes<HTMLInputElement> {
  connectorId: string;
}

const CheckBox = forwardRef<HTMLInputElement, Props>(
  ({ connectorId, className, children, ...rest }, ref) => {
    return (
      <Flex gap="XS" alignItems="center">
        <div className="w-5 h-5 relative">
          <input
            id={connectorId}
            ref={ref}
            className={`peer appearance-none w-full h-full checked:bg-brand-secondary checked:border-brand-primary transition-all duration-300 border border-solid border-gray-999 rounded-[4px] ${className}`}
            type="checkbox"
            {...rest}
          />
          <span className="absolute hidden peer-checked:flex pointer-events-none inset-0 justify-center items-center">
            <Icon iconName="Tick" className="text-green-600 w-4" />
          </span>
        </div>
        <label className="font-IranYekan" htmlFor={connectorId}>
          {children}
        </label>
      </Flex>
    );
  },
);

CheckBox.displayName = "CheckBox in /components/modules/ui";
export default CheckBox;
