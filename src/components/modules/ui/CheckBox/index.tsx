import { forwardRef, InputHTMLAttributes } from "react";
import { Icon } from "../";

interface Props extends InputHTMLAttributes<HTMLInputElement> {
  connectorId: string;
}

const CheckBox = forwardRef<HTMLInputElement, Props>(
  ({ connectorId, className = "", children, ...rest }, ref) => {
    return (
      <div className="w-full flex items-center gap-2">
        <div className="w-5 h-5 relative">
          <input
            id={connectorId}
            ref={ref}
            className={`
              appearance-none w-full h-full checked:bg-primary-content checked:border-primary transition-all duration-300 border border-solid border-gray-400 rounded-md
              ${className}
            `}
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
      </div>
    );
  },
);

CheckBox.displayName = "CheckBox in /components/modules/UI";
export default CheckBox;
