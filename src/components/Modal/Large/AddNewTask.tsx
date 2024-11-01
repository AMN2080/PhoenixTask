import { useState } from "react";
import { Button } from "@/components/UI";
import Icon from "@/components/Icon";
import { createPortal } from "react-dom";
import QuckCalendar from "./QuickCalendar";
import { toast } from "react-toastify";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { toggleMediumModal } from "@/logic/store/store";
import { Flex } from "@/components/UI";

type addNewTaskProps = {
  handleAddNewTask?: ((data: (string | undefined)[]) => void) | undefined;
  boardList?: object[] | undefined;
};

const AddNewTask = ({ handleAddNewTask }: addNewTaskProps) => {
  const [calendar, setCalendar] = useState({
    modal: false,
    value: "",
  });

  const dispatch = useAppDispatch();
  const { isLoading } = useAppSelector((state) => state.tasks);

  const handleCalendar = (modalState: boolean, value?: string) => {
    setCalendar({ ...calendar, modal: modalState, value: value ?? "" });
  };

  const showTime = calendar.value ? calendar.value.substring(8, 10) : "";

  const handleNewTaskButton = () => {
    const taskDisc = document.querySelector<HTMLTextAreaElement>("#descTask");
    const taskTitle = document.querySelector<HTMLInputElement>("#taskTitle");
    const calendarEl = document.querySelector<HTMLInputElement>("#calendar");
    // const data = [taskTitle, taskDisc, calendar.value];

    if (!taskTitle?.value.trim()) {
      taskTitle?.classList.add("border-b");
      taskTitle?.classList.add("border-red-500");
    } else {
      taskTitle?.classList.remove("border-b");
      taskTitle?.classList.remove("border-red-500");
    }

    if (!taskDisc?.value.trim()) {
      taskDisc?.classList.add("border-b");
      taskDisc?.classList.add("border-red-500");
    } else {
      taskDisc?.classList.remove("border-b");
      taskDisc?.classList.remove("border-red-500");
    }

    if (!calendar.value) {
      calendarEl?.classList.add("border-b");
      calendarEl?.classList.add("text-red-500");
      calendarEl?.classList.add("border-red-500");
    } else {
      calendarEl?.classList.remove("border-b");
      calendarEl?.classList.remove("text-red-500");
      calendarEl?.classList.remove("border-red-500");
    }

    if ((taskDisc?.value && taskTitle?.value && calendar.value)?.trim()) {
      const sanitizedValues = [
        taskTitle?.value,
        taskDisc?.value,
        calendar.value,
      ].map((value) => (value ? value : undefined));
      handleAddNewTask && handleAddNewTask(sanitizedValues);
    } else {
      toast.warning("بخش‌های لازم را وارد کنید", { rtl: true });
    }
  };

  const listOfIcons = `w-12 h-12 text-xl rounded-full text-C1C1C1 border-C1C1C1 border-2 border-dashed flex justify-center items-center cursor-pointer`;

  return (
    <>
      <div className="modal-box overflow-visible opacity-100 z-30 py-9 px-11 rounded-2xl shadow-xl w-11/12 max-w-5xl min-w-[1000px]">
        <div>
          <Flex direction="col">
            {/* task header */}
            <Flex justifyContent="between" alignItems="center">
              <Flex direction="col">
                <Flex alignItems="center">
                  <div className={`h-4 w-4 mr-3 rounded-sm bg-[#D3D3D3]`}></div>
                  <input
                    type="text"
                    id="taskTitle"
                    name="taskTitle"
                    autoComplete="off"
                    placeholder="نام تسک را وارد کنید"
                    className="mr-3 text-2xl text-black font-medium focus:outline-none"
                    required
                  />
                </Flex>
              </Flex>
              <Button
                onClick={() => dispatch(toggleMediumModal(""))}
                asChild
                className="text-2xl hover:text-red-600 hover:rotate-90 transition-all flex-none"
              >
                <Icon iconName="Close" />
              </Button>
            </Flex>

            {/* task subHeader */}
            <Flex
              alignItems="center"
              className="mt-10 text-base text-black font-medium"
            >
              در
              <input
                type="text"
                name="forMember"
                id="forMember"
                placeholder="پروژه اول"
                className="w-40 mx-2 text-base font-normal border px-2 py-1 rounded-md  focus:outline-none"
              />
              برای
              <span className="w-9 h-9 mr-3 p-1 text-neutral rounded-full border-2 border-dashed flex justify-center items-center">
                <Icon iconName="UserPlus" />
              </span>
            </Flex>

            {/* task inputs */}
            <div className="w-full h-48 mt-10">
              <textarea
                name="descTask"
                id="descTask"
                placeholder="توضیحاتی برای تسک بنویسید"
                className={`w-full h-full text-base font-normal border rounded-xl p-5 resize-none focus:outline-none`}
              ></textarea>
            </div>

            <Flex alignItems="center" className="w-full mt-11">
              <div className="font-normal text-base">افزودن پیوست</div>
              <label
                htmlFor="addFileTask"
                className="w-28 border border-primary p-1 rounded-md flex justify-center items-center text-base font-normal mr-4 text-primary"
              >
                <Icon iconName="Link" />
                <span className="text-black mr-1">آپلود فایل</span>
              </label>

              <input
                type="file"
                name="addFileTask"
                id="addFileTask"
                className="hidden"
              />
            </Flex>

            {/* task footer */}
            <Flex
              justifyContent="between"
              alignItems="center"
              className="w-full mt-11"
            >
              {/* list of icons */}
              <ul className="w-72 relative flex items-center justify-between">
                {/* priority */}
                <li className={`${listOfIcons}`}>
                  <span>
                    <Icon iconName="Flag" />
                  </span>
                </li>
                <li
                  id="calendar"
                  className={`${listOfIcons} ${
                    showTime != "" && "!text-primary !border-primary"
                  }  `}
                  onClick={() => handleCalendar(true)}
                >
                  {calendar.value === "" ? (
                    <Icon iconName="Calendar" />
                  ) : (
                    showTime
                  )}
                </li>

                <li className={listOfIcons}>
                  <Icon iconName="Tag" />
                </li>

                <li
                  className={`w-12 h-12 text-[#C1C1C1] -z-10 rounded-full border-2 flex justify-center items-center cursor-pointer border-none relative text-6xl`}
                >
                  <Icon iconName="Eye" />
                  <span className="h-6 w-6 rounded-full -top-2 right-0 flex justify-center items-center absolute text-sm bg-[#4AB7D8] text-black">
                    ۲
                  </span>
                </li>
              </ul>

              {/* create task button */}

              <div className="w-32 h-8">
                {isLoading ? (
                  <Button
                    size="full"
                    className="disabled:pointer-events-none h-10 p-2.5 text-sm font-bold leading-4 flex justify-center items-center"
                  >
                    <Icon iconName="More" />
                  </Button>
                ) : (
                  <Button onClick={handleNewTaskButton}>ساخت تسک</Button>
                )}
              </div>
            </Flex>
          </Flex>
        </div>
        {/* modals on modals */}
        {calendar.modal &&
          createPortal(
            <QuckCalendar handleCalendar={handleCalendar} />,
            document.body,
          )}
      </div>
    </>
  );
};

export default AddNewTask;
