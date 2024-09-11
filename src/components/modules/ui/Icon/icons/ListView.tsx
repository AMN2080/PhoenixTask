import { forwardRef, SVGProps } from "react";

const ListView = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
  ({ ...rest }, ref) => (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      stroke="#323232"
      xmlns="http://www.w3.org/2000/svg"
      ref={ref}
      {...rest}
    >
      <path
        d="M11 12H21"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M6.41399 10.586C7.19499 11.367 7.19499 12.633 6.41399 13.414C5.63299 14.195 4.36699 14.195 3.58599 13.414C2.80499 12.633 2.80499 11.367 3.58599 10.586C4.36699 9.80499 5.63299 9.80499 6.41399 10.586"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M11 5H21"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M3.02002 4.508L4.67302 5.996L8.00002 3"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M11 19H21"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M6.41399 17.586C7.19499 18.367 7.19499 19.633 6.41399 20.414C5.63299 21.195 4.36699 21.195 3.58599 20.414C2.80499 19.633 2.80499 18.367 3.58599 17.586C4.36699 16.805 5.63299 16.805 6.41399 17.586"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  ),
);

ListView.displayName = "ListView Icon";
export default ListView;
