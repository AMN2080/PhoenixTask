import Icon from "@/components/Icon";
import { createPortal } from "react-dom";
import QuckCalendar from "@/components/Modal/Large/QuickCalendar";
import { useState } from "react";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { fetchUpdateTask } from "@/logic/store/store";
import { getPersianDateWithOutTime } from "@/logic/utils/date/getPersianDate";
import getGregorianDate from "@/logic/utils/date/getGregorianDate";

type TaskInfoHeaderLeftProps = {
  handleCloseTaskInfo: () => void;
  deadline: string | undefined;
  description: string;
  name: string;
};

const TaskInfoHeaderLeft = ({
  handleCloseTaskInfo,
  deadline,
  description,
  name,
}: TaskInfoHeaderLeftProps) => {
  const [calendar, setCalendar] = useState({
    modal: false,
    value: "",
  });
  const handleCalendar = (modalState: boolean) => {
    setCalendar({ ...calendar, modal: modalState });
  };

  const dispatch = useAppDispatch();
  const { selectedTaskId } = useAppSelector((state) => state.boards);

  const submitChangesHandler = (deadline: string) => {
    dispatch(
      fetchUpdateTask({
        description,
        name,
        deadline,
        taskId: selectedTaskId,
      }),
    );
  };

  return (
    <div className="w-1/2 h-full relative">
      <div className="w-full h-14 px-4  absolute bottom-6 flex justify-between items-center">
        {/* Task Info Left */}
        <div className="flex h-full items-center divide-x divide-F4F4F4 divide-x-reverse">
          {/* Creation Date */}
          <div className="h-full pl-8 ">
            <span className="text-BBBBBB text-xs font-medium">
              ساخته شده در
            </span>
            <p className="text-base-content text-base font-medium">
              1 اردیبهشت 1402
            </p>
          </div>
          {/* Deadline */}
          <div
            onClick={() => handleCalendar(true)}
            className="h-full pr-7 ml-auto cursor-pointer"
          >
            <div className="flex gap-2">
              <span className="text-BBBBBB text-xs font-medium">ددلاین</span>
              <Icon iconName="Calendar" />
            </div>
            <div className="text-base-content text-base font-medium">
              {deadline
                ? getPersianDateWithOutTime(getGregorianDate(deadline))
                : "تعریف نشده"}
            </div>
          </div>
        </div>
      </div>

      {/* Closing window */}
      <span onClick={handleCloseTaskInfo}>
        <Icon
          iconName="Close"
          className="hover:rotate-90 hover:text-error transition-all cursor-pointer absolute left-3 top-2 text-neutral"
        />
      </span>
      {calendar.modal &&
        createPortal(
          <QuckCalendar
            submitChangesHandler={submitChangesHandler}
            handleCalendar={handleCalendar}
          />,
          document.body,
        )}
    </div>
  );
};

export default TaskInfoHeaderLeft;
