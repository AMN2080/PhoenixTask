import { lazy, Suspense, SVGProps, ComponentType } from "react";

const iconMap = {
  Calendar: lazy(() => import("./icons/Calendar")),
  CheckBox: lazy(() => import("./icons/CheckBox")),
  Close: lazy(() => import("./icons/Close")),
  ColumnView: lazy(() => import("./icons/ColumnView")),
  Comment: lazy(() => import("./icons/Comment")),
  Description: lazy(() => import("./icons/Description")),
  ListView: lazy(() => import("./icons/ListView")),
  Loading: lazy(() => import("./icons/Loading")),
  Logout: lazy(() => import("./icons/Logout")),
  More: lazy(() => import("./icons/More")),
  Profile: lazy(() => import("./icons/Profile")),
  Remove: lazy(() => import("./icons/Remove")),
  SquarePlus: lazy(() => import("./icons/SquarePlus")),
  Tick: lazy(() => import("./icons/Tick")),
};

interface IconProps extends SVGProps<SVGSVGElement> {
  iconName: keyof typeof iconMap;
}

const Icon = ({ iconName, ...rest }: IconProps) => {
  const IconComponent = iconMap[iconName] as ComponentType<
    SVGProps<SVGSVGElement>
  >;
  if (!IconComponent) return null;

  return (
    <Suspense fallback={<div>Loading...</div>}>
      <IconComponent {...rest} />
    </Suspense>
  );
};

export default Icon;
