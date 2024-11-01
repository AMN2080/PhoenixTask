import { useAppDispatch } from "@/logic/store/hook";
import { toggleMediumModal } from "@/logic/store/store";
import Icon from "@/components/Icon";
import { Flex, Button } from "@/components/UI";

type AddTaskOnCalendar = {
  clickDate: string;
};
const AddTaskOnCalendar = ({ clickDate }: AddTaskOnCalendar) => {
  const dispatch = useAppDispatch();
  return (
    <div className="modal-box w-2/3 max-w-1xl min-w-[550px] py-5 px-12 overflow-visible">
      {/* header */}
      <Flex alignItems="center">
        <label
          htmlFor="my-modal-3"
          className="cursor-pointer text-black"
          onClick={() => dispatch(toggleMediumModal(""))}
        >
          <Icon
            iconName="Close"
            className="text-3xl hover:rotate-90 cursor-pointer"
          />
        </label>
        <input
          type="text"
          id="taskTitle"
          name="taskTitle"
          autoComplete="off"
          placeholder="نام تسک را وارد کنید"
          className="mr-3 text-2xl text-black font-medium focus:outline-none"
        />
      </Flex>

      {/* content */}
      <Flex
        alignItems="center"
        justifyContent="between"
        className="relativen w-full mt-10"
      >
        <Flex className="text-neutral text-2xl">
          <span className="cursor-pointer border-none">
            <Icon iconName="Flag" />
          </span>
          <span className="mr-5 text-primary text-xl font-medium">
            {clickDate}
          </span>
        </Flex>

        <div className="w-32">
          <Button>ساختن تسک</Button>
        </div>
      </Flex>
    </div>
  );
};

export default AddTaskOnCalendar;
