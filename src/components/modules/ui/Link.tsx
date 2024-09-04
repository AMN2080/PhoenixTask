import { ReactNode } from "react";
import RouterLink from "next/link";
import {
  fontWeight,
  fontSize,
} from "@/logic/SharedComponentStyles/sharedStyles";

interface Props {
  className?: string;
  children: ReactNode;
  to: string;
  weight?: keyof typeof fontWeight;
  underline?: boolean;
  textSize?: keyof typeof fontSize;
}

const Link = ({
  children,
  to,
  underline = false,
  className,
  weight = "400",
  textSize = "XS",
  ...rest
}: Props) => {
  return (
    <RouterLink
      href={to}
      className={`
        font-IranYekan text-primary
        ${underline && "underline underline-offset-8"}
        ${fontWeight[weight]}
        ${fontSize[textSize]}
        ${className}
      `}
      {...rest}
    >
      {children}
    </RouterLink>
  );
};

export default Link;
