import dynamic from "next/dynamic";
import { SVGProps } from "react";

const iconMap = {
  Calendar: dynamic(() => import("./icons/Calendar")),
  CheckBox: dynamic(() => import("./icons/CheckBox")),
  Close: dynamic(() => import("./icons/Close")),
  ColumnView: dynamic(() => import("./icons/ColumnView")),
  Comment: dynamic(() => import("./icons/Comment")),
  Description: dynamic(() => import("./icons/Description")),
  Flag: dynamic(() => import("./icons/Flag")),
  ListView: dynamic(() => import("./icons/ListView")),
  Loading: dynamic(() => import("./icons/Loading")),
  Logout: dynamic(() => import("./icons/Logout")),
  More: dynamic(() => import("./icons/More")),
  Profile: dynamic(() => import("./icons/Profile")),
  Remove: dynamic(() => import("./icons/Remove")),
  RoundArrow: dynamic(() => import("./icons/RoundArrow")),
  SquarePlus: dynamic(() => import("./icons/SquarePlus")),
  Tick: dynamic(() => import("./icons/Tick")),
};

interface IconProps extends SVGProps<SVGSVGElement> {
  iconName: keyof typeof iconMap;
}

const Icon = ({ iconName, ...rest }: IconProps) => {
  const IconComponent = iconMap[iconName];
  if (!IconComponent) return null;

  return <IconComponent {...rest} />;
};

export default Icon;
