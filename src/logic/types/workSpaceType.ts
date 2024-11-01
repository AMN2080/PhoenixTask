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
  // projects: ProjectProps;
  // members: MemberProps[];
}[];

export { type workSpacesType, type ProjectPropsType };
