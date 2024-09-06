"use client"

import ColumnSectionBar from "@/components/templates/ColumnSectionBar";

import { Draggable } from "react-beautiful-dnd";
import ColumnTaskCard from "@/components/templates/ColumnTaskCard";
// import { Task } from "../../../services/features/boards/boardSlice";
// import { StrictModeDroppable } from "../../ui/StrictModeDroppable";
// import { useAppSelector } from "../../../services/app/hook";

type BoardProps = {
  title: string;
  number: number;
  id: string;
  borderColor: string;
  index: number;
  tasks: Task[];
};

export default function ColumnView({
  title,
  number,
  tasks,
  borderColor,
  id,
  index,
}: BoardProps) {
  // const {  } = useAppSelector((state) => state.boards);
  const boardTasks = [...tasks];
  const sortedTasks = boardTasks.sort((a, b) => a.position - b.position);

  return (
    <Draggable
      key={id}
      draggableId={id}
      index={index}
      searchedTaskValue
      isDragDisabled={searchedTaskValue !== ""}
    >
      {(provided) => (
        <div {...provided.draggableProps} ref={provided.innerRef}>
          {/* Sticky Header */}
          <div {...provided.dragHandleProps}>
            <ColumnSectionBar
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
                    <ColumnTaskCard
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
}
