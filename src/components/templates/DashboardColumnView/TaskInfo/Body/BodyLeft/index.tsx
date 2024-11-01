import { useEffect, useRef, useState } from "react";
import Icon from "@/components/Icon";
// import { LuEdit } from "react-icons/lu";
import { commentType } from "@/logic/types/boardType";
import AddComment from "../../AddComment";
import getPersianDate from "@/logic/utils/date/getPersianDate";
import { useAppDispatch, useAppSelector } from "@/logic/store/hook";
import { deleteComment, updateComment } from "@/logic/store/store";
import { createPortal } from "react-dom";
import { resetComment } from "@/logic/store/slices/boardSlice";
import { toast } from "react-toastify";

type TaskInfoBodyLeftType = {
  comments: commentType[];
  taskId: string;
};

const TaskInfoBodyLeft = ({ comments, taskId }: TaskInfoBodyLeftType) => {
  const loggedInUser = JSON.parse(localStorage.getItem("user") as string).id;
  const [editingCommentId, setEditingCommentId] = useState("");
  const [deletingCommentId, setDeletingCommentId] = useState("");
  const [previousComment, setPreviousComment] = useState("");
  const [confirmationModaIsOpen, setConfirmationModaIsOpen] = useState(false);
  const commentRef = useRef<HTMLDivElement>(null);

  const handleEditClick = (id: string, commentText: string) => {
    setEditingCommentId(id);
    setPreviousComment(commentText);
  };

  const dispatch = useAppDispatch();
  const deleteCommentHandler = (commentId: string) => {
    dispatch(deleteComment(commentId));
    setDeletingCommentId("");
  };

  const editCommentHandler = () => {
    if (commentRef.current?.textContent) {
      dispatch(
        updateComment({
          text: commentRef.current.textContent as string,
          id: editingCommentId,
        }),
      );
      setEditingCommentId("");
    }
  };

  const handleCommentCancel = () => {
    setEditingCommentId("");
    if (commentRef.current?.textContent)
      commentRef.current.textContent = previousComment;
  };

  useEffect(() => {
    if (editingCommentId && commentRef.current) {
      // Set the focus to the end of the text
      const range = document.createRange();
      const selection = window.getSelection();
      range.selectNodeContents(commentRef.current);
      range.collapse(false);
      selection?.removeAllRanges();
      selection?.addRange(range);
      // Move the cursor to the end of the text
      commentRef.current.focus();
    }
  }, [editingCommentId]);

  const {
    addCommentIsError,
    addCommentIsLoading,
    addCommentIsSuccess,
    addCommentMessage,
    deleteCommentIsError,
    deleteCommentIsSuccess,
    deleteCommentIsLoading,
    deleteCommentMessage,
    editingCommentIsError,
    editingCommentIsLoading,
    editingCommentIsSuccess,
    editingCommentMessage,
  } = useAppSelector((state) => state.boards);

  useEffect(() => {
    // Handling Add comment states
    if (addCommentIsError) {
      toast.dismiss();
      toast.error(`${addCommentMessage} ❗`);
      dispatch(resetComment());
    }
    if (addCommentIsSuccess) {
      toast.dismiss();
      toast.success(`کامنت با موفقیت اضافه شد 🎉`, {
        autoClose: 2000,
        rtl: true,
      });
      dispatch(resetComment());
    }
    // Handling Delete comment states
    if (deleteCommentIsError) {
      toast.dismiss();
      toast.error(`${deleteCommentMessage} ❗`);
      dispatch(resetComment());
    }
    if (deleteCommentIsSuccess) {
      toast.dismiss();
      toast.success(`کامنت با موفقیت حذف شد `, {
        autoClose: 2000,
        rtl: true,
      });
      dispatch(resetComment());
    }
    // Handling Edit comment states
    if (editingCommentIsError) {
      toast.dismiss();
      toast.error(`${editingCommentMessage} ❗`);
      dispatch(resetComment());
    }
    if (editingCommentIsSuccess) {
      toast.dismiss();
      toast.success(`کامنت با موفقیت ویرایش شد `, {
        autoClose: 2000,
        rtl: true,
      });
      dispatch(resetComment());
    }
  }, [
    addCommentIsError,
    addCommentIsLoading,
    addCommentIsSuccess,
    addCommentMessage,
    deleteCommentIsError,
    deleteCommentIsSuccess,
    deleteCommentIsLoading,
    deleteCommentMessage,
    editingCommentIsError,
    editingCommentIsLoading,
    editingCommentIsSuccess,
    editingCommentMessage,
    dispatch,
  ]);

  return (
    <div className="w-1/2 box-border overflow-auto scrollbar-thin scrollbar-thumb-gray-200 scrollbar-thumb-rounded-full scrollbar-track-white mb-11">
      {/* TaskInfo Body Left Container */}
      <div className="mx-4 my-6 flex flex-col gap-5 ">
        {/* History of The Task */}
        <ul className="flex flex-col gap-4">
          {/* History Items */}
          <li className="flex items-center justify-between">
            {/* History Action */}
            <div className="flex items-center justify-start gap-1">
              {/* User */}
              <b className="text-primary text-base">شما</b>
              {/* Action */}
              <span className="font-normal text-base text-black">
                این تسک را ساختید
              </span>
            </div>
            {/* History Date */}
            <span className="text-ACAEB0 font-normal text-xs">1 ساعت پیش</span>
          </li>
          {/* Dummy Actions!  */}
          <li className="flex items-center justify-between">
            {/* History Action */}
            <div className="flex items-center justify-start gap-1">
              {/* User */}
              <b className="text-primary text-base">سعید</b>
              {/* Action */}
              <span className="font-normal text-base text-black">
                این تسک را از ToDo به Done برد
              </span>
            </div>
            {/* History Date */}
            <span className="text-ACAEB0 font-normal text-xs">1 ساعت پیش</span>
          </li>
        </ul>
        {/* Added Comments */}
        <ul className=" flex flex-col gap-2 ">
          {comments.length ? (
            comments.map((comment) => (
              <li key={comment.id} className="chat chat-start">
                <div className="chat-image avatar">
                  <div className="w-10 rounded-full ">
                    <span className="bg-red-300 w-full h-full flex items-center justify-center">
                      {comment.user.username.substring(0, 2)}
                    </span>
                  </div>
                </div>
                <div className="chat-header flex gap-2 items-center">
                  <span>{comment.user.username}</span>
                  <time className="text-xs opacity-50">
                    {getPersianDate(comment.createdAt)}
                  </time>
                </div>

                <div
                  suppressContentEditableWarning={true}
                  className={`group chat-bubble ${
                    !editingCommentId && "hover:pb-6"
                  } ${
                    editingCommentId === comment.id &&
                    "pb-8 hover:pb-8 min-w-full"
                  } relative min-w-[100px] transition-all delay-200 ${
                    comment.id === editingCommentId
                      ? "  rounded-md  before:border-b-0 before:border-C1C1C1"
                      : ""
                  }`}
                >
                  <div
                    ref={
                      comment.id === editingCommentId ? commentRef : undefined
                    }
                    className="focus:outline-none"
                    contentEditable={comment.id === editingCommentId}
                    suppressContentEditableWarning={true}
                  >
                    {comment.text}
                  </div>
                  {loggedInUser === comment.user.id && (
                    <div className="absolute flex bottom-1 left-1">
                      {comment.id !== editingCommentId ? (
                        <div
                          className={`opacity-0 flex gap-1 group-hover:opacity-100 transition-all ${
                            editingCommentId && "hidden"
                          }`}
                        >
                          <Icon
                            iconName="Remove"
                            onClick={() => {
                              setConfirmationModaIsOpen(true);
                              setDeletingCommentId(comment.id);
                            }}
                            className="cursor-pointer text-white hover:text-red-500 mr-2 text-base"
                          />
                          {/* <LuEdit
                            className="cursor-pointer text-white hover:text-red-500"
                            onClick={() =>
                              handleEditClick(comment.id, comment.text)
                            }
                            size="15"
                          /> */}
                        </div>
                      ) : (
                        <div className="absolute -bottom-[3px] -left-[3px] flex gap-1">
                          <button
                            contentEditable="false"
                            className="btn btn-xs bg-[#ff3333] border-none hover:bg-[#ff1a1a]"
                            onClick={() => {
                              handleCommentCancel();
                            }}
                          >
                            لغو
                          </button>
                          <button
                            contentEditable="false"
                            className="btn btn-xs bg-primary hover:bg-[#1d7f80] focus:outline-none text-white border-none"
                            onClick={() => editCommentHandler()}
                          >
                            ثبت
                          </button>
                        </div>
                      )}
                    </div>
                  )}
                </div>
              </li>
            ))
          ) : (
            <p className="h-20 grid place-content-center">
              اولین کامنتت رو بزار
            </p>
          )}
          {addCommentIsLoading && (
            <li className="mx-auto">
              <span className="loading loading-bars loading-lg text-primary"></span>
            </li>
          )}
        </ul>
        {/* Comment Input Component */}
        <AddComment taskId={taskId} />
      </div>
      {confirmationModaIsOpen &&
        createPortal(
          <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-30 z-40">
            <div className="alert alert-warning fixed top-1/2 left-[37%] z-50 w-[36%] h-16 bg-white">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="stroke-warning shrink-0 h-6 w-6"
                fill="none"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"
                />
              </svg>

              <span>آیا از حذف کامنت مطمئن هستید؟</span>
              <div>
                <button
                  onClick={() => setConfirmationModaIsOpen(false)}
                  className="btn btn-sm bg-[#ff3333]  border-none hover:bg-[#ff1a1a]"
                >
                  خیر
                </button>
                <button
                  onClick={() => {
                    deleteCommentHandler(deletingCommentId);
                    setConfirmationModaIsOpen(false);
                  }}
                  className="btn btn-sm bg-primary hover:bg-[#1d7f80] focus:outline-none text-white border-none"
                >
                  بله
                </button>
              </div>
            </div>
          </div>,
          document.body,
        )}
    </div>
  );
};

export default TaskInfoBodyLeft;
