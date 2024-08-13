import {
  BodySize,
  fontWeight,
} from "../../../logic/SharedComponentStyles/sharedStyles";
interface Props {
  children?: string | string[] | number;
  colorVariant?: "primary" | "secondary";
  size?: keyof typeof BodySize;
  weight?: keyof typeof fontWeight;
  className?: string;
}

const Text = ({
  children,
  colorVariant = "primary",
  size = "XS",
  weight = "400",
  className,
}: Props) => {
  return (
    <p
      className={`font-IranYekan 
      ${BodySize[size]} 
      ${fontWeight[weight]} 
      ${className ?? ""}`}
    >
      {children}
    </p>
  );
};

export default Text;
