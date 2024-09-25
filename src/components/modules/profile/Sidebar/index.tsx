import Icon from "@/components/Icon";
import { Button, Link, Heading, Flex } from "@/components/UI";
import { useRouter } from "next/navigation";

function ProfileSidebar() {
  const router = useRouter();
  return (
    <section className="w-1/6 h-screen pt-10 border-l-[0.5px] border-[#AAAAAA]">
      <Flex direction="col" className="w-[80%] mr-12">
        <Heading
          as="h1"
          size="XS"
          className="bg-clip-text text-transparent bg-gradient-to-r from-primary to-secondary"
        >
          Phoenix Task Manager
        </Heading>
        <div className="self-start mt-20">
          <Button
            onClick={() => router.push("/list")}
            className="text-l px-2 py rounded-lg"
          >
            بازگشت
          </Button>
        </div>
        {/* NavLinks */}
        <ul className="flex flex-col gap-8 mt-11">
          <li>
            <Link
              className="flex gap-3 cursor-pointer px-[10px] py-1 rounded hover:bg-[#c5ffff] transition-all"
              to={"/profile/personal-info"}
            >
              <Icon iconName="PersonalINfo" className="text-neutral-content" />
              <span className="font-medium text-xl text-neutral-content">
                اطلاعات فردی
              </span>
            </Link>
          </li>
          <li>
            <Link
              to={"/profile/account-info"}
              className="flex gap-3 cursor-pointer px-[10px] py-1 rounded hover:bg-[#c5ffff] transition-all dark:hover:bg-[#2c3640]"
            >
              <Icon iconName="AccountInfo" className="text-neutral-content" />
              <span className="font-medium text-xl text-neutral-content">
                اطلاعات حساب
              </span>
            </Link>
          </li>
          <li>
            <Link
              to={"/profile/situation"}
              className="flex gap-3 cursor-pointer px-[10px] py-1 rounded hover:bg-[#c5ffff] transition-all"
            >
              <Icon iconName="Setting" className="text-neutral-content" />
              <span className="font-medium text-xl text-neutral-content">
                تنظیمات
              </span>
            </Link>
          </li>
        </ul>
      </Flex>
    </section>
  );
}

export default ProfileSidebar;
