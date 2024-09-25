type ProjectPropsType = {
  _id: string;
  boards: [];
  name: string;
}[];

type workSpacesType = {
  _id: string;
  name: string;
  user: string;
  projects: ProjectPropsType;
}[];

export { type workSpacesType, type ProjectPropsType };
