import Button from "../../UI/Button";
import { FiLink } from "react-icons/fi";
import { useEffect, useRef, useState } from "react";
import CloseIcon from "../../UI/Close";
import { toast } from "react-toastify";
import { useAppDispatch, useAppSelector } from "../../../services/app/hook";
import {
  addMemberToProject,
  addWorkSpaceMember,
  fetchAddedMemberWorkspace,
  removeMemberThanProject,
  removeWorkSpaceMember,
  toggleMediumModal,
} from "../../../services/app/store";
import { BsTrash } from "react-icons/bs";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

type Members = {
  user: {
    id: string;
    email: string;
    username: string;
  };
};
type ShareModalProps = {
  ModalTitle: string;
  id?: string;
};

const ShareModal = ({ ModalTitle, id }: ShareModalProps) => {
  const [members, setMembers] = useState<Members[]>([]);
  const [confirm, setConfirm] = useState("");
  const inputInvite = useRef<HTMLInputElement>(null);
  const dispatch = useAppDispatch();

  const {
    workSpaces: workMembers,
    isSuccessPost,
    isLoadingPost,
    addedMemberUserName: addedMemberWorkspace,
  } = useAppSelector((state) => state.workSpaces);
  const {
    isSuccessPost: isSuccessProject,
    isLoadingPost: isLoadingProject,
    workSpaces,
  } = useAppSelector((state) => state.projects);

  useEffect(() => {
    if (inputInvite.current?.value && isSuccessPost) {
      dispatch(fetchAddedMemberWorkspace(addedMemberWorkspace));
      inputInvite.current.value = "";
    }

    handleMembers();
  }, [dispatch, workMembers, isSuccessPost, isSuccessProject, workSpaces]);

  // check has member
  const checkHasMember = (memberName: string) => {
    if (ModalTitle === "محیط کاری") {
      const workspaceIndex = workMembers.findIndex(
        (workspace) => workspace.id === id,
      );
      const hasMember = workMembers[workspaceIndex].members.some(
        (member) => member.user.username === memberName,
      );
      return hasMember;
    }
    if (ModalTitle === "پروژه") {
      const project = workSpaces.map((workSpace) =>
        workSpace.projects.find((project) => project.id === id),
      );
      project.some((project) => project?.members);
      const hasMember = project[0]?.members.some(
        (member) => member.user.username === memberName,
      );
      return hasMember;
    }
  };

  // handle and setMembers for map
  const handleMembers = () => {
    if (ModalTitle === "محیط کاری") {
      const filter = workMembers.filter((item) => item.id === id);
      if (filter.length > 0) {
        const membersArray: Members[] = (filter[0] as any).members;
        setMembers(membersArray);
      }
    }

    if (ModalTitle === "پروژه") {
      const projects = workSpaces.map((workSpace) => workSpace.projects);
      const selectedProject: any = [];
      projects.forEach((project) => {
        project.forEach(
          (item) => item.id === id && selectedProject.push(project),
        );
      });

      if (selectedProject.length > 0) {
        const projectMembers = selectedProject[0].find(
          (project: { id: string | undefined }) => project.id === id,
        );

        setMembers(projectMembers.members);
      }
    }
  };

  // Add member with called dispatch redux toolkit
  const handleAddMember = () => {
    const inviteValue = inputInvite.current?.value;
    !inputInvite.current?.value.trim() &&
      toast.warning("اسم ممبر یادت نره !", { rtl: true });
    if (ModalTitle === "محیط کاری" && inviteValue?.trim()) {
      const workspaceIds: (string | undefined)[] = [id, inviteValue];
      checkHasMember(inviteValue)
        ? toast.error(`کاربر ${inviteValue} از قبل اضافه شده !`, { rtl: true })
        : dispatch(addWorkSpaceMember(workspaceIds));
    }

    if (ModalTitle === "پروژه" && inviteValue?.trim()) {
      const projectsIds: (string | undefined)[] = [id, inviteValue];

      checkHasMember(inviteValue)
        ? toast.error(`کاربر ${inviteValue} از قبل اضافه شده !`, { rtl: true })
        : dispatch(addMemberToProject(projectsIds));
    }
  };

  // Remove member with called dispatch redux toolkit
  const handleRemoveMember = (selectedMemberId: string) => {
    if (ModalTitle === "محیط کاری") {
      const workspaceIds = [id, selectedMemberId];
      inputInvite.current?.value && (inputInvite.current.value = "");
      dispatch(removeWorkSpaceMember(workspaceIds));
    }
    if (ModalTitle === "پروژه") {
      const projectsIds: (string | undefined)[] = [id, selectedMemberId];
      inputInvite.current?.value && (inputInvite.current.value = "");
      dispatch(removeMemberThanProject(projectsIds));
    }
  };

  return (
    <div className="modal-box overflow-visible w-3/4 z-50 min-w-[500px]">
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
            {`به اشتراک گذاری ${ModalTitle}`}
          </div>

          <span></span>
        </div>

        {/* card content */}
        <div className="mt-11 w-full">
          <div className="w-full flex flex-col relative">
            {/* Send invite Link  */}
            <div className="flex">
              <input
                type="text"
                placeholder="دعوت با نام کاربری"
                name="invite"
                id="invite"
                ref={inputInvite}
                className="w-4/5 h-10 p-3 bg-F0F1F3 rounded-tr-lg rounded-br-lg text-sm font-normal focus:outline-none border border-[#F7F9F9] border-l-0"
              />

              <div className="w-24">
                <Button
                  onClick={handleAddMember}
                  disabled={isLoadingPost || isLoadingProject}
                >
                  ارسال
                </Button>
              </div>
            </div>

            <div className="w-full mt-7 flex justify-between items-center ">
              <div className="flex items-center">
                <FiLink />
                <span className="mr-3 text-sm font-normal text-neutral-content">
                  لینک خصوصی
                </span>
              </div>

              <div className="w-20 h-6 px-3 py-1 text-xs flex items-center justify-center font-normal text-neutral-content rounded-md border border-[#E9EBF0] cursor-pointer">
                کپی لینک
              </div>
            </div>
            {isLoadingPost || isLoadingProject ? (
              <AiOutlineLoading3Quarters
                size="2.8rem"
                className="m-auto animate-spin text-primary"
              />
            ) : (
              <div className="mt-7 flex flex-col">
                {members.length > 0 && (
                  <h4 className="text-sm font-normal text-[#7D828C]">
                    اشتراک گذاشته شده با
                  </h4>
                )}
                <ul className="max-h-40 overflow-auto scrollbar-thin scrollbar-thumb-gray-200 scrollbar-thumb-rounded-full scrollbar-track-white">
                  {members &&
                    members.map((item) => (
                      <li
                        key={item.user.id}
                        className="w-full mt-5"
                      >
                        {confirm === item.user.id ? (
                          <div className="flex items-center justify-between  border p-2 rounded-md text-base">
                            <p className="text-black">
                              از حذف کاربر مطمئن هستید؟
                            </p>
                            <div>
                              <button
                                className="focus:outline-none ml-6 "
                                onClick={(e) => {
                                  e.stopPropagation();
                                  setConfirm("");
                                }}
                              >
                                خیر
                              </button>
                              <button
                                className="text-primary mr-2 focus:outline-none"
                                onClick={() => handleRemoveMember(item.user.id)}
                              >
                                بله
                              </button>
                            </div>
                          </div>
                        ) : (
                          <div className="flex items-center justify-between">
                            <div className="flex items-center">
                              <div className="w-8 h-8 text-sm flex justify-center items-center text-white bg-F27474 rounded-full">
                                {item.user.username.substring(0, 2)}
                              </div>
                              <span className="w-fit mr-2 px-2 py-1 rounded-md flex items-center justify-center font-normal truncate">
                                {item.user.email}
                              </span>
                            </div>

                            <div
                              className="relative w-26 rounded-md py-1 px-2 text-sm flex items-center justify-center font-normal border border-[#E9EBF0] cursor-pointer hover:text-red-500 hover:border-red-500 transition-all"
                              onClick={() => {
                                setConfirm(item.user.id);
                              }}
                            >
                              <span className="ml-4 ">حذف ممبر</span>
                              <BsTrash />
                            </div>
                          </div>
                        )}
                      </li>
                    ))}
                </ul>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
export default ShareModal;
