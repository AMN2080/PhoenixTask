import { forwardRef, SVGProps } from "react";

const Description = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      width="12"
      height="13"
      viewBox="0 0 12 13"
      fill="none"
      stroke="#BDC0C6"
      xmlns="http://www.w3.org/2000/svg"
      ref={ref}
      {...rest}
    >
      <path
        d="M1.99899 2.49901H10.0023"
        stroke-width="0.75"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M3.49936 4.49998H10.0021"
        stroke-width="0.75"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M3.49936 6.50059H10.0021"
        stroke-width="0.75"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M1.99899 8.50157H10.0023"
        stroke-width="0.75"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M6.00058 10.5022H10.0022"
        stroke-width="0.75"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
    </svg>
  ),
);

Description.displayName = "Description Icon";
export default Description;
