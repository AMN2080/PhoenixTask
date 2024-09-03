import { forwardRef, SVGProps } from "react";

const Logout = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      width="24"
      height="24"
      viewBox="0 0 512 512"
      xmlns="http://www.w3.org/2000/svg"
      ref={ref}
      {...rest}
    >
      <path
        fill="none"
        stroke="currentColor"
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="32"
        d="M320 176v-40a40 40 0 0 0-40-40H88a40 40 0 0 0-40 40v240a40 40 0 0 0 40 40h192a40 40 0 0 0 40-40v-40m64-160l80 80l-80 80m-193-80h273"
      />
    </svg>
  ),
);

Logout.displayName = "Logout Icon";
export default Logout;
