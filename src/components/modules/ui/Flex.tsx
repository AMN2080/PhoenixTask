import { HTMLAttributes, ReactNode } from "react";

const gapSize = {
  XS: "gap-2",
  S: "gap-4",
  M: "gap-6",
  L: "gap-8",
  XL: "gap-10",
};
const FlexDirection = {
  row: "flex-row",
  row_reverse: "flex-row-reverse",
  col: "flex-col",
  col_reverse: "flex-col-reverse",
};

const justifyMap = {
  start: "justify-start",
  end: "justify-end",
  center: "justify-center",
  between: "justify-between",
  around: "justify-around",
  evenly: "justify-evenly",
};

const alignMap = {
  start: "items-start",
  end: "items-end",
  center: "items-center",
  baseline: "items-baseline",
  stretch: "items-stretch",
};

interface Props extends HTMLAttributes<HTMLDivElement> {
  children: ReactNode;
  direction?: keyof typeof FlexDirection;
  gap?: keyof typeof gapSize;
  justifyContent?: "start" | "end" | "center" | "between" | "around" | "evenly";
  alignItems?: "start" | "end" | "center" | "baseline" | "stretch";
  width?: "w-full" | "w-fit";
}

const Flex = ({
  children,
  justifyContent = "start",
  alignItems = "start",
  direction = "row",
  gap = "XS",
  className,
  width = "w-full",
  ...rest
}: Props) => {
  return (
    <div
      className={`flex 
      ${gapSize[gap]}
      ${FlexDirection[direction]} 
      ${justifyContent ? justifyMap[justifyContent] : "justify-start"}
      ${alignItems ? alignMap[alignItems] : "items-start"}
      ${className ?? ""}
      ${width}
      `}
      {...rest}
    >
      {children}
    </div>
  );
};

export default Flex;
