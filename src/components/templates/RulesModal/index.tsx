import { Button, Flex, Heading, Text } from "@/components/UI";
import Icon from "@/components/Icon";

interface RulesModalProps {
  visible?: boolean;
  onClose: () => void;
}

export default function RulesModal({ visible, onClose }: RulesModalProps) {
  return (
    <Flex
      justifyContent="center"
      alignItems="center"
      className={`${!visible && "hidden"} fixed h-screen w-screen z-50 inset-0 bg-[#17191B]/60 backdrop-blur-sm`}
    >
      <div className="relative bg-white max-w-[800px] w-full rounded-[20px] pt-6 pr-6 pb-8 pl-6 shadow-[0_30px_60px_-30px_rgba(0,0,0,0.3)]">
        <Button
          onClick={onClose}
          asChild
          className="text-2xl hover:text-red-600 hover:rotate-90 transition-all flex-none"
        >
          <Icon iconName="Close" />
        </Button>
        <Heading className="mb-8" as="h2" size="L" align="center">
          قوانین و مقررات
        </Heading>
        <Flex>
          <Text textSize="M">
            لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ، و با
            استفاده از طراحان گرافیک است، چاپگرها و متون بلکه روزنامه و مجله در
            ستون و سطرآنچنان که لازم است، و برای شرایط فعلی تکنولوژی مورد نیاز،
            و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد، کتابهای
            زیادی در شصت و سه درصد گذشته حال و آینده، شناخت فراوان جامعه و
            متخصصان را می طلبد، تا با نرم افزارها شناخت بیشتری را برای طراحان
            رایانه ای علی الخصوص طراحان خلاقی، و فرهنگ پیشرو در زبان فارسی ایجاد
            کرد، در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه
            راهکارها، و شرایط سخت تایپ به پایان رسد و زمان مورد نیاز شامل
            حروفچینی دستاوردهای اصلی، و جوابگوی سوالات پیوسته اهل دنیای موجود
            طراحی اساسا مورد استفاده قرار گیرد.
          </Text>
        </Flex>
      </div>
    </Flex>
  );
}
