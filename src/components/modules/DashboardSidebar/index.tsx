"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { Flex, Icon, Link, Button } from "@/components/modules/UI";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { fetchAllWorkSpaces, resetAllState } from "@/logic/store/store";
import { logOut } from "@/logic/store/slices/authSlice";
import { ProjectProps } from "@/logic/store/slices/workSpacesSlice";
// import ProfileButton from "../../ui/ProfileButton";
// import SearchInput from "../../ui/SearchInput";
// import NewSpace from "./NewSpace";
// import WorkSpaceList from "./WorkSpaceList";
// import SpaceMenu from "./SpaceMenu";

export type workSpacesType = {
  _id: string;
  name: string;
  user: string;
  projects: ProjectProps;
}[];

export default function DashboardSidebar() {
  const router = useRouter();

  const {
    message,
    isError,
    isLoading,
    isSuccess,
    workSpaces,
    selectedSpace,
    searchedWorkSpace,
  } = useAppSelector((state) => state.workSpaces);
  const { user } = useAppSelector((state) => state.auth);
  const dispatch = useAppDispatch();

  useEffect(() => {
    !isSuccess && workSpaces.length === 0 && dispatch(fetchAllWorkSpaces());
  }, [dispatch, selectedSpace, workSpaces.length, isSuccess, workSpaces]);

  const getSelectedWorkSpaces = workSpaces.filter((workSpace) => {
    return workSpace._id === selectedSpace;
  });

  const workSpacesToRender = getSelectedWorkSpaces.length
    ? getSelectedWorkSpaces
    : searchedWorkSpace
      ? searchedWorkSpace
      : isSuccess
        ? workSpaces
        : [];

  const clickProfile = () => {
    dispatch(resetAllState());
    dispatch(logOut());
    router.push("/login");
  };

  return (
    <Flex
      direction="col"
      justifyContent="between"
      className="w-1/5 h-screen py-10 pr-12 pl-4 border-l border-gray-400"
    >
      <div>
        <h1 className="text-2xl font-extrabold bg-clip-text text-transparent bg-gradient-to-r from-[#118C80] to-[#4AB7D8]">
          Phoenix Task Manager
        </h1>
        {/* <SpaceMenu workSpaces={workSpaces || []} /> */}
        <input
          type="text"
          placeholder="جستجو در میزِکارها"
          className="w-full my-3 p-2 bg-slate-100 text-xs font-IranYekan outline-none rounded-md"
        />
        <Button
          className="flex items-center gap-3"
          variant="outline"
          size="full"
        >
          <Icon iconName="SquarePlus" className="text-primary" />
          <span>ایجاد محیط کاری جدید</span>
        </Button>
      </div>
      <div>
        {/* <SearchInput placeHolder="جستجو کنید" extraClass="my-3" type="sideBar" /> */}
        {/* <NewSpace /> */}
        {/* {isLoading && <Icon iconName="Loading" />}
      {isError && <div className="m-auto text-FB0606">{`${message}`}</div>}
      {isSuccess && <WorkSpaceList workSpaces={workSpacesToRender} />} */}
        <Link
          className="flex items-center gap-2"
          weight="800"
          textSize="S"
          to="/profile/personal-info"
        >
          <Icon iconName="Profile" className="" />
          پروفایل
          <button className="flex items-center w-fit gap-2 ">
            <span
              className={`flex justify-center items-center rounded-full bg-yellow-300 dark:text-[#1E2124] ${className}`}
            >
              {user?.username.slice(0, 2)}
            </span>
            {user?.username}
          </button>
          {/* <ProfileButton userName={user?.username} className="w-9 h-9 p-2" /> */}
        </Link>
        <button
          className="w-fit mt-5 flex items-center gap-2 text-base font-IranYekan font-extrabold text-red-400"
          onClick={() => clickProfile()}
        >
          <Icon iconName="Logout" />
          خروج
        </button>
      </div>
    </Flex>
  );
}
