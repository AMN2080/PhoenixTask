import { forwardRef, SVGProps } from "react";

const Tick = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      width="32"
      height="32"
      viewBox="0 0 32 27"
      xmlns="http://www.w3.org/2000/svg"
      ref={ref}
      {...rest}
    >
      <path
        fill="currentColor"
        d="M26.99 0L10.13 17.17l-5.44-5.54L0 16.41L10.4 27l4.65-4.73l.04.04L32 5.1z"
      />
    </svg>
  ),
);

Tick.displayName = "Tick Icon";
export default Tick;
