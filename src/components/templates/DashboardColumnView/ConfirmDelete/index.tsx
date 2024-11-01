type ConfirmDeleteProps = {
  cancel: (arg: boolean) => void;
  accept: any;
  status: string;
};

const ConfirmDelete = ({ accept, cancel, status }: ConfirmDeleteProps) => {
  return (
    <div className="flex items-center justify-around ">
      <p className="text-sm px-1">
        از حذف {status} مطمئنی ؟
      </p>
      <button
        className="focus:outline-none  mr-3 text-sm"
        onClick={(e) => {
          e.stopPropagation();
          cancel(false);
        }}
      >
        لغو
      </button>
      <button
        className="text-primary mr-3 focus:outline-none text-sm"
        onClick={accept}
      >
        تایید
      </button>
    </div>
  );
};

export default ConfirmDelete;
