import { useState } from "react";
import { createPortal } from "react-dom";
import Icon from "@/components/Icon";
// import { FiCheckCircle } from "react-icons/fi";
// import { TbCalendarTime } from "react-icons/tb";
import TaskInfo from "@/components/templates/DashboardColumnView/TaskInfo";
import { Draggable } from "react-beautiful-dnd";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { fetchDeleteTask } from "@/logic/store/store";
import { getPersianDateWithOutTime } from "@/logic/utils/date/getPersianDate";
import getGregorianDate from "@/logic/utils/date/getGregorianDate";
import ConfirmDelete from "@/components/templates/DashboardColumnView/ConfirmDelete";

export type taskAssignsType = {
  id: string;
  username: string;
  email: string;
};

export type commentType = {
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

export type Task = {
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

const ColumnTaskCard = ({
  name,
  description,
  id,
  position,
  comments,
  taskAssigns,
  board,
  deadline,
  borderColor,
  title,
}: Task) => {
  const taskInfo = {
    name,
    description,
    id,
    comments,
    taskAssigns,
    position,
    board,
    deadline,
    borderColor,
    title,
  };
  const [isExpanded, setIsExpanded] = useState(false);
  const [isOpen, setIsOpen] = useState(false);
  const { searchedTaskValue } = useAppSelector((state) => state.boards);
  const colors = JSON.parse(localStorage.getItem("Colors") as string);

  const [confirm, setConfirm] = useState(false);
  const handleCloseTaskInfo = () => {
    setIsOpen(false);
  };

  const { isLoading } = useAppSelector((state) => state.tasks);
  const dispatch = useAppDispatch();
  const handleCardHover = (isHovering: boolean) => {
    setIsExpanded(isHovering);
  };

  const handleDeleteTask = (event: React.MouseEvent) => {
    event.stopPropagation();
    dispatch(fetchDeleteTask(id));
  };

  return (
    <>
      <Draggable
        key={id}
        draggableId={id}
        index={position}
        isDragDisabled={searchedTaskValue !== ""}
      >
        {(provided) => (
          <div
            {...provided.draggableProps}
            {...provided.dragHandleProps}
            ref={provided.innerRef}
            className={`border w-[250px] rounded p-3 cursor-pointer bg-white text-base-content shadow-[0px_6px_8px_rgba(0,0,0,0.14)] mb-3`}
            onMouseOver={() => handleCardHover(true)}
            onMouseLeave={() => !confirm && handleCardHover(false)}
            onClick={() => setIsOpen(true)}
          >
            <div className="flex justify-between items-baseline mb-4">
              <div className="h-4 font-medium leading-4 text-right text-534D60 text-xs">
                {name}
              </div>
              <div
                dir="ltr"
                className={`flex justify-end -space-x-3 transition-opacity delay-100 ${
                  isExpanded ? "visible opacity-100" : "invisible opacity-0"
                }
            `}
              >
                {taskAssigns.length ? (
                  <>
                    {[...taskAssigns].slice(0, 2).map((member, index) => (
                      <div className="w-6 h-6 text-xs" key={member.id}>
                        <div
                          className={`${colors[index]} w-full h-full rounded-full flex items-center justify-center pt-1 text-white border`}
                        >
                          {member.username.substring(0, 2)}
                        </div>
                      </div>
                    ))}
                    {taskAssigns.length > 2 && (
                      <div className="w-6 h-6 text-xs ">
                        <div
                          className={`bg-323232 w-full h-full rounded-full flex items-center justify-center pt-1 text-white border-2`}
                        >
                          +{taskAssigns.length - 2}
                        </div>
                      </div>
                    )}
                  </>
                ) : (
                  "‌‌‌‌‌‌"
                )}
              </div>
            </div>
            <div className="flex items-center justify-start mb-3 gap-1">
              <div className="text-[11px] text-0E0E0E leading-4 text-right">
                {description}
              </div>
              <div className="text-xs text-BDC0C6 ">
                <Icon iconName="Description"></Icon>
              </div>
            </div>
            <div className="flex justify-start items-center pb-6 pt-2 text-[10px] gap-1">
              <span className="text-FB0606">
                <Icon iconName="Calendar"></Icon>
                {/* <TbCalendarTime size="0.9rem" /> */}
              </span>
              <div className="text-343434 text-xs mt-1">
                {deadline
                  ? getPersianDateWithOutTime(getGregorianDate(deadline))
                  : "تعریف نشده"}
              </div>
            </div>
            <div className="flex justify-between">
              <div className="flex items-center gap-3">
                <span className="bg-BFFDE3 text-[10px] p-1 rounded-l-2xl">
                  درس
                </span>
                <span className="bg-EEDFF6 text-[10px] p-1 rounded-l-2xl">
                  پروژه
                </span>
              </div>
              <div className="text-BDC0C6 flex items-center gap-1">
                <span className="text-xs mt-[5px]">{comments.length}</span>
                <Icon iconName="Comment"></Icon>
              </div>
            </div>
            <div
              className={` overflow-hidden justify-between flex border-t  transition-all duration-500 
              ${isExpanded ? "h-9 mt-5 pt-4" : "h-[0px] opacity-0 mt-0 pt-0"}
              `}
            >
              {confirm ? (
                <ConfirmDelete
                  status="تسک"
                  cancel={setConfirm}
                  accept={handleDeleteTask}
                />
              ) : (
                <>
                  <div className="hover:text-primary hover:scale-110 cursor-pointer">
                    {/* <FiCheckCircle /> */}
                    <Icon iconName="CheckBox" />
                  </div>
                  {isLoading ? (
                    <Icon iconName="More"></Icon>
                  ) : (
                    <div
                      className="hover:scale-110 cursor-pointer"
                      onClick={(e) => {
                        e.stopPropagation();
                        setConfirm(true);
                      }}
                    >
                      <Icon iconName="Remove"></Icon>
                    </div>
                  )}
                </>
              )}
            </div>
          </div>
        )}
      </Draggable>
      {isOpen &&
        createPortal(
          <TaskInfo
            taskInfo={taskInfo}
            handleCloseTaskInfo={handleCloseTaskInfo}
          />,
          document.body,
        )}
    </>
  );
};

export default ColumnTaskCard;
