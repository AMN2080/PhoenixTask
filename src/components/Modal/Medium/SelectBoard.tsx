import { useEffect, useRef, useState } from "react";
import { useAppDispatch } from "@/logic/store/hook";
import { toggleMediumModal } from "@/logic/store/store";
import { Flex, Button } from "@/components/UI";
import Icon from "@/components/Icon";

type dataList = {
  id: string;
  name: string;
};
type SelectBoardProps = {
  data: dataList[];
  selectedHandle: (id: string) => void;
  status: string;
};

const SelectBoard = ({ data, selectedHandle, status }: SelectBoardProps) => {
  const [boardId, setBoardId] = useState("");
  const selectRef = useRef<any | null>(null);

  const dispatch = useAppDispatch();
  const handleSelectValue = (event: React.ChangeEvent<HTMLElement>) => {
    const element = event.target as HTMLInputElement;
    setBoardId(element.value);
  };
  useEffect(() => {
    true;
  }, [status]);

  return (
    <div className="modal-box w-[400px]">
      {data.length > 0 ? (
        <div className="p-5 rounded-lg">
          {/* card header */}
          <Flex justifyContent="between" alignItems="center">
            <label
              htmlFor="my-modal-3"
              className="text-neutral-content cursor-pointer"
              onClick={() => dispatch(toggleMediumModal(""))}
            >
              <Icon
                iconName="Close"
                className="text-3xl hover:rotate-90 cursor-pointer"
              />
            </label>

            <div className="font-semibold text-2xl text-neutral-content">
              انتخاب {status}
            </div>

            <span></span>
          </Flex>
          {/* card content */}

          <div className="mt-11 w-full">
            {/* selectedList */}
            <Flex justifyContent="center" alignItems="center">
              <select
                dir="rtl"
                onChange={handleSelectValue}
                className="select select-accent w-full text-center"
                id="sel"
                defaultValue={boardId}
                ref={selectRef}
              >
                <option disabled value={boardId}>
                  {status} مورد نظر را انتخاب کنید
                </option>
                {data &&
                  data.map((item) => {
                    return (
                      <option key={item.id} value={item.id}>
                        {item.name}
                      </option>
                    );
                  })}
              </select>
            </Flex>

            {/* button */}
            <div className="mt-16">
              <Button
                onClick={() => {
                  const selectedValue = selectRef.current?.value;
                  selectedValue && selectedHandle(selectedValue);
                }}
              >
                ادامه
              </Button>
            </div>
          </div>
        </div>
      ) : (
        <>
          <Flex justifyContent="between" alignItems="center">
            <label
              htmlFor="my-modal-3"
              className="text-neutral-content cursor-pointer"
              onClick={() => dispatch(toggleMediumModal(""))}
            >
              <Icon
                iconName="Close"
                className="text-3xl hover:rotate-90 cursor-pointer"
              />
            </label>

            <div className="font-semibold text-2xl text-neutral-content"></div>

            <span></span>
          </Flex>
          <div className="font-semibold text-xl text-neutral-content text-center">
            {status === "پروژه"
              ? `${status} ای وجود نداره ، یدونه بساز`
              : `${status}ی وجود نداره ، یدونه بساز`}
          </div>
        </>
      )}
    </div>
  );
};

export default SelectBoard;
