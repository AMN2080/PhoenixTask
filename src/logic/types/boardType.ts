type taskAssignsType = {
  id: string;
  username: string;
  email: string;
};

type commentType = {
  id: string; // comment id
  text: string;
  user: {
    id: string; // user id
    username: string;
    firstname: string;
    email: string;
  };
  task: string;
  createdAt: string;
};

type taskType = {
  id: string;
  name: string;
  description: string;
  label?: [];
  board?: string;
  taskAssigns: taskAssignsType[];
  comments: commentType[];
  position: number;
  deadline: string;
  borderColor: string;
  title: string;
};

export { type taskAssignsType, type commentType, type taskType };
