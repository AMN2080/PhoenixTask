import { Task } from "@/components/templates/DashboardColumnView/TaskCard";
import HeaderRight from "./Header/HeaderRight";
import HeaderLeft from "./Header/HeaderLeft";
import BodyRight from "./Body/BodyRight";
import BodyLeft from "./Body/BodyLeft";
import { useEffect } from "react";
import { setSelectedBoardId, setSelectedTaskId } from "@/logic/store/store";
import { useAppDispatch } from "@/logic/store/hook";

type TaskInfoProps = {
  handleCloseTaskInfo: () => void;
  taskInfo: Task;
};

const TaskInfo = ({ handleCloseTaskInfo, taskInfo }: TaskInfoProps) => {
  const {
    comments,
    board,
    id,
    description,
    name,
    deadline,
    taskAssigns,
    borderColor,
    title,
  } = taskInfo;
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(setSelectedBoardId(board));
    dispatch(setSelectedTaskId(id));
  }, [dispatch, board, id]);

  return (
    <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-30 z-40 ">
      <div className=" fixed top-0 left-0 flex items-center justify-center z-30 w-full h-full">
        <div className="w-11/12 h-3/4 bg-white rounded-2xl p-8 relative overflow-hidden">
          <div className="w-full h-full divide-y divide-F4F4F4">
            {/* TaskInfo Header */}
            <section className="w-full  h-1/4 flex divide-x divide-F4F4F4 divide-x-reverse ">
              <HeaderRight
                borderColor={borderColor}
                taskAssigns={taskAssigns}
                title={title}
              />

              <HeaderLeft
                deadline={deadline}
                name={name}
                description={description}
                handleCloseTaskInfo={handleCloseTaskInfo}
              />
            </section>

            {/* ************************************************************ */}

            {/* TaskInfo Body */}
            <section className="w-full h-3/4 flex divide-x divide-F4F4F4 divide-x-reverse">
              <BodyRight
                description={description}
                name={name}
                deadline={deadline}
              />
              <BodyLeft comments={comments} taskId={id} />
            </section>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TaskInfo;
