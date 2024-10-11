import { Draggable } from "react-beautiful-dnd";
import Header from "@/components/templates/DashboardColumnView/Header";
import TaskCard from "@/components/templates/DashboardColumnView/TaskCard";
import { Task } from "@/logic/store/slices/boardSlice";
import { StrictModeDroppable } from "@/components/templates/StrictModeDroppable";
import { useAppSelector } from "@/logic/store/hook";

type ColumnViewBoardProps = {
  title: string;
  number: number;
  id: string;
  borderColor: string;
  index: number;
  tasks: Task[];
};

const ColumnViewBoard = ({
  title,
  number,
  tasks,
  borderColor,
  id,
  index,
}: ColumnViewBoardProps) => {
  const { searchedTaskValue } = useAppSelector((state) => state.boards);
  const boardTasks = [...tasks];
  const sortedTasks = boardTasks.sort((a, b) => a.position - b.position);

  return (
    <Draggable
      key={id}
      draggableId={id}
      index={index}
      isDragDisabled={searchedTaskValue !== ""}
    >
      {(provided) => (
        <div {...provided.draggableProps} ref={provided.innerRef}>
          {/* Sticky Header */}
          <div {...provided.dragHandleProps}>
            <Header
              title={title}
              number={number}
              borderColor={borderColor}
              id={id}
            />
          </div>
          <StrictModeDroppable droppableId={id} type="task">
            {(provided) => (
              <div
                className="min-w-[250px] h-fit max-h-[80vh] overflow-y-auto flex-shrink scrollbar-none pb-32"
                {...provided.droppableProps}
                ref={provided.innerRef}
              >
                {/* Task Cards */}
                {sortedTasks?.map(
                  (
                    {
                      name,
                      description,
                      _id,
                      comments,
                      label,
                      taskAssigns,
                      board,
                      deadline,
                    },
                    index,
                  ) => (
                    <TaskCard
                      borderColor={borderColor}
                      title={title}
                      position={index}
                      key={_id}
                      name={name}
                      description={description}
                      _id={_id}
                      label={label}
                      board={board}
                      taskAssigns={taskAssigns}
                      comments={comments}
                      deadline={deadline}
                    />
                  ),
                )}
                {provided.placeholder}
              </div>
            )}
          </StrictModeDroppable>
        </div>
      )}
    </Draggable>
  );
};

export default ColumnViewBoard;
