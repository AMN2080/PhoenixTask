import { useEffect, useRef, useState } from "react";
import { toast } from "react-toastify";
import {
  addMemberToProject,
  addWorkSpaceMember,
  fetchAddedMemberWorkspace,
  removeMemberThanProject,
  removeWorkSpaceMember,
  toggleMediumModal,
} from "@/logic/store/store";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { Flex, Button } from "@/components/UI";
import Icon from "@/components/Icon";

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
            {`به اشتراک گذاری ${ModalTitle}`}
          </div>

          <span></span>
        </Flex>

        {/* card content */}
        <div className="mt-11 w-full">
          <Flex direction="col" className="w-full relative">
            {/* Send invite Link  */}
            <Flex>
              <input
                type="text"
                placeholder="دعوت با نام کاربری"
                name="invite"
                id="invite"
                ref={inputInvite}
                className="w-4/5 h-10 p-3 bg-[#F0F1F3] rounded-tr-lg rounded-br-lg text-sm font-normal focus:outline-none border border-[#F7F9F9] border-l-0"
              />

              <div className="w-24">
                <Button
                  onClick={handleAddMember}
                  disabled={isLoadingPost || isLoadingProject}
                >
                  ارسال
                </Button>
              </div>
            </Flex>

            <Flex
              justifyContent="between"
              alignItems="center"
              className="w-full mt-7"
            >
              <Flex alignItems="center">
                <Icon iconName="Link" />
                <span className="mr-3 text-sm font-normal text-neutral-content">
                  لینک خصوصی
                </span>
              </Flex>

              <Flex
                justifyContent="center"
                alignItems="center"
                className="w-20 h-6 px-3 py-1 text-xs font-normal text-neutral-content rounded-md border border-[#E9EBF0] cursor-pointer"
              >
                کپی لینک
              </Flex>
            </Flex>
            {isLoadingPost || isLoadingProject ? (
              <Icon iconName="Loading" />
            ) : (
              <Flex direction="col" className="mt-7">
                {members.length > 0 && (
                  <h4 className="text-sm font-normal text-[#7D828C]">
                    اشتراک گذاشته شده با
                  </h4>
                )}
                <ul className="max-h-40 overflow-auto scrollbar-thin scrollbar-thumb-gray-200 scrollbar-thumb-rounded-full scrollbar-track-white">
                  {members &&
                    members.map((item) => (
                      <li key={item.user.id} className="w-full mt-5">
                        {confirm === item.user.id ? (
                          <Flex
                            alignItems="center"
                            justifyContent="between"
                            className="border p-2 rounded-md text-base"
                          >
                            <p className="text-neutral-content">
                              از حذف کاربر مطمئن هستید؟
                            </p>
                            <div>
                              <button
                                className="focus:outline-none ml-6"
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
                          </Flex>
                        ) : (
                          <Flex justifyContent="between" alignItems="center">
                            <Flex alignItems="center">
                              <Flex
                                justifyContent="center"
                                alignItems="center"
                                className="w-8 h-8 text-sm text-white bg-error-content rounded-full"
                              >
                                {item.user.username.substring(0, 2)}
                              </Flex>
                              <span className="w-fit mr-2 px-2 py-1 rounded-md flex items-center justify-center font-normal truncate">
                                {item.user.email}
                              </span>
                            </Flex>

                            <Flex
                              justifyContent="center"
                              alignItems="center"
                              className="relative w-26 rounded-md py-1 px-2 text-sm font-normal border border-[#E9EBF0] cursor-pointer hover:text-error hover:border-error transition-all"
                              onClick={() => {
                                setConfirm(item.user.id);
                              }}
                            >
                              <span className="ml-4">حذف ممبر</span>
                              <Icon iconName="Remove" />
                            </Flex>
                          </Flex>
                        )}
                      </li>
                    ))}
                </ul>
              </Flex>
            )}
          </Flex>
        </div>
      </div>
    </div>
  );
};
export default ShareModal;
