type ProjectPropsType = {
  id: string;
  boards: [];
  name: string;
}[];

type workSpacesType = {
  id: string;
  name: string;
  color: string;
  // user: string;
  // projects: ProjectPropsType;
}[];

export { type workSpacesType, type ProjectPropsType };
