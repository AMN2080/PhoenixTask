import { forwardRef, SVGProps } from "react";

const More = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      width="20"
      height="20"
      viewBox="0 0 20 20"
      fill="none"
      stroke="#323232"
      xmlns="http://www.w3.org/2000/svg"
      ref={ref}
      {...rest}
    >
      <path
        d="M15.4189 10.0002C15.4189 10.2304 15.2323 10.4171 15.002 10.4171C14.7718 10.4171 14.5852 10.2304 14.5852 10.0002C14.5852 9.77 14.7718 9.58337 15.002 9.58337C15.2323 9.58337 15.4189 9.77 15.4189 10.0002"
        stroke-width="1.25"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M10.4168 10.0002C10.4168 10.2304 10.2302 10.4171 9.99997 10.4171C9.76976 10.4171 9.58313 10.2304 9.58313 10.0002C9.58313 9.77 9.76976 9.58337 9.99997 9.58337C10.2302 9.58337 10.4168 9.77 10.4168 10.0002"
        stroke-width="1.25"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M5.4148 10.0002C5.4148 10.2304 5.22817 10.4171 4.99796 10.4171C4.76774 10.4171 4.58112 10.2304 4.58112 10.0002C4.58112 9.77 4.76774 9.58337 4.99796 9.58337C5.22817 9.58337 5.4148 9.77 5.4148 10.0002"
        stroke-width="1.25"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
    </svg>
  ),
);

More.displayName = "More Icon";
export default More;
