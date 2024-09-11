import { Icon } from "@/components/modules/UI";
import { Task } from "@/logic/services/boards/boardSlice";
import getGregorianDate from "@/logic/shamsi-date/getGregorianDate";
import { getPersianDateWithOutTime } from "@/logic/shamsi-date/getPersianDate";

interface StatusProjects {
  tasks: Task[];
  color: string;
}

const CollapseTable = ({ tasks, color }: StatusProjects) => {
  const colors = JSON.parse(localStorage.getItem("Colors") as string);
  return (
    <>
      {!tasks[0] ? (
        <div className="m-auto">تسکی جهت نمایش وجود ندارد ☹️</div>
      ) : (
        <table className="w-full text-right">
          <thead>
            <tr className="text-base font-medium ">
              <th className=""></th>
              <th>اعضا</th>
              <th>ددلاین</th>
              <th>اولویت</th>
              <th>توضیحات</th>
            </tr>
          </thead>
          <tbody>
            {tasks.map(({ _id, name, description, taskAssigns, deadline }) => (
              <tr key={_id} className="text-sm">
                <th className="flex items-center mr-16 py-6 w-72">
                  <span className={`w-4 h-4 ${color} rounded-sm ml-2 `}></span>
                  {name}
                </th>
                <th>
                  <div dir="ltr" className="flex justify-end -space-x-3 w-20">
                    {taskAssigns.length ? (
                      <>
                        {[...taskAssigns].slice(0, 3).map((member, index) => (
                          <div className="w-9 h-9 " key={member._id}>
                            <div
                              className={`${colors[index]} w-full h-full rounded-full flex items-center justify-center pt-1 text-white border-2 dark:border-[#0F111A]`}
                            >
                              {member.username.substring(0, 2)}
                            </div>
                          </div>
                        ))}
                        {taskAssigns.length > 3 && (
                          <div className="w-9 h-9 ">
                            <div
                              className={`bg-323232 w-full h-full rounded-full flex items-center justify-center pt-1 text-white border-2 dark:border-[#0F111A]`}
                            >
                              +{taskAssigns.length - 3}
                            </div>
                          </div>
                        )}
                      </>
                    ) : (
                      "تعریف نشده"
                    )}
                  </div>
                </th>
                <td>
                  {deadline
                    ? getPersianDateWithOutTime(getGregorianDate(deadline))
                    : "تعریف نشده"}
                </td>
                <td>
                  <span className="text-FB0606">
                    <Icon iconName="Flag" />
                  </span>
                </td>
                <td>
                  <Icon iconName="Description" />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
};

export default CollapseTable;
