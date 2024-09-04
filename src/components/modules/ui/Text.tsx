import {
  fontSize,
  fontWeight,
} from "../../../logic/SharedComponentStyles/sharedStyles";
interface Props {
  children?: string | string[] | number;
  colorVariant?: "primary" | "secondary";
  textSize?: keyof typeof fontSize;
  weight?: keyof typeof fontWeight;
  className?: string;
}

const Text = ({
  children,
  textSize = "XS",
  weight = "400",
  className = "",
}: Props) => {
  return (
    <p
      className={`
        font-IranYekan text-neutral-content
        ${fontSize[textSize]} 
        ${fontWeight[weight]} 
        ${className}
      `}
    >
      {children}
    </p>
  );
};

export default Text;
