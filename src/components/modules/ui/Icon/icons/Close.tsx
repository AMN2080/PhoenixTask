import { forwardRef, SVGProps } from "react";

const Close = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      width="32"
      height="33"
      viewBox="0 0 32 33"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      stroke="#323232"
      ref={ref}
      {...rest}
    >
      <path
        d="M10.6666 11.1666L21.3333 21.8333"
        stroke-width="1.5"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M21.3333 11.1666L10.6666 21.8333"
        stroke-width="1.5"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
    </svg>
  ),
);

Close.displayName = "Close Icon";
export default Close;
