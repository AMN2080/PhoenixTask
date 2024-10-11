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
    _id,
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
    dispatch(setSelectedTaskId(_id));
  }, [dispatch, board, _id]);

  return (
    <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-30 z-40 ">
      <div className=" fixed top-0 left-0 flex items-center justify-center z-30 w-full h-full">
        <div className="w-11/12 h-3/4 bg-white dark:bg-[#15202b] rounded-2xl p-8 relative overflow-hidden">
          <div className="w-full h-full divide-y divide-F4F4F4 dark:divide-[#57585f] dark:text-[#f7f9f9] ">
            {/* TaskInfo Header */}
            <section className="w-full  h-1/4 flex divide-x divide-F4F4F4 dark:divide-[#57585f] divide-x-reverse ">
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
            <section className="w-full h-3/4 flex divide-x divide-F4F4F4 dark:divide-[#57585f] divide-x-reverse">
              <BodyRight
                description={description}
                name={name}
                deadline={deadline}
              />
              <BodyLeft comments={comments} taskId={_id} />
            </section>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TaskInfo;
