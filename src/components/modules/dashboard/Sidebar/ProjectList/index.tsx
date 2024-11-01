import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { deleteProject, toggleSmallModal } from "@/logic/store/store";

import {
  resetBoards,
  setSelectedProjectId,
} from "@/logic/store/slices/boardSlice";
import {
  editProjectName,
  resetPostProject,
  setSelectedProject,
  setSelectedProjectSidebar,
} from "@/logic/store/slices/projectSlice";
import { fetchBoards } from "@/logic/store/store";
import { createPortal } from "react-dom";
import SideMore from "@/components/Modal/Small/SideMore";
import Modal from "@/components/Modal";
import { Button } from "@/components/UI";
// import Button from "../../ui/Button";
import Icon from "@/components/Icon";

export type Projects = {
  projects: { id: string; name: string; boards: [] }[];
};

export type MorePosition = {
  top: number | string | undefined;
  left: number | string | undefined;
  clientX?: number | undefined;
  clientY?: number | undefined;
};
function ProjectList({ projects }: Projects) {
  const dispatch = useAppDispatch();
  const { small } = useAppSelector((state) => state.modals);
  const [projectId, setProjectId] = useState("");
  const { projects: projectState } = useAppSelector((state) => state.boards);
  const { selectedProjectSidebar } = useAppSelector((state) => state.projects);
  const [morePosition, setMorePosition] = useState<MorePosition>({
    top: 0,
    left: 0,
  });
  const [editMood, setEditMood] = useState("");
  const handleEditMood = (toggle: string | undefined) => {
    toggle && setEditMood(toggle);
  };

  const handleItemClick = (e?: React.MouseEvent<HTMLElement, MouseEvent>) => {
    // Get the client's screen dimensions
    const { clientX, clientY } = e || { clientX: 0, clientY: 0 };

    // Calculate the new top position based on the client's Y-coordinate and window height
    const windowHeight = window.innerHeight;
    const top = `${Math.min(clientY, windowHeight - 220)}px`;

    // Calculate the new left position based on the client's X-coordinate and window width
    const windowWidth = window.innerWidth;
    const left = `${Math.min(clientX, windowWidth - 220)}px`;

    // Set the new position in the state
    setMorePosition({ ...morePosition, top, left, clientX, clientY });
  };

  useEffect(() => {
    const handleWindowResize = () => {
      const { clientX, clientY } = morePosition;

      // Calculate the new top position based on the updated window height
      const windowHeight = window.innerHeight;
      const top = clientY && `${Math.min(clientY, windowHeight - 220)}px`;

      // Calculate the new left position based on the updated window width
      const windowWidth = window.innerWidth;
      const left = clientX && `${Math.min(clientX, windowWidth - 220)}px`;

      setMorePosition({ ...morePosition, top, left });
    };

    // Add the resize event listener
    window.addEventListener("resize", handleWindowResize);

    // Clean up the event listener on component unmount
    return () => {
      window.removeEventListener("resize", handleWindowResize);
    };
  }, [morePosition]);

  // handle delete project
  const handleDeleteProject = () => {
    projectId && dispatch(deleteProject(projectId));
    handleItemClick();
    dispatch(setSelectedProjectId(""));
    dispatch(setSelectedProject(""));
    dispatch(resetBoards());
  };

  const handleEdit = (id: string) => {
    const val = document.querySelector<HTMLInputElement>("#edit")?.value;
    if (val?.trim()) {
      const data = [id, val];
      dispatch(setSelectedProject(val.trim()));
      dispatch(editProjectName(data));
      dispatch(resetPostProject());
      setEditMood("");
    }
  };
  return (
    <>
      {projects.map(({ id, name }) => (
        <div key={id}>
          {editMood === id ? (
            <div className="flex px-1 py-3 w-full">
              <input
                type="text"
                id="edit"
                placeholder="نام جدید"
                autoComplete="off"
                className="w-3/4 font-medium  border border-[#AAAAAA] h-10 rounded-tr-md rounded-br-md px-3 py-2 focus:outline-none placeholder:text-sm"
              />
              <Button
                asChild
                onClick={() => setEditMood("")}
                className="!w-1/4 text-xs rounded-none bg-error placeholder:text-xs focus:!ring-0"
              >
                لغو
              </Button>
              <Button
                asChild
                onClick={() => handleEdit(id)}
                className="!w-1/4 text-xs rounded-tr-none rounded-br-none focus:!ring-0"
              >
                ویرایش
              </Button>
            </div>
          ) : (
            <div
              className={`pb-3 font-medium flex justify-between items-center cursor-pointer group/content ${
                selectedProjectSidebar === name
                  ? "text-[#118c80] transition-all"
                  : ""
              }`}
              key={id}
              onClick={() => {
                const projectIndex = projectState.findIndex((project) => {
                  return project.projectId === id;
                });
                if (projectIndex < 0) dispatch(fetchBoards(id));
                dispatch(setSelectedProjectSidebar(name));
                dispatch(setSelectedProjectId(id));
                dispatch(setSelectedProject(name));
              }}
            >
              {name}
              <span
                className=" left-2 cursor-pointer hidden group-hover/content:block z-10"
                onClick={(e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
                  setProjectId(id);
                  dispatch(toggleSmallModal(id));
                  handleItemClick(e);
                }}
              >
                <Icon iconName="More" />
              </span>
            </div>
          )}
        </div>
      ))}

      {small === projectId &&
        small != "" &&
        createPortal(
          <Modal className="!bg-transparent">
            <SideMore
              sideMoreState="تسک"
              morePosition={morePosition}
              handleDelete={handleDeleteProject}
              id={projectId}
              handleEditMood={handleEditMood}
            />
          </Modal>,
          document.body,
        )}
    </>
  );
}

export default ProjectList;
