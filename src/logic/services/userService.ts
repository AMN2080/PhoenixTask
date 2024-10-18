import AXIOS from "@/logic/utils/AXIOS";
import { personalInfoType } from "@/logic/schemas/personalInfo";
import store from "@/logic/store/store";
import { updateUser } from "@/logic/store/slices/authSlice";

const API_URL = "/api/users/";

// updateUserById
const updateUserById = async (userData: personalInfoType) => {
  const id = JSON.parse(localStorage.getItem("user") as string)?.id;
  const response = await AXIOS.put(API_URL + id, userData);
  if (response.data) {
    localStorage.setItem("user", JSON.stringify(response.data));
    store.dispatch(updateUser(response.data));
  }
  return response.data;
};

const fetchAddedMemberWorkspace = async (memberId: string | undefined) => {
  const response = await AXIOS.get(API_URL + memberId);
  return response.data;
};

const authServie = {
  updateUserById,
  fetchAddedMemberWorkspace,
};
export default authServie;
