import { forwardRef, SVGProps } from "react";

const Setting = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
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
        d="M17.41 6.59L15 5.5l2.41-1.09L18.5 2l1.09 2.41L22 5.5l-2.41 1.09L18.5 9zm3.87 6.13L20.5 11l-.78 1.72l-1.72.78l1.72.78l.78 1.72l.78-1.72L23 13.5zm-5.04 1.65l1.94 1.47l-2.5 4.33l-2.24-.94c-.2.13-.42.26-.64.37l-.3 2.4h-5l-.3-2.41c-.22-.11-.43-.23-.64-.37l-2.24.94l-2.5-4.33l1.94-1.47c-.01-.11-.01-.24-.01-.36s0-.25.01-.37l-1.94-1.47l2.5-4.33l2.24.94c.2-.13.42-.26.64-.37L7.5 6h5l.3 2.41c.22.11.43.23.64.37l2.24-.94l2.5 4.33l-1.94 1.47c.01.12.01.24.01.37s0 .24-.01.36M13 14c0-1.66-1.34-3-3-3s-3 1.34-3 3s1.34 3 3 3s3-1.34 3-3"
      />
    </svg>
  ),
);

Setting.displayName = "Setting Icon";
export default Setting;
