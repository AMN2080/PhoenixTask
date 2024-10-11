import { useEffect, useState } from "react";
import { createPortal } from "react-dom";
import Icon from "@/components/Icon";
import { Button } from "@/components/UI";
import Modal from "@/components/Modal/";
import NewWorkspace from "@/components/Modal/Medium/NewWorkspace";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { toast } from "react-toastify";
import {
  closeAllModals,
  fetchProjects,
  resetPostBoard,
  resetPostProject,
  resetPostWorkspace,
  resetProject,
  resetTask,
  setSelectedProjectSidebar,
  toggleMediumModal,
} from "@/logic/store/store";

const NewSpace = () => {
  const [workSpaceStep, setWorkSpaceStepe] = useState("ساختن محیط کاری جدید");

  const dispatch = useAppDispatch();
  const { medium } = useAppSelector((state) => state.modals);

  const {
    isErrorPost,
    isLoadingPost,
    isSuccessPost,
    messagePost,
    selectedWorkSpaceId,
  } = useAppSelector((state) => state.workSpaces);
  const {
    isErrorPost: isErrorProject,
    isLoadingPost: isLoadingPorject,
    isSuccessPost: isSuccessProject,
    messagePost: messageProject,
  } = useAppSelector((state) => state.projects);
  const {
    isErrorPost: isErrorBoard,
    isLoadingPost: isLoadingBoard,
    isSuccessPost: isSuccessBoard,
    messagePost: messageBoard,
  } = useAppSelector((state) => state.boards);

  const {
    isError: isErrorTask,
    isLoading: isLoadingTask,
    isSuccess: isSuccessTask,
    message: messageTask,
  } = useAppSelector((state) => state.tasks);

  useEffect(() => {
    // workSpace
    if (isErrorPost) {
      toast.dismiss();
      messagePost != "" && toast.error(`${messagePost} ❗`);
      dispatch(resetPostWorkspace());
    }
    if (isSuccessPost) {
      toast.dismiss();
      messagePost != "" && toast.success(`${messagePost}`, { rtl: true });
      dispatch(resetPostWorkspace());
      medium != "اشتراک محیط کاری" && dispatch(closeAllModals());
    }
    // project
    if (isErrorProject) {
      toast.dismiss();
      messageProject != "" && toast.error(`${messageProject} ❗`);
      dispatch(resetPostProject());
    }
    if (isSuccessProject) {
      messageProject === "پروژه حذف شد " &&
        dispatch(setSelectedProjectSidebar(""));
      if (messageProject !== "پروژه حذف شد " && messageProject != "") {
        dispatch(fetchProjects(selectedWorkSpaceId));
        dispatch(resetProject());
      }

      toast.dismiss();
      messageProject != "" && toast.success(`${messageProject}`, { rtl: true });
      dispatch(resetPostProject());
      medium !== "اشتراک تسک" && medium !== "shareModalHeader"
        ? dispatch(closeAllModals())
        : null;
    }

    // board
    if (isErrorBoard) {
      toast.dismiss();
      messageBoard != "" && toast.error(`${messageBoard} ❗`);
      dispatch(resetPostBoard());
    }
    if (isSuccessBoard) {
      toast.dismiss();
      messageBoard != "" && toast.success(`${messageBoard}`, { rtl: true });
      dispatch(resetPostBoard());
      dispatch(closeAllModals());
    }

    // task
    if (isErrorTask) {
      toast.dismiss();
      toast.error(`${messageTask} ❗`);
      dispatch(resetTask());
    }
    if (isSuccessTask && messageTask != "") {
      toast.dismiss();
      toast.success(`${messageTask}`, { rtl: true });
      dispatch(resetTask());
      dispatch(closeAllModals());
    }
  }, [
    dispatch,
    isErrorPost,
    isLoadingPost,
    isSuccessPost,
    messagePost,
    isErrorProject,
    isLoadingPorject,
    isSuccessProject,
    messageProject,
    isErrorBoard,
    isLoadingBoard,
    isSuccessBoard,
    messageBoard,
    messageTask,
    isSuccessTask,
    isLoadingTask,
    isErrorTask,
    medium,
    selectedWorkSpaceId,
  ]);

  return (
    <>
      <Button
        disabled={isLoadingPost}
        variant="outline"
        className="flex justify-evenly items-center w-full gap-1 py-3"
        onClick={() => dispatch(toggleMediumModal("workspace"))}
      >
        <Icon iconName="SquarePlus" />
        <span>ایجاد محیط کاری جدید</span>
      </Button>

      {medium === "workspace" &&
        createPortal(
          <Modal>
            <NewWorkspace
              workSpaceStep={workSpaceStep}
              setWorkSpaceStepe={setWorkSpaceStepe}
            />
          </Modal>,
          document.body,
        )}
    </>
  );
};

export default NewSpace;
