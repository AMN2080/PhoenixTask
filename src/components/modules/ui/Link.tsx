import { ReactNode } from "react";
import RouterLink from "next/link";
import {
  fontWeight,
  BodySize,
  colors,
} from "../../../logic/SharedComponentStyles/sharedStyles";

interface Props {
  className?: string;
  children: ReactNode;
  to: string;
  weight?: keyof typeof fontWeight;
  underline?: boolean;
  color?: keyof typeof colors;
  colorVariant?: "primary" | "secondary";
  size?: keyof typeof BodySize;
}
const Link = ({
  children,
  to,
  underline = false,
  color = "default",
  className,
  colorVariant = "primary",
  weight = "400",
  size = "XS",
  ...rest
}: Props) => {
  return (
    <RouterLink
      href={to}
      className={`font-iranyekan font-black text-2xl 
      ${colors[color][colorVariant]} 
      ${underline && "underline underline-offset-8"}
      ${fontWeight[weight]}
      ${BodySize[size]}
      ${className}
      `}
      {...rest}
    >
      {children}
    </RouterLink>
  );
};

export default Link;
