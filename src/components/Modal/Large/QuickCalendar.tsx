import { useState } from "react";
import persian from "react-date-object/calendars/persian";
import persian_fa from "react-date-object/locales/persian_fa";
import { Calendar } from "react-multi-date-picker";
import "react-multi-date-picker/styles/colors/teal.css";
import "react-multi-date-picker/styles/backgrounds/bg-dark.css";
import "./customCalendar.css";
import { useAppSelector } from "@/logic/store/hook";
import Icon from "@/components/Icon";
import { Flex } from "@/components/UI";

type QuickCalendarProps = {
  handleCalendar: (modalState: boolean, value?: any) => void;
  submitChangesHandler?: (deadline: string) => void;
};

const QuickCalendar = ({
  handleCalendar,
  submitChangesHandler,
}: QuickCalendarProps) => {
  const [value, setValue] = useState(null);
  const [deadline, setDeadline] = useState({
    showDeadline: "",
    value: "",
  });
  const { theme } = useAppSelector((state) => state.user);
  // custom weekDays name
  const weekDays = [
    "شنبه",
    "یکشنبه",
    "دوشنبه",
    "سه شنبه",
    "چهارشنبه",
    "پنجشنبه",
    "جمعه",
  ];

  // handle deadLine value and show deadline
  const handleDeadline = (date: any) => {
    const deadlineShow = date?.format("YYYY/MM/DD");
    const valueDate = date?.format("YYYY-MM-DDTHH:mm:ss");
    // convert
    const englishNum =
      valueDate != undefined && persianToEnglishNumber(valueDate);
    setValue(date);
    setDeadline({ ...deadline, showDeadline: deadlineShow, value: englishNum });
  };

  // convert persian selected number to english
  const persianToEnglishNumber = (number: any) => {
    return number.replace(/[\u06F0-\u06F9]/g, function (digit: string) {
      return String.fromCharCode(digit.charCodeAt(0) - 1728);
    });
  };

  return (
    <>
      <div
        className="modal opacity-100 pointer-events-auto bg-transparent visible"
        id="my-modal-2"
      >
        <div className="modal-box opacity-100 p-0 max-w-4xl min-w-[896px] h-5/6 min-h-[608px] max-h-[608px] rounded-3xl shadow-[0_12px_32px_rgba(0,0,0,0.25)]">
          {/* calendar Header */}
          <Flex alignItems="center" justifyContent="around" className="w-full h-32 border-b-2 font-medium">
            <Flex alignItems="center" justifyContent="center" className="text-2xl font-medium text-neutral-content">
              <span className="ml-3">
                <Icon iconName="Calendar" />
              </span>
              ددلاین
              <span className="text-primary mr-3">{deadline.showDeadline}</span>
            </Flex>
          </Flex>

          {/* calendar Content */}
          <div className="w-full h-4/6">
            <Calendar
              onChange={handleDeadline}
              value={value}
              weekDays={weekDays}
              headerOrder={["MONTH_YEAR", "LEFT_BUTTON", "RIGHT_BUTTON"]}
              monthYearSeparator=" "
              className={theme === "dark" ? "bg-dark teal" : `teal`}
              multiple={false}
              calendar={persian}
              locale={persian_fa}
            />
          </div>

          {/* calendar Footer */}
          <Flex justifyContent="end" gap="S" className="w-full pl-8 mt-2">
            <div className="w-32 h-8">
              <label
                htmlFor="my-modal"
                onClick={() => handleCalendar(false, deadline.value)}
                className={`w-full h-10 p-2.5 text-sm font-bold leading-4 cursor-pointer flex justify-center items-center text-white rounded-md ${
                  submitChangesHandler &&
                  "bg-error"
                } bg-primary`}
              >
                بستن
              </label>
            </div>
            {submitChangesHandler && (
              <div className="w-32 h-8">
                <label
                  htmlFor="my-modal"
                  onClick={() => {
                    handleCalendar(false);
                    if (deadline && deadline.value)
                      submitChangesHandler(deadline.value);
                  }}
                  className="w-full h-10 p-2.5 text-sm font-bold leading-4 cursor-pointer flex justify-center items-center text-white rounded-md bg-primary"
                >
                  ثبت
                </label>
              </div>
            )}
          </Flex>
        </div>
      </div>
    </>
  );
};

export default QuickCalendar;
