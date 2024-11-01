import { useState } from "react";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import {
  setSelectedSpace,
  setSelectedWorkSpaceHeader,
  setSelectedWorkSpaceId,
} from "@/logic/store/slices/workSpacesSlice";
import { fetchProjects } from "@/logic/store/store";
import { workSpacesType } from "@/logic/types/workSpaceType";

type WorkSpaceMenuProps = {
  workSpaces: workSpacesType;
};

const WorkSpaceMenu = ({ workSpaces }: WorkSpaceMenuProps) => {
  const [selectedValue, setSelectedValue] = useState<string>("");
  const dispatch = useAppDispatch();
  const { workSpaces: projectState } = useAppSelector(
    (state) => state.projects,
  );

  const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedId = e.target.value;
    const selectedName = e.target.selectedOptions[0].label;

    setSelectedValue(selectedId);
    if (selectedId) {
      dispatch(setSelectedSpace(selectedId));
      if (selectedName !== "محیط کاری‌ها")
        dispatch(setSelectedWorkSpaceHeader(selectedName));

      const workSpaceIndex = projectState.findIndex((projects) => {
        return projects.workSpaceId === selectedId;
      });
      if (selectedId != "محیط کاری‌ها") {
        if (workSpaceIndex < 0) {
          dispatch(fetchProjects(selectedId));
          dispatch(setSelectedWorkSpaceId(selectedId));
        } else {
          dispatch(setSelectedWorkSpaceId(selectedId));
        }
      }
    }
  };

  return (
    <select
      value={selectedValue} // Use the selected value state variable
      onChange={handleSelectChange}
      className="p-2 bg-white outline-none focus:ring-1 focus:ring-primary blur:ring-none rounded-md mt-7 w-full font-semibold"
    >
      <option className="text-323232 font-semibold">محیط کاری‌ها</option>
      {workSpaces.map(({ id, name }) => (
        <option
          className="font-semibold hover:text-white"
          key={id}
          value={id} // Use the workspace ID as the value of the option
        >
          {name}
        </option>
      ))}
    </select>
  );
};

export default WorkSpaceMenu;
