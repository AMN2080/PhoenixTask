import { toast } from "react-toastify";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { createProject, toggleMediumModal } from "@/logic/store/store";
import Icon from "@/components/Icon";
import { Flex, Button, Input } from "@/components/UI";

type projectProps = {
  id?: string;
};

const NewProject = ({ id }: projectProps) => {
  const dispatch = useAppDispatch();
  const { isLoadingPost } = useAppSelector((state) => state.projects);

  const handleNewProject = () => {
    const name = document.querySelector<HTMLInputElement>("#newProject")?.value;
    const formData: (string | undefined | undefined)[] = [name, id];
    if (name?.trim()) {
      dispatch(createProject(formData));
    } else {
      toast.warning("نام پروژه را وارد کنید", { rtl: true });
    }
  };

  return (
    <div className="modal-box w-3/4 max-w-lgl min-w-[500px]">
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
            ساختن پروژه جدید
          </div>

          <span></span>
        </Flex>

        {/* card content */}
        {isLoadingPost ? (
          <Icon iconName="Loading" />
        ) : (
          <div className="mt-11 w-full">
            <Input connectorId="newProject" label="نام پروژه" />

            {/* Button  */}
            <div className="mt-16">
              <Button disabled={isLoadingPost} onClick={handleNewProject}>
                ساختن پروژه جدید
              </Button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default NewProject;
