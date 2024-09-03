import { forwardRef, SVGProps } from "react";

const Remove = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
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
        fill-rule="evenodd"
        clip-rule="evenodd"
        d="M13.6627 17.2544H6.98868C6.14475 17.2544 5.44349 16.6031 5.38062 15.7608L4.65759 5.96973H15.9697L15.2708 15.7567C15.2104 16.6007 14.5083 17.2544 13.6627 17.2544V17.2544Z"
        stroke-width="1.20907"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M10.3273 9.19385V14.0301"
        stroke-width="1.20907"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M3.87897 5.96967H16.7757"
        stroke-width="1.20907"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M14.3576 5.96979L13.5411 3.79185C13.3049 3.16233 12.7036 2.74561 12.0313 2.74561H8.62337C7.95113 2.74561 7.34982 3.16233 7.11364 3.79185L6.29712 5.96979"
        stroke-width="1.20907"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M13.0921 9.19385L12.7455 14.0301"
        stroke-width="1.20907"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
      <path
        d="M7.56261 9.19385L7.90921 14.0301"
        stroke-width="1.20907"
        stroke-linecap="round"
        stroke-linejoin="round"
      />
    </svg>
  ),
);

Remove.displayName = "Remove Icon";
export default Remove;
