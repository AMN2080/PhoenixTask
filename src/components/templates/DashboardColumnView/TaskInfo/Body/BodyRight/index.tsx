import { Button } from "@/components/UI";
import Icon from "@/components/Icon";
import { useRef } from "react";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { fetchUpdateTask } from "@/logic/store/store";

type TaskInfoBodyRightType = {
  description: string;
  name: string;
  deadline: string;
};

const TaskInfoBodyRight = ({
  description,
  name,
  deadline,
}: TaskInfoBodyRightType) => {
  const nameRef = useRef<HTMLHeadingElement>(null);
  const descriptionRef = useRef<HTMLTextAreaElement>(null);

  const dispatch = useAppDispatch();
  const { selectedTaskId } = useAppSelector((state) => state.boards);

  const submitChangesHandler = () => {
    if (
      (descriptionRef.current?.value !== description ||
        nameRef.current?.textContent !== name) &&
      descriptionRef.current &&
      descriptionRef.current.value &&
      nameRef.current?.textContent
    ) {
      dispatch(
        fetchUpdateTask({
          description: descriptionRef.current.value,
          name: nameRef.current.textContent,
          deadline,
          taskId: selectedTaskId,
        }),
      );
    }
  };
  return (
    <div className="w-1/2 box-border overflow-auto scrollbar-thin scrollbar-thumb-gray-200 scrollbar-thumb-rounded-full scrollbar-track-white ml-[1px]">
      {/* TaskInfo Body Right Container */}
      <div className="mx-4 my-6 flex flex-col gap-5">
        {/* Tags */}
        <div className="flex items-center justify-start gap-2 ">
          <div className="border-[1.5px]  border-dashed rounded-full p-1.5 cursor-pointer border-[#C1C1C1]">
            <Icon iconName="Tag" className="text-xl" />
          </div>
          {/* Badges */}
          <div className="flex gap-1 ">
            <span className="badge-md leading-6 rounded-md bg-blue-600 text-white ">
              front
            </span>
            <span className="badge-md leading-6 rounded-md bg-yellow-600 text-white ">
              design
            </span>
          </div>
        </div>
        {/* Task Description */}
        <div>
          <h2
            ref={nameRef}
            role="input"
            suppressContentEditableWarning={true}
            contentEditable
            // placeholder="عنوان تسک خود را اینجا ویرایش کنید"
            className="font-semibold text-2xl text-base-content mb-3 min-h-16 flex items-center p-2 focus:outline-none focus:border"
          >
            {name}
          </h2>
          <textarea
            ref={descriptionRef}
            name="editTask"
            placeholder="تسک خود را اینجا ویرایش کنید"
            className="w-full outline-none resize-none text-black text-base p-4 min-h-[150px] focus:min-h-[150px] rounded-sm  border border-C1C1C1 focus:ring-0 focus:shadow-inner transition-all  scrollbar-thin scrollbar-thumb-gray-200 scrollbar-thumb-rounded-full scrollbar-track-white scrollbar-corner-transparent"
            defaultValue={description}
            onChange={() => {
              true;
            }}
          />
          <Button onClick={submitChangesHandler} className="max-w-fit mr-auto">
            ثبت تغییرات
          </Button>
        </div>

        {/* Attachments */}
        <div>
          {/* Add Attachments */}
          <form className="text-primary w-fit cursor-pointer">
            <label
              htmlFor="file"
              className="flex items-center gap-2 font-medium text-xs cursor-pointer"
            >
              <Icon iconName="SquarePlus" className="text-base" />
              <span className="pt-0.5">اضافه کردن پیوست</span>
            </label>
            <input hidden id="file" type="file" />
          </form>
        </div>
      </div>
    </div>
  );
};

export default TaskInfoBodyRight;
