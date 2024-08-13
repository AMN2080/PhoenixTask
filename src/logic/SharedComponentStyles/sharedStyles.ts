const fontWeight = {
  "100": "font-thin",
  "200": "font-ultraLight",
  "300": "font-light",
  "400": "font-normal",
  "500": "font-medium",
  "550": "font-demiBold",
  "600": "font-bold",
  "700": "font-extraBold",
  "800": "font-black",
  "900": "font-extraBlack",
  "950": "font-heavy",
};

enum Size {
  XS = "text-xl",
  S = "text-2xl",
  M = "text-3xl",
  L = "text-4xl",
}

enum BodySize {
  XS = "text-xs",
  S = "text-sm",
  M = "text-base",
  L = "text-xl",
  XL = "text-2xl",
}

export { fontWeight, BodySize, Size };
