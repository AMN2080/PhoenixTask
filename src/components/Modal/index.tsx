import { useAppDispatch } from "@/logic/store/hook";
import { closeAllModals } from "@/logic/store/store";
import { Flex } from "@/components/UI"

type modalProps = {
  children: React.ReactNode;
  className?: string;
};

const Modal = ({ children, className }: modalProps) => {
  const dispatch = useAppDispatch();
  const handleOnModals = () => {
    dispatch(closeAllModals());
  };

  return (
    <Flex
      onClick={handleOnModals}
      justifyContent="center"
      alignItems="center"
      className={`modal opacity-100 visible z-50 pointer-events-auto select-none ${className}`}
    >
      <div onClick={(e) => e.stopPropagation()}>{children}</div>
    </Flex>
  );
};

export default Modal;
