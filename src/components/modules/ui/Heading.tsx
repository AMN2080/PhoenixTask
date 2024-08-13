import { createElement } from "react";
import {
  fontWeight,
  Size,
} from "@/logic/SharedComponentStyles/sharedStyles";

enum TextAlign {
  center = "text-center",
  left = "text-left",
  right = "text-right",
}

interface Props {
  as: "h1" | "h2" | "h3" | "h4" | "h5" | "h6";
  align?: keyof typeof TextAlign;
  children: React.ReactNode;
  colorVariant?: "primary" | "secondary";
  size?: keyof typeof Size;
  weight?: keyof typeof fontWeight;
  className?: string;
}

const Heading: React.FC<Props> = ({
  as,
  children,
  colorVariant = "primary",
  size = "XS",
  weight = "800",
  align = "right",
  className,
}) => {
  return createElement(
    as,
    {
      className: `font-IranYekan ${Size[size]} ${fontWeight[weight]} ${className} ${TextAlign[align]}`,
    },
    children,
  );
};

export default Heading;
