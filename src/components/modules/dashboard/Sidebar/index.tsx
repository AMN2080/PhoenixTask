"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { Flex, Link, Button } from "@/components/UI";
import Icon from "@/components/Icon";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { fetchAllWorkSpaces, resetAllState } from "@/logic/store/store";
import { logOut } from "@/logic/store/slices/authSlice";
import { workSpacesType } from "@/logic/types/workSpaceType";
// import ProfileButton from "../../ui/ProfileButton";
// import SearchInput from "../../ui/SearchInput";
import NewWorkSpaceButton from "./NewWorkSpaceButton";
// import WorkSpaceList from "./WorkSpaceList";
// import SpaceMenu from "./SpaceMenu";

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
      className="w-1/6 h-screen py-10 pr-12 pl-4 border-l border-gray-400"
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
        <NewWorkSpaceButton />
      </div>
      {/* <SearchInput placeHolder="جستجو کنید" extraClass="my-3" type="sideBar" /> */}
      {/* {isLoading && <Icon iconName="Loading" />}
      {isError && <div className="m-auto text-FB0606">{`${message}`}</div>}
      {isSuccess && <WorkSpaceList workSpaces={workSpacesToRender} />} */}
      <div>
        <Link
          className="flex items-center gap-2"
          weight="800"
          textSize="S"
          to="/profile/personal-info"
        >
          <Icon
            iconName="Profile"
            className="border-2 border-primary overflow-hidden rounded-full w-9 h-9 p-2"
          />
          {user?.username}
        </Link>
        <Button
          asChild
          className="w-fit mt-5 flex items-center gap-2 text-base font-IranYekan font-extrabold text-red-400"
          onClick={() => clickProfile()}
        >
          <Icon iconName="Logout" />
          خروج
        </Button>
      </div>
    </Flex>
  );
}
