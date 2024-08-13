import {
  BodySize,
  fontWeight,
} from "../../../logic/SharedComponentStyles/sharedStyles";
import Text from "./Text";

interface Props {
  className?: string;
  children: string;
  size?: keyof typeof BodySize;
  weight?: keyof typeof fontWeight;
}
const TagBadge: React.FC<Props> = ({
  className,
  children,
  size,
  weight = "800",
}) => {
  return (
    <span
      className={`inline-flex items-center p-2 justify-center h-[24px] rounded-[14px] 
      ${className ?? ""}`}
    >
      <Text weight={weight} size={size}>
        {children}
      </Text>
    </span>
  );
};

export default TagBadge;
