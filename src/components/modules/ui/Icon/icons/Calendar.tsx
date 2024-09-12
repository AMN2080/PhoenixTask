import { forwardRef, SVGProps } from "react";

const Calendar = forwardRef<SVGSVGElement, SVGProps<SVGSVGElement>>(
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
        d="M16 2V6"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M8 2V6"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M3 9H21"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        fillRule="evenodd"
        clipRule="evenodd"
        d="M19 4H5C3.895 4 3 4.895 3 6V19C3 20.105 3.895 21 5 21H19C20.105 21 21 20.105 21 19V6C21 4.895 20.105 4 19 4Z"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M7.01301 12.729C6.87501 12.729 6.76301 12.841 6.76401 12.979C6.76401 13.117 6.87601 13.229 7.01401 13.229C7.15201 13.229 7.26401 13.117 7.26401 12.979C7.26401 12.841 7.15201 12.729 7.01301 12.729"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M12.013 12.729C11.875 12.729 11.763 12.841 11.764 12.979C11.764 13.117 11.876 13.229 12.014 13.229C12.152 13.229 12.264 13.117 12.264 12.979C12.264 12.841 12.152 12.729 12.013 12.729"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M17.013 12.729C16.875 12.729 16.763 12.841 16.764 12.979C16.764 13.117 16.876 13.229 17.014 13.229C17.152 13.229 17.264 13.117 17.264 12.979C17.264 12.841 17.152 12.729 17.013 12.729"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M7.01301 16.729C6.87501 16.729 6.76301 16.841 6.76401 16.979C6.76401 17.117 6.87601 17.229 7.01401 17.229C7.15201 17.229 7.26401 17.117 7.26401 16.979C7.26401 16.841 7.15201 16.729 7.01301 16.729"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M12.013 16.729C11.875 16.729 11.763 16.841 11.764 16.979C11.764 17.117 11.876 17.229 12.014 17.229C12.152 17.229 12.264 17.117 12.264 16.979C12.264 16.841 12.152 16.729 12.013 16.729"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  ),
);

Calendar.displayName = "Calendar Icon";
export default Calendar;
