type taskAssignsType = {
  _id: string;
  username: string;
  email: string;
};

type commentType = {
  _id: string; // comment _id
  text: string;
  user: {
    _id: string; // user _id
    username: string;
    firstname: string;
    email: string;
  };
  task: string;
  createdAt: string;
};

type taskType = {
  _id: string;
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
