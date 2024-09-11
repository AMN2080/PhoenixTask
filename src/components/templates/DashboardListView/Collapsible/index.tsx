import { useState } from "react";
import { Icon } from "@/components/modules/UI";

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
      <div
        className="flex items-center gap-2 w-fit cursor-pointer"
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
        <h3 className={titleClass}>{title}</h3>
        <div className="text-sm font-normal flex items-center justify-between">
          <div>{numberTask && numberTask + " تسک"}</div>
        </div>
      </div>
      {isExpanded && <div className="flex flex-col">{children}</div>}
    </div>
  );
};

export default Collapsible;
