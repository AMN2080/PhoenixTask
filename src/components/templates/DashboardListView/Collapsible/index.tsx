import { useState } from "react";
import { Heading, Flex, Icon } from "@/components/modules/UI";

type CollapsibleProps = {
  title: string;
  titleClass: string;
  numberTask?: number;
  chevronClass: string;
  children: React.ReactNode;
  id?: string | undefined;
  onClick?: () => void;
};
const Collapsible = ({
  title,
  children,
  chevronClass,
  titleClass,
  numberTask,
}: CollapsibleProps) => {
  const [isExpanded, setIsExpanded] = useState(false);

  return (
    <div className="mt-8">
      <Flex
        alignItems="center"
        gap="XS"
        className="w-fit cursor-pointer"
        onClick={() => {
          setIsExpanded(!isExpanded);
        }}
      >
        <span className={chevronClass}>
          {isExpanded ? (
            <Icon
              iconName="RoundArrow"
              // className="transition-transform delay-75 ease-in-out"
            />
          ) : (
            <Icon
              iconName="RoundArrow"
              className="rotate-180 transition-transform delay-75 ease-in-out"
            />
          )}
        </span>
        <Heading as="h3" className={titleClass}>
          {title}
        </Heading>
        <Flex
          justifyContent="center"
          alignItems="center"
          className="text-sm font-normal"
        >
          <div>{numberTask && numberTask + " تسک"}</div>
        </Flex>
      </Flex>
      {isExpanded && (
        <Flex direction="col" className="">
          {children}
        </Flex>
      )}
    </div>
  );
};

export default Collapsible;
