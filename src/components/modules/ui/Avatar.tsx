import Text from "./Text";
import { fontSize } from "../../../logic/SharedComponentStyles/sharedStyles";

interface Props {
  firstName: string;
  lastName: string;
  textSize?: keyof typeof fontSize;
  className?: string;
}

const Avatar: React.FC<Props> = ({
  firstName,
  lastName,
  textSize,
  className,
}) => {
  return (
    <span
      className={`pt-[6.1px] pr-[5.42px] pb-[4.75px] pl-[5.42px] inline-flex justify-center items-center rounded-full ${className}`}
    >
      <Text weight="500" size={textSize}>
        {firstName[0].toUpperCase() + lastName[0].toUpperCase()}
      </Text>
    </span>
  );
};

export default Avatar;
