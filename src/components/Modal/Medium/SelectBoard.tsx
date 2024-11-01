import { useEffect, useRef, useState } from "react";
import Button from "../../UI/Button";
import CloseIcon from "../../UI/Close";
import { useAppDispatch } from "../../../services/app/hook";
import { toggleMediumModal } from "../../../services/app/store";

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
        <div className="p-5  rounded-lg">
          {/* card header */}
          <div className="w-full flex justify-between items-center">
            <label
              htmlFor="my-modal-3"
              className="text-323232 cursor-pointer"
              onClick={() => dispatch(toggleMediumModal(""))}
            >
              <CloseIcon />
            </label>

            <div className="font-semibold text-2xl text-black">
              انتخاب {status}
            </div>

            <span></span>
          </div>
          {/* card content */}

          <div className="mt-11 w-full ">
            {/* selectedList */}
            <div className="flex items-center justify-center">
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
            </div>

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
          <div className="w-full flex justify-between items-center">
            <label
              htmlFor="my-modal-3"
              className="text-323232 cursor-pointer"
              onClick={() => dispatch(toggleMediumModal(""))}
            >
              <CloseIcon />
            </label>

            <div className="font-semibold text-2xl text-black"></div>

            <span></span>
          </div>
          <div className="font-semibold text-xl text-black text-center">
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
