import { IoClose as CloseIcon } from "react-icons/io5";
import Button from "@/components/modules/ui/Button";
import Flex from "@/components/modules/ui/Flex";
import Heading from "@/components/modules/ui/Heading";
import Text from "@/components/modules/ui/Text";

interface RulesModalProps {
  visible?: boolean;
  onClose: () => void;
}

export default function RulesModal({ visible, onClose }: RulesModalProps) {
  return (
    <div
      className={`${
        !visible && "hidden"
      } fixed h-screen w-screen z-50 inset-0 bg-[#17191B]/60 backdrop-blur-sm flex justify-center items-center`}
    >
      <div className="relative bg-white max-w-[800px] w-full rounded-[20px] pt-6 pr-6 pb-8 pl-6 shadow-[0_30px_60px_-30px_rgba(0,0,0,0.3)]">
        <Button
          onClick={onClose}
          asChild
          className="text-2xl hover:text-red-600 hover:rotate-90 transition-all flex-none"
        >
          <CloseIcon />
        </Button>
        <Heading className="mb-8" as="h2" size="L" align="center">
          قوانین و مقررات
        </Heading>
        <Flex>
          <Text size="M"> ادب رو رعایت کنین دوستان </Text>
        </Flex>
      </div>
    </div>
  );
}
