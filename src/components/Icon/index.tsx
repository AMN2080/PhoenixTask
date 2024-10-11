import dynamic from "next/dynamic";
import { SVGProps } from "react";

const iconMap = {
  AccountInfo: dynamic(() => import("./icons/AccountInfo")),
  Calendar: dynamic(() => import("./icons/Calendar")),
  CheckBox: dynamic(() => import("./icons/CheckBox")),
  Close: dynamic(() => import("./icons/Close")),
  ColumnView: dynamic(() => import("./icons/ColumnView")),
  Comment: dynamic(() => import("./icons/Comment")),
  Description: dynamic(() => import("./icons/Description")),
  Edit: dynamic(() => import("./icons/Edit")),
  Emoji: dynamic(() => import("./icons/Emoji")),
  Eye: dynamic(() => import("./icons/Eye")),
  Flag: dynamic(() => import("./icons/Flag")),
  Link: dynamic(() => import("./icons/Link")),
  ListView: dynamic(() => import("./icons/ListView")),
  Loading: dynamic(() => import("./icons/Loading")),
  Logout: dynamic(() => import("./icons/Logout")),
  More: dynamic(() => import("./icons/More")),
  PersonalINfo: dynamic(() => import("./icons/PersonalInfo")),
  Profile: dynamic(() => import("./icons/Profile")),
  Remove: dynamic(() => import("./icons/Remove")),
  RoundArrow: dynamic(() => import("./icons/RoundArrow")),
  Setting: dynamic(() => import("./icons/Setting")),
  SquarePlus: dynamic(() => import("./icons/SquarePlus")),
  Tag: dynamic(() => import("./icons/Tag")),
  Tick: dynamic(() => import("./icons/Tick")),
  Update: dynamic(() => import("./icons/Update")),
  UserPlus: dynamic(() => import("./icons/UserPlus")),
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
