import { ReactNode } from "react";
import RouterLink from "next/link";
import {
  fontWeight,
  BodySize,
} from "../../../logic/SharedComponentStyles/sharedStyles";

interface Props {
  className?: string;
  children: ReactNode;
  to: string;
  weight?: keyof typeof fontWeight;
  underline?: boolean;
  colorVariant?: "primary" | "secondary";
  size?: keyof typeof BodySize;
}
const Link = ({
  children,
  to,
  underline = false,
  className,
  colorVariant = "primary",
  weight = "400",
  size = "XS",
  ...rest
}: Props) => {
  return (
    <RouterLink
      href={to}
      className={`font-IranYekan ${underline && "underline underline-offset-8"} ${fontWeight[weight]} ${BodySize[size]} ${className}`}
      {...rest}
    >
      {children}
    </RouterLink>
  );
};

export default Link;
