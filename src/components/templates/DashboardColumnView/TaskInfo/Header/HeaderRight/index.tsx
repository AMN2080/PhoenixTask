import Icon from "@/components/Icon";
import { taskAssignsType } from "@/logic/types/boardType";
import TaskAssign from "../../TaskAssign";
import { useState } from "react";

export type TaskInfoHeaderRightType = {
  taskAssigns: taskAssignsType[];
  borderColor: string;
  title: string;
};

const TaskInfoHeaderRight = ({
  taskAssigns,
  borderColor,
  title,
}: TaskInfoHeaderRightType) => {
  const [taskAssignModal, setTaskAssignModal] = useState(false);
  const colors = JSON.parse(localStorage.getItem("Colors") as string);

  const btnColor = borderColor.split("-")[2];
  const shadowColor = `shadow-[0px_0px_0px_2px_#${btnColor}]`;

  return (
    <div className="w-1/2 h-full ml-[1px] relative ">
      <div className="w-full h-14 absolute bottom-6 flex place-content-between items-center px-4">
        {/* Task Info Right */}
        <div className="h-9 flex items-center gap-7">
          {/* Status Changer */}
          <div
            className={`flex rounded-sm group hover:${shadowColor} transition-all`}
          >
            <button
              className={`w-28 h-8 text-white bg-${btnColor} p-1 justify-center truncate group-hover:rounded-s-md  group-hover:scale-110 transition-all`}
            >
              {title}
            </button>
          </div>

          {/* Assign task */}
          <ul dir="ltr" className="flex -space-x-2 ">
            <li onClick={() => setTaskAssignModal(true)}>
              <div className="border-[1.5px] border-dashed rounded-full p-1.5 cursor-pointer border-[#C1C1C1]">
                <Icon
                  iconName="UserPlus"
                  className="text-[#C1C1C1] text-[20px]"
                />
              </div>{" "}
            </li>
            <>
              {[...taskAssigns].slice(0, 3).map((user, index) => (
                <li key={user.id} className="w-8 h-8 cursor-pointer ">
                  <div
                    className={`${colors[index]} w-full h-full rounded-full flex items-center justify-center pt-1 text-white`}
                  >
                    {user.username.substring(0, 2)}
                  </div>
                </li>
              ))}
              {taskAssigns.length > 3 && (
                <div className="w-8 h-8 cursor-pointer ">
                  <div
                    className={`bg-[#0A111B] w-full h-full rounded-full flex items-center justify-center pt-1 text-white`}
                  >
                    +{taskAssigns.length - 3}
                  </div>
                </div>
              )}
            </>
          </ul>

          {taskAssignModal && (
            <TaskAssign
              taskAssigns={taskAssigns}
              setTaskAssignModal={setTaskAssignModal}
            />
          )}

          {/* Priority Flag */}
          <div className="border-[1.5px] border-dashed rounded-full p-1.5 cursor-pointer border-error">
            <Icon iconName="Flag" className="text-error text-xl" />
          </div>
        </div>
        {/* Share */}
        <div>
          <div
            className="flex items-center justify-center gap-1 "
            role="button"
          >
            <Icon iconName="Share" className="text-[#BDBDBD] text-2xl" />
            <span className="text-base font-dana font-medium text-base-content">
              اشتراک گذاری
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TaskInfoHeaderRight;
