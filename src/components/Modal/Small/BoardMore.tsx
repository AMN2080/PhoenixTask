import Icon from "@/components/Icon";
import { useAppDispatch } from "@/logic/store/hook";
import { toggleXSmallModal } from "@/logic/store/store";
import { MorePosition } from "@/components/modules/dashboard/Sidebar/ProjectList";
import { useState } from "react";
import ConfirmDelete from "@/components/templates/DashboardColumnView/ConfirmDelete";

type BoardMoreProps = {
  position: MorePosition;
  handleDeleteBoard: () => void;
  id: string;
  handleEditMood: (arg: string) => void;
};

const BoardMore = ({
  position,
  handleDeleteBoard,
  id,
  handleEditMood,
}: BoardMoreProps) => {
  const liStyle =
    "flex items-center cursor-pointer mt-3 text-sm text-neutral-content font-normal";
  const [confirm, setConfirm] = useState(false);
  const dispatch = useAppDispatch();

  return (
    <>
      <ul
        style={{ top: position.top, left: position.left }}
        className="absolute top-7 min-w-40 mt-1 left-2 rounded-lg p-3 z-50 bg-white shadow-lg"
      >
        {confirm ? (
          <li className={`${liStyle} !mt-0 p-2`}>
            <ConfirmDelete
              status="ستون"
              cancel={setConfirm}
              accept={handleDeleteBoard}
            />
          </li>
        ) : (
          <>
            <li
              className={liStyle}
              onClick={() => {
                handleEditMood(id);
                dispatch(toggleXSmallModal(""));
              }}
            >
              <span className="text-sm">
                <Icon iconName="Edit" />
              </span>
              <p className="mr-2">ویرایش نام ستون</p>
            </li>

            <li className={liStyle} onClick={() => setConfirm(true)}>
              <span className="text-sm">
                <Icon iconName="Remove" />
              </span>
              <p className="mr-2">حذف ستون</p>
            </li>
          </>
        )}
      </ul>
    </>
  );
};

export default BoardMore;
