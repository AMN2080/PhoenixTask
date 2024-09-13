import { forwardRef, SVGProps } from "react";

const RoundArrow = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width="32"
      height="32"
      viewBox="0 0 24 24"
      ref={ref}
      {...rest}
    >
      <path
        fill="#888888"
        d="M12.382 3a1 1 0 0 1 .894.553L14 5h6a1 1 0 0 1 1 1v11a1 1 0 0 1-1 1h-6.382a1 1 0 0 1-.894-.553L12 16H5v6H3V3zm-.618 2H5v9h8.236l1 2H19V7h-6.236z"
      />
    </svg>
  ),
);

RoundArrow.displayName = "RoundArrow Icon";
export default RoundArrow;
