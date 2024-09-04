import {
  fontSize,
  fontWeight,
} from "../../../logic/SharedComponentStyles/sharedStyles";
import Text from "./Text";

interface Props {
  className?: string;
  children: string;
  size?: keyof typeof fontSize;
  weight?: keyof typeof fontWeight;
}
const TagBadge: React.FC<Props> = ({
  children,
  size,
  weight = "800",
  className = "",
}) => {
  return (
    <span
      className={`
        inline-flex items-center p-2 justify-center h-6 rounded-2xl
        ${className}
      `}
    >
      <Text weight={weight} size={size}>
        {children}
      </Text>
    </span>
  );
};

export default TagBadge;
