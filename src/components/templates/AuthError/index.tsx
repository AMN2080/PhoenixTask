import { FieldError } from "react-hook-form";
import Text from "@/components/modules/UI/Text";

interface ErrorMessageProps {
  error: FieldError | undefined;
}

export default function ErrorMessage({ error }: ErrorMessageProps) {
  if (error) {
    return (
      <Text
        className="mt-1 relative self-start z-20 shadow-[0_0_5px_#f02236] bg-red-500 before:-z-10 rounded-lg text-white p-2 before:content-[''] before:w-4 before:h-4 before:rotate-45 before:absolute before:-top-[20%] before:right-5 before:bg-red-500"
        textSize="S"
      >
        {error.message}
      </Text>
    );
  }
  return null;
}
