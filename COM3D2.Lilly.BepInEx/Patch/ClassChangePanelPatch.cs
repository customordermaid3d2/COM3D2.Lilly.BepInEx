using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    class ClassChangePanelPatch
    {
        public static Dictionary<int, SortedList<int, List<Skill.Data>>> job_class_special_skill_list_;// = new Dictionary<int, SortedList<int, List<Skill.Data>>>();
        public static Dictionary<int, ClassUnit> job_class_unit_dic_;// = new Dictionary<int, ClassUnit>();
        public static Dictionary<int, ClassUnit> yotogi_class_unit_dic_;// = new Dictionary<int, ClassUnit>();

        [HarmonyPatch(typeof(ClassChangePanel), "Awake")]
        [HarmonyPostfix]
        public static void Awake( Dictionary<int, ClassUnit> ___job_class_unit_dic_, Dictionary<int, ClassUnit> ___yotogi_class_unit_dic_)
        {
            if (job_class_unit_dic_.Count != null)
            {
                return;
            }
            MyLog.LogMessage(MyUtill.GetClassMethodName(MethodBase.GetCurrentMethod()));
            job_class_unit_dic_ = ___job_class_unit_dic_;
            yotogi_class_unit_dic_ = ___yotogi_class_unit_dic_;

            /* GetGameInfo() 로 이동
            foreach (KeyValuePair<int, ClassUnit> item in job_class_unit_dic_)
            {
                ClassUnit value = item.Value;
                JobClass.Data data = JobClass.GetData(value.maid_class_type);
                MyLog.LogMessage("JobClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName

            }
            foreach (KeyValuePair<int, ClassUnit> item in yotogi_class_unit_dic_)
            {
                ClassUnit value = item.Value;
                YotogiClass.Data data = YotogiClass.GetData(value.yotogi_class_type);
                MyLog.LogMessage("YotogiClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
            }
            */

        }

        [HarmonyPatch(typeof(ClassChangePanel), "CreateClassText")]
        [HarmonyPostfix]
        public static void CreateClassText(Dictionary<int, SortedList<int, List<Skill.Data>>> ___job_class_special_skill_list_)
        {
            if (job_class_special_skill_list_ != null)
            {
                return;
            }
            MyLog.LogMessage(MyUtill.GetClassMethodName(MethodBase.GetCurrentMethod()));
            //return;
            job_class_special_skill_list_ = ___job_class_special_skill_list_;

            return;

            foreach (JobClass.Data data in JobClass.GetAllDatas(false))
            {
                job_class_special_skill_list_.Add(data.id, new SortedList<int, List<Skill.Data>>());
            }
        }

        //private Dictionary<int, ClassUnit> job_class_unit_dic_ = new Dictionary<int, ClassUnit>();               
        //private Dictionary<int, ClassUnit> yotogi_class_unit_dic_ = new Dictionary<int, ClassUnit>();

        public static Maid maid_;

        // public void SetTargetMaid(Maid maid)
        [HarmonyPatch(typeof(ClassChangePanel), "SetTargetMaid")]
        [HarmonyPostfix]
        public static void SetTargetMaid(Maid maid)
        {
            MyLog.LogMessage(MyUtill.GetClassMethodName(MethodBase.GetCurrentMethod()));
            maid_ = maid;
            return;
            /*
            if (this.job_class_unit_dic_.Count == 0)
            {
                bool activeSelf = base.gameObject.activeSelf;
                base.gameObject.SetActive(true);
                this.Awake();
                base.gameObject.SetActive(activeSelf);
            }
            this.maid_ = maid;
            Status status = this.maid_.status;
            foreach (KeyValuePair<int, ClassUnit> keyValuePair in this.job_class_unit_dic_)
            {
                keyValuePair.Value.UpdateMaidData(this.maid_);
            }
            this.jobUIs.tabPanel.ResetSelect();
            foreach (KeyValuePair<int, ClassUnit> keyValuePair2 in this.job_class_unit_dic_)
            {
                ClassUnit value = keyValuePair2.Value;
                value.button.SetSelect(false);
                value.button.isEnabled = status.jobClass.Contains(value.maid_class_type);
                JobClass.Data data = JobClass.GetData(value.maid_class_type);
                if (MaidManagement.compatibilityMode)
                {
                    value.gameObject.SetActive(data.classType != AbstractClassData.ClassType.New);
                }
                else
                {
                    value.gameObject.SetActive(data.classType != AbstractClassData.ClassType.Old);
                }
                if (value.gameObject.activeSelf)
                {
                    value.gameObject.SetActive(data.learnConditions.isLearnPossiblePersonal(maid.status.personal));
                }
            }
            GameObject gameObject = this.yotogiUIs.itemGrid.gameObject;
            while (gameObject.transform.parent != null && !gameObject.transform.gameObject.activeInHierarchy)
            {
                gameObject = ((!(gameObject.transform.parent != null)) ? null : gameObject.transform.parent.gameObject);
            }
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
            this.jobUIs.Reset();
            int num = (status.selectedJobClass == null) ? int.MinValue : status.selectedJobClass.data.id;
            if (num != -2147483648)
            {
                this.jobUIs.tabPanel.Select(this.job_class_unit_dic_[num].button);
                if (this.jobUIs.classListObject.activeSelf)
                {
                    this.job_class_unit_dic_[num].UpdateInfo();
                }
            }
            foreach (KeyValuePair<int, ClassUnit> keyValuePair3 in this.yotogi_class_unit_dic_)
            {
                keyValuePair3.Value.UpdateMaidData(this.maid_);
            }
            this.yotogiUIs.tabPanel.ResetSelect();
            foreach (KeyValuePair<int, ClassUnit> keyValuePair4 in this.yotogi_class_unit_dic_)
            {
                ClassUnit value2 = keyValuePair4.Value;
                value2.button.SetSelect(false);
                value2.button.isEnabled = status.yotogiClass.Contains(value2.yotogi_class_type);
                YotogiClass.Data data2 = YotogiClass.GetData(value2.yotogi_class_type);
                value2.gameObject.SetActive(data2.learnConditions.isLearnPossiblePersonal(maid.status.personal));
            }
            gameObject = this.yotogiUIs.itemGrid.gameObject;
            while (gameObject.transform.parent != null && !gameObject.transform.gameObject.activeInHierarchy)
            {
                gameObject = ((!(gameObject.transform.parent != null)) ? null : gameObject.transform.parent.gameObject);
            }
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
            this.yotogiUIs.Reset();
            num = ((status.selectedYotogiClass == null) ? int.MinValue : status.selectedYotogiClass.data.id);
            if (num != -2147483648)
            {
                this.yotogiUIs.tabPanel.Select(this.yotogi_class_unit_dic_[num].button);
                if (this.yotogiUIs.classListObject.activeSelf)
                {
                    this.yotogi_class_unit_dic_[num].UpdateInfo();
                }
            }
            if (this.class_type_tab_panel_.GetSelectButtonObject() == null)
            {
                this.class_type_tab_panel_.Select(this.job_class_button_);
            }

            */
        }
    }
}

/*
data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termName, data.termExplanatoryText);
0 , Old , ノービスメイド , Novice , 駆け出しのメイドにぴったりなメイドクラス。最初はここから始まります。メイドの作法をしっかり覚えていきましょう。 , MaidStatus / ジョブクラス / Novice , MaidStatus / ジョブクラス / 説 明 / Novice
1 , Old , ラブリーメイド , Lovely , 可憐なメイドにぴったりなメイドクラス。守ってあげたくなるような可愛さを滲ませます。 , MaidStatus / ジョブクラス / Lovely , MaidStatus / ジョブクラス / 説明 / Lovely
2 , Old , エレガンスメイド , Elegance , 優雅な振る舞いがとても似合うメイドためのメイドクラス。普段から優雅に振る舞っていると、ついつい紅茶を淹れたくなるようです。 , MaidStatus / ジョブクラス / Elegance , MaidStatus / ジョブクラス / 説明 / Elegance
3 , Old , セクシーメイド , Sexy , セクシーな魅力で周りを惹きつけるメイドのためのメイドクラス。刺さる視線がもっとメイドをセクシーにしていきます。 , MaidStatus / ジョブクラス / Sexy , MaidStatus / ジョブクラス / 説明 / Sexy
4 , Old , イノセントメイド , Innocent , 無垢な心を忘れないメイドのためのメイドクラス。小さな気遣いや思いやりが、周りの人々を幸せにしていきます。 , MaidStatus / ジョブクラス / Innocent , MaidStatus / ジョブクラス / 説 明 / Innocent
5 , Old , チャームメイド , Charm , ステキな魅力を振りまくメイドのためのメイドクラス。内面から溢れ出す不思議な魅力に、虜になってしまいそうです。 , MaidStatus / ジョブクラス / Charm , MaidStatus / ジョブクラス / 説明 / Charm
6 , Old , レディーメイド , Ready , 落ち着いたレディのためのメイドクラス。品のあるレディを目指すメイドにも。最高のレディを目指していきましょう。 , MaidStatus / ジョブクラス / Ready , MaidStatus / ジョブクラス / 説明 / Ready
10 , New , ノービスメイド , Novice2 , メイドとしての初歩を歩み始めた事を表すジョブメイドクラス。まずはメイドとしての知識と経験を。 , MaidStatus / ジョブクラス / Novice2 , MaidStatus / ジョブクラス / 説明 / Novice2
70 , New , エンパイアメイド , Empire , 接客、上流階級の知識など、劇場で働く際に必要な技術の習熟を表すジョブメイドクラス。劇場を建設する事で習得可能。最も伝統的なジョブメイドクラスです。 , MaidStatus / ジョブクラス / Empire , MaidStatus / ジョブクラス / 説明 / Empire
20 , New , コンシェルジュメイド , Concierge , ベッドメイクやドア係など、ホテルで働く際に必要な技術の習熟を表すジョブメイドクラス。ホテルを建設する事で習得可能。特別な記念日のフォローなども行います。 , MaidStatus / ジョブクラス / Concierge , MaidStatus / ジョブクラス / 説明 / Concierge
60 , New , セラピストメイド , Therapist , 施術全般や健康に関わる事など、リフレで働く際に必要な技術の習熟を表すジョブメイドクラス。リフレを建設する事で習得可能。お客様の身も心も癒します。 , MaidStatus / ジョブク ラス / Therapist , MaidStatus / ジョブクラス / 説明 / Therapist
100 , New , ナイトメイド , Night , 女王様やM嬢など、SMクラブで働く際に必要な技術の習熟を表すジョブメイドクラス。SMクラブを建設する事で習得可能。背徳的でアブノーマルな奉仕をお客様に。 , MaidStatus / ジョブクラス / Night , MaidStatus / ジョブクラス / 説明 / Night


0 , Old , デビューメイド , Debut , 夜伽経験の浅い未熟なメイドにぴったりな夜伽クラス。夜伽の道は茨の道。じっくり技術を磨いていきましょう。 , MaidStatus / 夜伽クラス / Debut , MaidStatus / 夜伽クラス / 説明 / Debut
1 , Old , ルードネスメイド , Rudeness , 夜伽に対して積極的なメイドにぴったりな夜伽クラス。美しくも淫らで、抜け出せない沼へようこそ。 , MaidStatus / 夜伽クラス / Rudeness , MaidStatus / 夜伽クラス / 説明 / Rudeness
2 , Old , スレイブメイド , Slave , 被虐的なプレイも嬉々として受け入れてしまうメイドにぴったりな夜伽クラス。気絶には気を付けましょう。 , MaidStatus / 夜伽クラス / Slave , MaidStatus / 夜伽クラス / 説明 / Slave
3 , Old , アブノーマルメイド , Abnormal , ちょっぴり変態的なプレイがクセになりそうなメイドにぴったりな夜伽クラス。だんだんそれもクセになる？ , MaidStatus / 夜伽クラス / Abnormal , MaidStatus / 夜伽クラス / 説明 / Abnormal
4 , Share , 詰られサービスメイド , Service , ご主人様を詰る夜伽を習得する夜伽クラス。さっさとそこに跪いてブヒブヒ鳴きなさい。 , MaidStatus / 夜伽クラス / Service , MaidStatus / 夜伽クラス / 説明 / Service
5 , Old , エスコートメイド , Escort , 夜伽を自分からリードしていきたいと思うメイドにぴったりな夜伽クラス。甘くて淫靡な快楽の虜です。 , MaidStatus / 夜伽クラス / Escort , MaidStatus / 夜伽クラス / 説明 / Escort
6 , Share , ソープベーシックメイド , Soap , 基礎的なソープランドで行う夜伽を習得する夜伽クラス。魅惑と癒しの一時をあなたに。 , MaidStatus / 夜伽クラス / Soap , MaidStatus / 夜伽クラス / 説明 / Soap
7 , Old , スウィートメイド , Sweet , ご主人様と恋人以上な関係のメイドにぴったりな夜伽クラス。止まらないラブなハートが甘くホットに燃えあがる。 , MaidStatus / 夜伽クラス / Sweet , MaidStatus / 夜伽クラス / 説明 / Sweet
8 , Old , パーティメイド , Party , たくさんの男達とのプレイにも抵抗がないメイドにぴったりな夜伽クラス。くんずほぐれつ酒池肉林！今夜は皆で朝までパーリナイッ！ , MaidStatus / 夜伽クラス / Party , MaidStatus / 夜伽クラス / 説明 / Party
9 , Old , セックススレイブメイド , SexSlave , まるで性奴隷のような扱いを望むメイドにぴったりな夜伽クラス。メイドを苛める道具、多数取り揃えております。 , MaidStatus / 夜伽クラス / SexSlave , MaidStatus / 夜伽クラス / 説明 / SexSlave
10 , Old , フェスティバルメイド , Festival , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。メイドをしっかりと受け止めて上げましょう。 , MaidStatus / 夜伽クラス / Festival , MaidStatus / 夜 伽クラス / 説明 / Festival
11 , Share , ハーレムメイド , Harlem , 複数人のメイドでご主人様の相手をする夜伽を習得する夜伽クラス。ご主人様だけでも恥ずかしいのに…… , MaidStatus / 夜伽クラス / Harlem , MaidStatus / 夜伽クラス / 説明 / Harlem
12 , Old , ロイヤルソープメイド , Royalsoap , 泡やマットを使ったプレイが上手なメイドにぴったりな夜伽クラス。魅惑の泡に溺れてしまいそう。 , MaidStatus / 夜伽クラス / Royalsoap , MaidStatus / 夜伽クラス / 説明 / Royalsoap
13 , Old , プレイメイド , Toilet , トイレで変態的なエッチをしてしまうメイドにぴったりな夜伽クラス。誰かに見られないように注意です。 , MaidStatus / 夜伽クラス / Toilet , MaidStatus / 夜伽クラス / 説明 / Toilet
14 , Old , インモラルメイド , Immoral , 電車で変態的なエッチをしてしまうメイドにぴったりな夜伽クラス。背徳的な快楽を楽しみましょう。 , MaidStatus / 夜伽クラス / Immoral , MaidStatus / 夜伽クラス / 説明 / Immoral
15 , Old , ブライドメイド , Bride , まるで花嫁にするかようなエッチが大好きなメイドにぴったりな夜伽クラス。色々な体位で二人の愛を育みましょう。 , MaidStatus / 夜伽クラス / Bride , MaidStatus / 夜伽クラス / 説明 / Bride
16 , Old , サキュバスメイド , Succubus , 淫魔のように精を搾り取りたいメイドにぴったりな夜伽クラス。ご主人様以外の男性にも可愛がってもらいましょう！ , MaidStatus / 夜伽クラス / Succubus , MaidStatus / 夜伽クラス / 説明 / Succubus
17 , Old , クイーンメイド , Queen , ご主人様を豚として扱いたいメイドにぴったりな夜伽クラス。主従逆転の快楽をメイド様から教えて頂きましょう！ , MaidStatus / 夜伽クラス / Queen , MaidStatus / 夜伽クラス / 説明 / Queen
18 , Old , ダークネスメイド , Darkness , ダークでハードな夜伽を望むメイドにぴったりな夜伽クラス。その痛みはご主人様とメイドの為のもの。 , MaidStatus / 夜伽クラス / Darkness , MaidStatus / 夜伽クラス / 説明 / Darkness
19 , Old , スクールメイド , School , 学園での夜伽が大好きなメイドにぴったりな夜伽クラス。ご主人様という呼び方もここでは変わります。 , MaidStatus / 夜伽クラス / School , MaidStatus / 夜伽クラス / 説明 / School
20 , Old , NTRメイド , NTR , お客様に心まで寝取られてしまったメイドにぴったりな夜伽クラス。お客様と乱れるメイドをしっかりと見てあげましょう。 , MaidStatus / 夜伽クラス / NTR , MaidStatus / 夜伽クラス / 説明 / NTR
21 , Old , マリッジメイド , Marriage , 愛するご主人様との夜伽が大好きなメイドにぴったりな夜伽クラス。大好きなご主人様といつまでも。 , MaidStatus / 夜伽クラス / Marriage , MaidStatus / 夜伽クラス / 説明 / Marriage
22 , Old , リリィメイド , Lily , 女の子同士での夜伽が大好きなメイドにぴったりな夜伽クラス。メイドは禁断の夜伽に溺れます。 , MaidStatus / 夜伽クラス / Lily , MaidStatus / 夜伽クラス / 説明 / Lily
23 , Old , カーニバルメイド , Carnival , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。今度は謝肉祭だ！ , MaidStatus / 夜伽クラス / Carnival , MaidStatus / 夜伽クラス / 説明 / Carnival
24 , Old , チェリッシュメイド , Cherish , ご主人様だけの大切なメイドにぴったりな夜伽クラス。イチャイチャ、甘々。 , MaidStatus / 夜伽クラス / Cherish , MaidStatus / 夜伽クラス / 説明 / Cherish
25 , Old , バインドメイド , Bind , 逃げられぬ愛の拘束を求めるメイドにぴったりな夜伽クラス。ただ一人だけ、ご主人様の支配を受けたいから。 , MaidStatus / 夜伽クラス / Bind , MaidStatus / 夜伽クラス / 説明 / Bind
26 , Old , エロティックメイド , Erotic , ド変態なエッチが大好きなメイドにぴったりな夜伽クラス。しっかりとメイドの痴態を目に焼き付けましょう。 , MaidStatus / 夜伽クラス / Erotic , MaidStatus / 夜伽クラス / 説明 / Erotic
27 , Old , セレブレイトメイド , Celebrate , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。祝福されたメイドとエッチしましょう。 , MaidStatus/夜伽クラス/Celebrate , MaidStatus/夜伽クラ ス/説明/Celebrate
28 , Old , ディスペアメイド , Despair , 群がる男達にムリヤリメイドが犯されてしまう夜伽クラス。メイドの顔が絶望に歪みます。 , MaidStatus/夜伽クラス/Despair , MaidStatus/夜伽クラス/説明/Despair
29 , Old , エンプレスメイド , Empress , ご主人様をオモチャにしたいメイドにぴったりな夜伽クラス。それは愛の執着でもあります。 , MaidStatus/夜伽クラス/Empress , MaidStatus/夜伽クラス/説明/Empress
30 , Old , イチャラブメイド , Ityalove , どれだけ幸せになっても、まだまだ足りないと感じるメイドにぴったりな夜伽クラス。いつまでもイチャラブ。 , MaidStatus/夜伽クラス/Ityalove , MaidStatus/夜伽クラス/説明/Ityalove
31 , Old , ライブメイド , Live , ライブの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。ちょっと変態なエッチもご主人様となら。 , MaidStatus/夜伽クラス/Live , MaidStatus/夜伽クラス/説明/Live
32 , Old , アビスメイド , abyss , 群がる男達に無理矢理メイドがさらにハードに犯されてしまう夜伽クラス。それは失意の混沌。 , MaidStatus/夜伽クラス/abyss , MaidStatus/夜伽クラス/説明/abyss
33 , Old , ピッグブリーダーメイド , Breeding , M豚を調教する事に特化した夜伽クラス。跪いて泣いて頼むのなら、相手してあげてもいいわ。 , MaidStatus/夜伽クラス/Breeding , MaidStatus/夜伽クラス/説明/Breeding
34 , Old , ラブバインドメイド , Victim , 拘束され、欲望のままに好き勝手に犯されてしまう夜伽クラス。メイドはご主人様の欲望の被害者なのか、それとも…… , MaidStatus/夜伽クラス/Victim , MaidStatus/夜伽クラス/説明/Victim
35 , Old , アニバーサリーメイド , Anniversary , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。これまでもこれからもよろしく！ , MaidStatus/夜伽クラス/Anniversary , MaidStatus/夜伽クラ ス/説明/Anniversary
36 , Old , エクスタシーメイド , Ecstasy , 貪欲に快楽を求めるメイドにぴったりな夜伽クラス。メイドに感じた事の無い絶頂を何度でもプレゼントしましょう。 , MaidStatus/夜伽クラス/Ecstasy , MaidStatus/夜伽クラス/説明/Ecstasy
37 , Old , バグバグメイド , Bugbug , 長く続く名誉ある雑誌の為に用意された夜伽クラス。快楽と背徳の狭間をメイドと楽しみましょう。 , MaidStatus/夜伽クラス/Bugbug , MaidStatus/夜伽クラス/説明/Bugbug
38 , Old , クライシスメイド , Crisis , 台の上で、ソファの上で、男たちに弄ばれる夜伽クラス。抵抗できないメイドの行く末は、まさに危機一髪。 , MaidStatus/夜伽クラス/Crisis , MaidStatus/夜伽クラス/説明/Crisis
39 , Old , ヒーリングメイド , Healing , メイドから癒やしと快楽を与えられたいときの夜伽クラス。時には優しく、時には厳しいご奉仕を貴方へ。 , MaidStatus/夜伽クラス/Healing , MaidStatus/夜伽クラス/説明/Healing
40 , Old , オビディエントメイド , Obedient , 嗜虐的な欲望、その全てをメイドに与えたいときの夜伽クラス。もっと拘束して、もっと激しく苛め倒す。 , MaidStatus/夜伽クラス/Obedient , MaidStatus/夜伽クラス/説明/Obedient
42 , New , 変態辱めセックスメイド , Hentaihazukasime , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime , MaidStatus/夜伽クラス/説明/Hentaihazukasime
43 , New , ソープご奉仕セックスメイド , Sorpgohousi , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi , MaidStatus/夜伽クラス/説明/Sorpgohousi
44 , Share , 変態辱めセックスメイド , Hentaihazukasime_old , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して……おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_old , MaidStatus/夜伽クラス/説明/Hentaihazukasime_old
45 , Share , ソープご奉仕セックスメイド , Sorpgohousi_old , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ……？ , MaidStatus/夜伽クラス/Sorpgohousi_old , MaidStatus/夜伽クラス/説明/Sorpgohousi_old
46 , Share , アブノーマル卑猥セックスメイド , HiwaiSex , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex , MaidStatus/夜伽クラス/説明/HiwaiSex
47 , Share , 癒しラブラブ奉仕メイド , LoveLoveHoushi , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi , MaidStatus/夜伽クラス/説明/LoveLoveHoushi
48 , New , アブノーマル卑猥セックスメイド , HiwaiSex_old , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_old , MaidStatus/夜伽クラス/説明/HiwaiSex_old
49 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_old , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_old , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_old
52 , New , 変態辱めセックスメイド , Hentaihazukasime_sil , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_sil , MaidStatus/夜伽クラス/説明/Hentaihazukasime_sil
53 , New , ソープご奉仕セックスメイド , Sorpgohousi_sil , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_sil , MaidStatus/夜伽クラス/説明/Sorpgohousi_sil
54 , New , アブノーマル卑猥セックスメイド , HiwaiSex_sil , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_sil , MaidStatus/夜伽クラス/説明/HiwaiSex_sil
55 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_sil , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_sil , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_sil
56 , New , らぶらぶメイド , lovelove , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/lovelove , MaidStatus/夜伽クラス/説明/lovelove
57 , New , 欲情メイド , yokujyou , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/yokujyou , MaidStatus/夜伽クラス/説明/yokujyou
58 , Share , らぶらぶメイド , lovelove_old , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/lovelove_old , MaidStatus/夜伽クラス/説明/lovelove_old
59 , Share , 欲情メイド , yokujyou_old , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/yokujyou_old , MaidStatus/夜伽クラス/説明/yokujyou_old
60 , New , 変態辱めセックスメイド , Hentaihazukasime_dvl , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_dvl , MaidStatus/夜伽クラス/説明/Hentaihazukasime_dvl
61 , New , ソープご奉仕セックスメイド , Sorpgohousi_dvl , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_dvl , MaidStatus/夜伽クラス/説明/Sorpgohousi_dvl
62 , New , アブノーマル卑猥セックスメイド , HiwaiSex_dvl , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_dvl , MaidStatus/夜伽クラス/説明/HiwaiSex_dvl
63 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_dvl , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_dvl , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_dvl
64 , New , 拘束変態責めメイド , Kousokuhentaiseme , メイドを拘束し、ハードに責める夜伽クラス。拘束し、絶対に逃げられない状態でメイドを苛烈に責めてあげましょう , MaidStatus/夜伽クラス/Kousokuhentaiseme , MaidStatus/夜伽クラス/説明/Kousokuhentaiseme
65 , New , ハードセックスメイド , Hardsex , 愛しのメイドに対し、もっとたくさんセックスをして愛し合いたい時の目夜伽クラス。終わらない夜をメイドと。 , MaidStatus/夜伽クラス/Hardsex , MaidStatus/夜伽クラス/説明/Hardsex
66 , Share , 拘束変態責めメイド , Kousokuhentaiseme_old , メイドを拘束し、ハードに責める夜伽クラス。拘束し、絶対に逃げられない状態でメイドを苛烈に責めてあげましょう , MaidStatus/夜伽クラス/Kousokuhentaiseme_old , MaidStatus/夜伽クラス/説明/Kousokuhentaiseme_old
67 , Share , ハードセックスメイド , Hardsex_old , 愛しのメイドに対し、もっとたくさんセックスをして愛し合いたい時の目夜伽クラス。終わらない夜をメイドと。 , MaidStatus/夜伽クラス/Hardsex_old , MaidStatus/夜伽ク ラス/説明/Hardsex_old
68 , New , 変態辱めセックスメイド , Hentaihazukasime_ldy , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_ldy , MaidStatus/夜伽クラス/説明/Hentaihazukasime_ldy
69 , New , ソープご奉仕セックスメイド , Sorpgohousi_ldy , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_ldy , MaidStatus/夜伽クラス/説明/Sorpgohousi_ldy
70 , New , アブノーマル卑猥セックスメイド , HiwaiSex_ldy , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_ldy , MaidStatus/夜伽クラス/説明/HiwaiSex_ldy
71 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_ldy , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_ldy , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_ldy
72 , Share , 服従セックスメイド , Fukujyuusex_old , メイドを服従させ拘束し、ご主人様の意のままにする夜伽クラス。全てはご主人様の為に。 , MaidStatus/夜伽クラス/Fukujyuusex_old , MaidStatus/夜伽クラス/説明/Fukujyuusex_old
73 , Share , ハードスワップセックスメイド , Hardswapsex_old , メイドに対し別の男とセックスさせる夜伽クラス。他の男に弄ばれ嬲られるメイド。 , MaidStatus/夜伽クラス/Hardswapsex_old , MaidStatus/夜伽クラス/説明/Hardswapsex_old
74 , Share , バグバグオーダーメイド , BUGBUGorder_old , 長く続く名誉ある雑誌の為に用意された夜伽クラス。詰られたり露出したり、ド変態なプレイを楽しみましょう！ , MaidStatus/夜伽クラス/BUGBUGorder_old , MaidStatus/夜伽クラス/説明/BUGBUGorder_old
75 , Share , マスタープレイメイド , Masterplay_old , メイドがご主人様となり、ご主人様を嬲り倒す夜伽クラス。『お前』と呼び方が変わり、詰られてしまいます。 , MaidStatus/夜伽クラス/Masterplay_old , MaidStatus/夜 伽クラス/説明/Masterplay_old
76 , Share , 変態恥辱セックスメイド , Hentaitijyokusex_old , メイドにポーズをとらせたり、メイドを乱暴に責めたりする夜伽クラス。変態恥辱責めに、メイドは淫らな本性を現し…… , MaidStatus/夜伽クラス/Hentaitijyokusex_old , MaidStatus/夜伽クラス/説明/Hentaitijyokusex_old
77 , Share , 甘やかしマザーメイド , Mamameido_old , 普段疲れているあなたを、ママが包まれるように甘やかしてくれる夜伽クラス。ママはあなたを優しく癒してくれます。 , MaidStatus/夜伽クラス/Mamameido_old , MaidStatus/夜伽クラス/説明/Mamameido_old
78 , Share , ご奉仕マザーメイド , MamameidoB_old , 普段疲れているあなたへ、ママがご奉仕してくれる夜伽クラス。ママはあなたを優しくご奉仕してくれます。 , MaidStatus/夜伽クラス/MamameidoB_old , MaidStatus/夜伽ク ラス/説明/MamameidoB_old
80 , New , アクティブセックスメイド , Activesex , 凜デレタイプ限定の夜伽クラス。お姉さんらしくリードしてくれます。 , MaidStatus/夜伽クラス/Activesex , MaidStatus/夜伽クラス/説明/Activesex
81 , Share , マニアックプレイメイド , Maniacplay_old , かなり変態的な夜伽クラス。犬の真似をさせたり、お尻の穴を舐めたり……異常な性癖を楽しみましょう。 , MaidStatus/夜伽クラス/Maniacplay_old , MaidStatus/夜伽 クラス/説明/Maniacplay_old
82 , Share , ハード拘束プレイメイド , Hardkousokuplay_old , 器具を用いてメイドを拘束する夜伽クラス。がちがちに拘束したメイドを、ご主人様のお好きなままに。 , MaidStatus/夜伽クラス/Hardkousokuplay_old , MaidStatus/夜伽クラス/説明/Hardkousokuplay_old
83 , Share , アブノーマル卑猥メイド・病 , HiwaiSex_yan , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。※この夜伽クラスはヤンデレのみ習得致します※ , MaidStatus/夜伽クラス/HiwaiSex_yan , MaidStatus/夜伽クラス/説明/HiwaiSex_yan
84 , Share , 癒しラブラブ奉仕メイド・病 , LoveLoveHoushi_yan , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。※この夜伽クラスはヤンデレのみ習得致します※ , MaidStatus/夜伽クラス/LoveLoveHoushi_yan , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_yan
90 , New , アブノーマルエクスタシーメイド , Abnormalecstasy , 普通のセックスでは行わないような、強い快楽をメイドに与える夜伽クラス。普通ではいられない…… , MaidStatus/夜伽クラス/Abnormalecstasy , MaidStatus/ 夜伽クラス/説明/Abnormalecstasy
100 , New , アブノーマルセックスメイド , Abnormalsex , 普通のセックスでは行わないようなセックスをする夜伽を習得する夜伽クラス。快楽からは逃れられない。 , MaidStatus/夜伽クラス/Abnormalsex , MaidStatus/夜伽クラス/説明/Abnormalsex
110 , New , エクスタシーベーシックメイド , Ecstasybasic , 基礎的なセックス夜伽を習得する夜伽クラス。何度も何度も愛し合いましょう。 , MaidStatus/夜伽クラス/Ecstasybasic , MaidStatus/夜伽クラス/説明/Ecstasybasic
120 , New , 献身セックスメイド , Kensinsex , 無垢タイプ限定の夜伽クラス。無垢なメイドがセックスで誠心誠意ご主人様を癒します。 , MaidStatus/夜伽クラス/Kensinsex , MaidStatus/夜伽クラス/説明/Kensinsex
130 , New , 専属専用ラブラブアナルセックスメイド , Senzokusenyouloveloveanalsex , 専属メイド限定の夜伽クラス。ご主人様だけに大好きを伝え、お尻を捧げます。 , MaidStatus/夜伽クラス/Senzokusenyouloveloveanalsex , MaidStatus/夜伽クラス/説明/Senzokusenyouloveloveanalsex
140 , New , 専属専用ラブラブセックスメイド , Senzokusenyoulovelovesex , 専属メイド限定の夜伽クラス。ご主人様だけに大好きを伝えます。 , MaidStatus/夜伽クラス/Senzokusenyoulovelovesex , MaidStatus/夜伽クラス/説 明/Senzokusenyoulovelovesex
150 , New , 倒錯変態プレイメイド , hentaiplay , 真面目タイプ限定の夜伽クラス。通常ではありえない場所での倒錯的な夜伽を頑張ってくれます。 , MaidStatus/夜伽クラス/hentaiplay , MaidStatus/夜伽クラス/説明/hentaiplay
151 , New , 倒錯変態セックスメイド , hentaisex , 真面目タイプ限定の夜伽クラス。通常ではありえないシチュエーションでの倒錯的なセックスに溺れます。 , MaidStatus/夜伽クラス/hentaisex , MaidStatus/夜伽クラス/説明/hentaisex
160 , New , オナニーベーシックメイド , Onaniebasic , 基礎的なオナニー夜伽を習得する夜伽クラス。ご主人様の前でオナニーを行います。 , MaidStatus/夜伽クラス/Onaniebasic , MaidStatus/夜伽クラス/説明/Onaniebasic
170 , New , 変態エキスパートメイド , Hentaiexpert , 過激な変態夜伽を習得する夜伽クラス。羞恥は鮮烈な夜伽のスパイス。 , MaidStatus/夜伽クラス/Hentaiexpert , MaidStatus/夜伽クラス/説明/Hentaiexpert
180 , New , 変態ベーシックメイド , Hentaibasic , 基礎的な変態夜伽を習得する夜伽クラス。羞恥を強く感じるメイドを愛でましょう。 , MaidStatus/夜伽クラス/Hentaibasic , MaidStatus/夜伽クラス/説明/Hentaibasic
190 , New , 露出ベーシックメイド , Rosyutubasic , 基礎的な露出夜伽を習得する夜伽クラス。エンパイアクラブから出て、更なる羞恥を感じましょう。 , MaidStatus/夜伽クラス/Rosyutubasic , MaidStatus/夜伽クラス/説明/Rosyutubasic
200 , New , ソープエキスパートメイド , Soapexpert , ソープランドで行う熟練したソープ夜伽を習得する夜伽クラス。培った癒しの技術でご主人様に尽くします。 , MaidStatus/夜伽クラス/Soapexpert , MaidStatus/夜伽クラス/説明/Soapexpert
210 , New , 奉仕エキスパートメイド , Houshiexpert , 発展的な奉仕夜伽を習得する夜伽クラス。より濃厚な奉仕をご主人様に。 , MaidStatus/夜伽クラス/Houshiexpert , MaidStatus/夜伽クラス/説明/Houshiexpert
220 , New , 奉仕ベーシックメイド , HoushiBasic , 基礎的な奉仕夜伽を習得する夜伽クラス。エンパイアクラブのメイドとしての基礎の基礎といえるでしょう。 , MaidStatus/夜伽クラス/HoushiBasic , MaidStatus/夜伽クラス/ 説明/HoushiBasic
230 , New , Ｍ女エキスパートメイド , Monnaexpert , 苛烈な被虐夜伽を習得する夜伽クラス。ご主人様となら、どんな事だって。 , MaidStatus/夜伽クラス/Monnaexpert , MaidStatus/夜伽クラス/説明/Monnaexpert
240 , New , Ｍ女ベーシックメイド , MonnaBasic , 基礎的な被虐夜伽を習得する夜伽クラス。ご主人様から与えられる痛みなら…… , MaidStatus/夜伽クラス/MonnaBasic , MaidStatus/夜伽クラス/説明/MonnaBasic
250 , New , スワッピングベーシックメイド , Swappingbasic , ご主人様以外の男性とスワッピングを行う夜伽を習得する夜伽クラス。ご主人様以外となんて…… , MaidStatus/夜伽クラス/Swappingbasic , MaidStatus/夜伽クラス/説明/Swappingbasic
260 , New , 乱交愛撫ベーシックメイド , Rankoubasic , ご主人様以外の男性を加えて複数人で行う夜伽を夜伽を習得する夜伽クラス。 , MaidStatus/夜伽クラス/Rankoubasic , MaidStatus/夜伽クラス/説明/Rankoubasic
270 , New , 酔いアナルベーシックメイド , Yoianalbasic , 酔った状態で行う夜伽を習得する夜伽クラス。開放的になったメイドのお尻の味を楽しみましょう。 , MaidStatus/夜伽クラス/Yoianalbasic , MaidStatus/夜伽クラス/ 説明/Yoianalbasic
280 , New , 酔いエキスパートメイド , Yoiexpart , 酔った状態で行う夜伽を習得する夜伽クラス。開放的になったメイドに、過激な夜伽を頼みましょう。 , MaidStatus/夜伽クラス/Yoiexpart , MaidStatus/夜伽クラス/説明/Yoiexpart
290 , New , 酔いベーシックメイド , Yoibasic , 酔った状態で行う夜伽を習得する夜伽クラス。開放的になったメイドとの甘い夜伽を楽しみましょう。 , MaidStatus/夜伽クラス/Yoibasic , MaidStatus/夜伽クラス/説明/Yoibasic
300 , New , 媚薬プレイメイド , Biyakuplay , 媚薬を用いた夜伽を習得する夜伽クラス。塗布型の媚薬を塗って、普段と違う声色のメイドのメイドを楽しみましょう。 , MaidStatus/夜伽クラス/Biyakuplay , MaidStatus/夜伽クラス/説明/Biyakuplay
310 , New , バージンメイド , Virginplay , 初々しい反応を見せる夜伽クラス。初めての相手は、あなた。※処女を喪失すると夜伽クラスは消失します。 , MaidStatus/夜伽クラス/Virginplay , MaidStatus/夜伽クラス/説明/Virginplay
320 , New , 目隠しプレイメイド , Mekakusiplay , 目隠しを用いた夜伽を習得する夜伽クラス。見えないだけ敏感に…… , MaidStatus/夜伽クラス/Mekakusiplay , MaidStatus/夜伽クラス/説明/Mekakusiplay
330 , New , 告白プレイメイド , Kokuhakuplay , フリーメイド限定夜伽クラス。お客様との行為を詳細に、淫靡に告白します。 , MaidStatus/夜伽クラス/Kokuhakuplay , MaidStatus/夜伽クラス/説明/Kokuhakuplay
340 , New , エクスタシーアナルベーシックメイド , Ecstasyanalbasic , 基礎的なお尻でのセックス夜伽を習得する夜伽クラス。お尻でもご主人様と…… , MaidStatus/夜伽クラス/Ecstasyanalbasic , MaidStatus/夜伽クラス/説 明/Ecstasyanalbasic
350 , New , 露出奉仕メイド , Rosyutuhousi , 無垢タイプ限定の夜伽クラス。優しい無垢なメイドがお外でも健気に奉仕します。 , MaidStatus/夜伽クラス/Rosyutuhousi , MaidStatus/夜伽クラス/説明/Rosyutuhousi
360 , New , 変態ハードメイド , HentaiHard , 凜デレタイプ限定の夜伽クラス。より過激な変態行為をお姉さんと。 , MaidStatus/夜伽クラス/HentaiHard , MaidStatus/夜伽クラス/説明/HentaiHard
370 , New , 献身奉仕メイド , Kensinhoushi , 無垢タイプ限定の夜伽クラス。無垢なメイドが献身的に奉仕します。 , MaidStatus/夜伽クラス/Kensinhoushi , MaidStatus/夜伽クラス/説明/Kensinhoushi
380 , New , 変態奉仕メイド , Hentai , 真面目タイプ限定の夜伽クラス。むっつりな真面目タイプは、ご主人様を洗っているうちに…… , MaidStatus/夜伽クラス/Hentai , MaidStatus/夜伽クラス/説明/Hentai
390 , New , アクティブ奉仕メイド , Activehoushi , 凜デレタイプ限定の夜伽クラス。お姉さんらしく奉仕してくれます。 , MaidStatus/夜伽クラス/Activehoushi , MaidStatus/夜伽クラス/説明/Activehoushi
400 , New , 失禁セックスメイド , Sikkinsex , 真面目タイプ限定の夜伽クラス。吊るされ、犯されているうちに真面目タイプは…… , MaidStatus/夜伽クラス/Sikkinsex , MaidStatus/夜伽クラス/説明/Sikkinsex
410 , New , M女ハードメイド , Monnahard , 凜デレタイプ限定の夜伽クラス。凜デレタイプのみに耐えられる痛烈にハードな夜伽を。 , MaidStatus/夜伽クラス/Monnahard , MaidStatus/夜伽クラス/説明/Monnahard
420 , New , M女従順メイド , Monnajyuujyun , 無垢タイプ限定の夜伽クラス。従順なメイドを乱暴に犯します。 , MaidStatus/夜伽クラス/Monnajyuujyun , MaidStatus/夜伽クラス/説明/Monnajyuujyun
430 , New , ハードスワッピングメイド , Hardrankou , 凜デレタイプ限定の夜伽クラス。ご主人様以外の男性に、激しく、過激に犯されます。 , MaidStatus/夜伽クラス/Hardrankou , MaidStatus/夜伽クラス/説明/Hardrankou
440 , New , 変態スワッピングメイド , Hentairankou , 真面目タイプ限定の夜伽クラス。真面目なメイドがご主人様以外の男性に、激しく、 , MaidStatus/夜伽クラス/Hentairankou , MaidStatus/夜伽クラス/説明/Hentairankou
450 , New , 嬲り乱交メイド , Naburirankou , 無垢タイプ限定の夜伽クラス。無垢なメイドに、複数の男のいやらしい手が伸びます。 , MaidStatus/夜伽クラス/Naburirankou , MaidStatus/夜伽クラス/説明/Naburirankou
460 , New , ノービスメイド , beginner , エンパイアクラブのメイドとしての基礎的な夜伽クラス。全てはここから。 , MaidStatus/夜伽クラス/beginner , MaidStatus/夜伽クラス/説明/beginner
490 , New , 甘えプレイメイド , Amaeplay , 文学少女タイプ専用の夜伽クラス。甘えん坊になったあの子に、たっぷり甘えてもらいましょう。 , MaidStatus/夜伽クラス/Amaeplay , MaidStatus/夜伽クラス/説明/Amaeplay
500 , New , 言いなりプレイメイド , Iinariplay , 文学少女タイプ専用の夜伽クラス。ご主人様の言う事なら、何でも…… , MaidStatus/夜伽クラス/Iinariplay , MaidStatus/夜伽クラス/説明/Iinariplay
510 , New , 忠実奉仕メイド , TyujitsuHoushi , 文学少女タイプ専用の夜伽クラス。今まで勉強した奉仕を披露して、一生懸命奉仕してくれます。 , MaidStatus/夜伽クラス/TyujitsuHoushi , MaidStatus/夜伽クラス/説明/TyujitsuHoushi
520 , New , 語らせ責めメイド , Katarase , 文学少女タイプ専用の夜伽クラス。普段言わないようなあんな言葉やこんな言葉を、無理やり喋らせてみましょう。 , MaidStatus/夜伽クラス/Katarase , MaidStatus/夜伽クラス/説明/Katarase
530 , New , 語らせスワップメイド , KataraseSwap , 文学少女タイプ専用の夜伽クラス。お客様へ語りかけるあの子を見たいあなたへ。 , MaidStatus/夜伽クラス/KataraseSwap , MaidStatus/夜伽クラス/説明/KataraseSwap
540 , New , 嬌声プレイメイド , Kyouseiplay , 文学少女タイプ専用の夜伽クラス。あの子にあられもない言葉を言わせたいあなたへ。 , MaidStatus/夜伽クラス/Kyouseiplay , MaidStatus/夜伽クラス/説明/Kyouseiplay
550 , New , 逆転プレイメイド , Gyakutenplay , 小悪魔タイプ専用の夜伽クラス。いつもからかってくるあの子に、やり返してあげましょう。 , MaidStatus/夜伽クラス/Gyakutenplay , MaidStatus/夜伽クラス/説明/Gyakutenplay
560 , New , ビッチメイド , Bitch , 小悪魔タイプ専用の夜伽クラス。ビッチになってしまったあの子が、あなたにどんな言葉を投げかけるでしょうか…… , MaidStatus/夜伽クラス/Bitch , MaidStatus/夜伽クラス/説明/Bitch
570 , New , 小悪魔メイド , Koakuma , 小悪魔タイプ専用の夜伽クラス。あなたに喜んで貰うため、いつも以上に小悪魔っぽく…… , MaidStatus/夜伽クラス/Koakuma , MaidStatus/夜伽クラス/説明/Koakuma
580 , New , リードお姉さんメイド , Leadoneesan , おしとやかタイプ専用の夜伽クラス。おしとやかなお姉さんにリードされて果ててしまいましょう。 , MaidStatus/夜伽クラス/Leadoneesan , MaidStatus/夜伽クラス/説明/Leadoneesan
590 , New , M豚調教メイド , Mbutaoneesan , おしとやかタイプ専用の夜伽クラス。M豚化してしまったお姉さん嬲りを楽しみましょう。 , MaidStatus/夜伽クラス/Mbutaoneesan , MaidStatus/夜伽クラス/説明/Mbutaoneesan
600 , New , M豚目隠しメイド , Mbutamekakushi , おしとやかタイプ専用の夜伽クラス。M豚化してしまったお姉さんを目隠ししてイタズラしてしまいましょう。 , MaidStatus/夜伽クラス/Mbutamekakushi , MaidStatus/夜伽クラス/説明/Mbutamekakushi
610 , New , 甘やかしお姉さんメイド , Amayakasioneesan , おしとやかタイプ専用の夜伽クラス。おしとやかなお姉さんに思い切り甘えましょう。 , MaidStatus/夜伽クラス/Amayakasioneesan , MaidStatus/夜伽クラス/説明/Amayakasioneesan
620 , New , 見下し詰りメイド , Mikudasinaziri , おしとやかタイプ専用の夜伽クラス。おしとやかなお姉さんに見下されるのは……とても素敵な体験になるでしょう。 , MaidStatus/夜伽クラス/Mikudasinaziri , MaidStatus/夜伽クラス/説明/Mikudasinaziri
630 , New , テリブルクイーンメイド , Terribleｑueen , ご主人様を危険なぐらい詰り倒したいメイドの為の夜伽クラス。全てはドMなご主人様の為に。 , MaidStatus/夜伽クラス/Terribleｑueen , MaidStatus/夜伽クラス/説明/Terribleｑueen
640 , New , 孕ませメイド , Haramase , メイド秘書タイプ専用の夜伽クラス。何としてでもご主人様との赤ちゃんを孕みたいと思っています。 , MaidStatus/夜伽クラス/Haramase , MaidStatus/夜伽クラス/説明/Haramase
650 , New , 従順犬メイド , Juujyun , メイド秘書タイプ専用の夜伽クラス。飼い主様の命令に絶対従う、従順な犬となってセックスをするメイドになります。 , MaidStatus/夜伽クラス/Juujyun , MaidStatus/夜伽クラス/説明/Juujyun
660 , New , 熟練ソープメイド , Jukurensoap , メイド秘書タイプ専用の夜伽クラス。メイド秘書にしか体得できない、熟練したソープの技術を持っています。 , MaidStatus/夜伽クラス/Jukurensoap , MaidStatus/夜伽クラス/説 明/Jukurensoap
670 , New , 邪淫奉仕メイド , Jainhoushi , メイド秘書タイプ専用の夜伽クラス。背徳的なまでに淫らに、複数の飢えた男達に奉仕を行います。 , MaidStatus/夜伽クラス/Jainhoushi , MaidStatus/夜伽クラス/説明/Jainhoushi
680 , New , 主従逆転メイド , Shyujyuugyakuten , メイド秘書タイプ専用の夜伽クラス。ご主人様との主従関係を逆転させ、苛烈にご主人様を責めます。 , MaidStatus/夜伽クラス/Shyujyuugyakuten , MaidStatus/夜伽クラス/説 明/Shyujyuugyakuten
700 , New , メスガキメイド , Mesugaki , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/Mesugaki , MaidStatus/夜伽クラス/説明/Mesugaki
710 , New , わがままメイド , Wagamama , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/Wagamama , MaidStatus/夜伽クラス/説明/Wagamama
720 , New , 退行メイド , Taikou , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/Taikou , MaidStatus/夜伽クラス/説明/Taikou
730 , New , ハードスワップメイド , Hardswap , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/Hardswap , MaidStatus/夜伽クラス/説明/Hardswap
740 , New , おもらしわんちゃんメイド , Omorashiwantyan , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/Omorashiwantyan , MaidStatus/夜伽クラス/説明/Omorashiwantyan
750 , New , 説教メイド , Sekkyou , 無愛想タイプ専用の夜伽クラス。ご主人様を徹底的に説教して、真人間に矯正させようとしています。 , MaidStatus/夜伽クラス/Sekkyou , MaidStatus/夜伽クラス/説明/Sekkyou
760 , New , 反論封印メイド , Hanlon_ng , 無愛想タイプ専用の夜伽クラス。反論を封印され、渋々従順な状態になっています。 , MaidStatus/夜伽クラス/Hanlon_ng , MaidStatus/夜伽クラス/説明/Hanlon_ng
770 , New , 欲望開放メイド , Yokuboukaihou , 無愛想タイプ専用の夜伽クラス。特殊なお酒を飲まされて、普段理性に押し込められている欲望が開放されています。 , MaidStatus/夜伽クラス/Yokuboukaihou , MaidStatus/夜伽クラス/説明/Yokuboukaihou
1000 , New , 服従セックスメイド , Fukujyuusex , メイドを服従させ拘束し、ご主人様の意のままにする夜伽クラス。全てはご主人様の為に。 , MaidStatus/夜伽クラス/Fukujyuusex , MaidStatus/夜伽クラス/説明/Fukujyuusex
1010 , New , ハードスワップセックスメイド , Hardswapsex , メイドに対し別の男とセックスさせる夜伽クラス。他の男に弄ばれ嬲られるメイド。 , MaidStatus/夜伽クラス/Hardswapsex , MaidStatus/夜伽クラス/説明/Hardswapsex
1020 , New , 変態辱めセックスメイド , Hentaihazukasime_sec , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_sec , MaidStatus/夜伽クラス/説明/Hentaihazukasime_sec
1030 , New , ソープご奉仕セックスメイド , Sorpgohousi_sec , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_sec , MaidStatus/夜伽クラス/説明/Sorpgohousi_sec
1040 , New , アブノーマル卑猥セックスメイド , HiwaiSex_sec , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_sec , MaidStatus/夜伽クラス/説明/HiwaiSex_sec
1050 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_sec , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_sec , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_sec
1060 , New , 変態辱めセックスメイド , Hentaihazukasime_sis , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_sis , MaidStatus/夜伽クラス/説明/Hentaihazukasime_sis
1070 , New , ソープご奉仕セックスメイド , Sorpgohousi_sis , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_sis , MaidStatus/夜伽クラス/説明/Sorpgohousi_sis
1080 , New , アブノーマル卑猥セックスメイド , HiwaiSex_sis , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_sis , MaidStatus/夜伽クラス/説明/HiwaiSex_sis
1090 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_sis , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_sis , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_sis
1100 , New , バグバグオーダーメイド , BUGBUGorder , 長く続く名誉ある雑誌の為に用意された夜伽クラス。詰られたり露出したり、ド変態なプレイを楽しみましょう！ , MaidStatus/夜伽クラス/BUGBUGorder , MaidStatus/夜伽 クラス/説明/BUGBUGorder
1200 , New , マスタープレイメイド , Masterplay , メイドがご主人様となり、ご主人様を嬲り倒す夜伽クラス。『お前』と呼び方が変わり、詰られてしまいます。 , MaidStatus/夜伽クラス/Masterplay , MaidStatus/夜伽クラス/説明/Masterplay
1210 , New , 変態恥辱セックスメイド , Hentaitijyokusex , メイドにポーズをとらせたり、メイドを乱暴に責めたりする夜伽クラス。変態恥辱責めに、メイドは淫らな本性を現し…… , MaidStatus/夜伽クラス/Hentaitijyokusex , MaidStatus/夜伽クラス/説明/Hentaitijyokusex
1300 , New , 変態トイレプレイメイド , Toiletplay , トイレで変態的なプレイを行う夜伽クラス。誰かが来てしまうのではというドキドキをあなたに。 , MaidStatus/夜伽クラス/Toiletplay , MaidStatus/夜伽クラス/説明/Toiletplay
1310 , New , ファイナルクイーンメイド , RoseWhipQueen , これ以上が無いぐらい苛烈な詰りを習得する夜伽クラス。お前はもう、これで終わりよ！ , MaidStatus/夜伽クラス/RoseWhipQueen , MaidStatus/夜伽クラス/説明/RoseWhipQueen
1400 , New , 甘やかしマザーメイド , Mamameido , 普段疲れているあなたを、ママが包まれるように甘やかしてくれる夜伽クラス。ママはあなたを優しく癒してくれます。 , MaidStatus/夜伽クラス/Mamameido , MaidStatus/夜伽 クラス/説明/Mamameido
1410 , New , ご奉仕マザーメイド , MamameidoB , 普段疲れているあなたへ、ママがご奉仕してくれる夜伽クラス。ママはあなたを優しくご奉仕してくれます。 , MaidStatus/夜伽クラス/MamameidoB , MaidStatus/夜伽クラス/説明/MamameidoB
1450 , New , 淫語実況メイド , ingojikkyou , お嬢様タイプ専用の夜伽クラス。貞操観念の強いお嬢様に、卑猥な言葉を言わせ夜伽を実況させます。 , MaidStatus/夜伽クラス/ingojikkyou , MaidStatus/夜伽クラス/説明/ingojikkyou
1460 , New , 王女様メイド , oujosama , お嬢様タイプ専用の夜伽クラス。お嬢様のワガママな部分を解放させて、王女のように振る舞いご主人様が尽くすようにします。 , MaidStatus/夜伽クラス/oujosama , MaidStatus/夜伽ク ラス/説明/oujosama
1470 , New , 隷属メイド , reizoku , お嬢様タイプ専用の夜伽クラス。普段はプライドの高いお嬢様を完全に屈服させて従順にします。 , MaidStatus/夜伽クラス/reizoku , MaidStatus/夜伽クラス/説明/reizoku
1480 , New , 抵抗メイド , teikou , お嬢様タイプ専用の夜伽クラス。プライドの高いお嬢様を他の男とさせて、抵抗する様子を楽しみます。 , MaidStatus/夜伽クラス/teikou , MaidStatus/夜伽クラス/説明/teikou
1500 , New , 変態辱めセックスメイド , Hentaihazukasime_cur , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_cur , MaidStatus/夜伽クラス/説明/Hentaihazukasime_cur
1510 , New , ソープご奉仕セックスメイド , Sorpgohousi_cur , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_cur , MaidStatus/夜伽クラス/説明/Sorpgohousi_cur
1520 , New , アブノーマル卑猥セックスメイド , HiwaiSex_cur , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_cur , MaidStatus/夜伽クラス/説明/HiwaiSex_cur
1530 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_cur , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_cur , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_cur
1600 , New , マニアックプレイメイド , Maniacplay , かなり変態的な夜伽クラス。犬の真似をさせたり、お尻の穴を舐めたり……異常な性癖を楽しみましょう。 , MaidStatus/夜伽クラス/Maniacplay , MaidStatus/夜伽クラス/説明/Maniacplay
1610 , New , ハード拘束プレイメイド , Hardkousokuplay , 器具を用いてメイドを拘束する夜伽クラス。がちがちに拘束したメイドを、ご主人様のお好きなままに。 , MaidStatus/夜伽クラス/Hardkousokuplay , MaidStatus/夜伽 クラス/説明/Hardkousokuplay
1700 , New , ラブバイブメイド , Lovevibe , ラブバイブを用いたプレイを行う夜伽クラス。かわい～いラブバイブを使ってメイドを可愛がりましょう！ , MaidStatus/夜伽クラス/Lovevibe , MaidStatus/夜伽クラス/説明/Lovevibe
1800 , New , 変態ハメ撮りメイド , Hamedori , カメラを用いた変態プレイを行う夜伽クラス。ご主人様のお気に入りオナニー動画をメイドと一緒に撮影しましょう！ , MaidStatus/夜伽クラス/Hamedori , MaidStatus/夜伽クラス/説明/Hamedori
1900 , New , ソフトＳＭメイド , SoftSM , どちらかというとソフトなSMプレイを行う夜伽クラス。ご主人様と一緒に、SMプレイを楽しみましょう。 , MaidStatus/夜伽クラス/SoftSM , MaidStatus/夜伽クラス/説明/SoftSM
1910 , New , ソフトＳＭメイド , SoftSM_add , どちらかというとソフトなSMプレイを行う夜伽クラス。ご主人様と一緒に、SMプレイを楽しみましょう。 , MaidStatus/夜伽クラス/SoftSM_add , MaidStatus/夜伽クラス/説明/SoftSM_add
2000 , New , 変態辱めセックスメイド , Hentaihazukasime_mis , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_mis , MaidStatus/夜伽クラス/説明/Hentaihazukasime_mis
2010 , New , ソープご奉仕セックスメイド , Sorpgohousi_mis , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_mis , MaidStatus/夜伽クラス/説明/Sorpgohousi_mis
2020 , New , アブノーマル卑猥セックスメイド , HiwaiSex_mis , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_mis , MaidStatus/夜伽クラス/説明/HiwaiSex_mis
2030 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_mis , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_mis , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_mis
2100 , New , 調教プレイメイド , Tyokyo , メイドに対し調教を行い、被虐的な快楽を教え込む夜伽クラス。愛するメイドと、背徳的な快楽を…… , MaidStatus/夜伽クラス/Tyokyo , MaidStatus/夜伽クラス/説明/Tyokyo
2110 , New , 調教プレイメイド , Tyokyo_add , メイドに対し調教を行い、被虐的な快楽を教え込む夜伽クラス。愛するメイドと、背徳的な快楽を…… , MaidStatus/夜伽クラス/Tyokyo_add , MaidStatus/夜伽クラス/説明/Tyokyo_add
2200 , New , いじめっ子メイド , Ijime , 幼馴染タイプ専用の夜伽クラス。普段は優しい幼馴染に、いじめっ子になりきってもらってご主人様を苛めてくれます , MaidStatus/夜伽クラス/Ijime , MaidStatus/夜伽クラス/説明/Ijime
2210 , New , 愛玩奴隷メイド , Aigandorei , 幼馴染タイプ専用の夜伽クラス。幼馴染がご主人様の奴隷になりきって、酷いことをして貰います。 , MaidStatus/夜伽クラス/Aigandorei , MaidStatus/夜伽クラス/説明/Aigandorei
2220 , New , 泥酔逆行メイド , Deisuigyakkou , 幼馴染タイプ専用の夜伽クラス。普段はしっかりしている幼馴染を酔わせて、子供っぽくしてエッチをします。 , MaidStatus/夜伽クラス/Deisuigyakkou , MaidStatus/夜伽クラス/説明/Deisuigyakkou
2230 , New , 母性メイド , Bosei , 幼馴染タイプ専用の夜伽クラス。ご主人様の事が大好きな幼馴染が、母性を発揮して甘えさせてくれます。 , MaidStatus/夜伽クラス/Bosei , MaidStatus/夜伽クラス/説明/Bosei
2240 , New , 上級奉仕メイド , Jyokyuhousi , 幼馴染タイプ専用の夜伽クラス。ご奉仕が好きな幼馴染が、夜伽を重ねて上達したご奉仕を披露してくれます。 , MaidStatus/夜伽クラス/Jyokyuhousi , MaidStatus/夜伽クラス/説明/Jyokyuhousi
2300 , New , ハードSMプレイメイド , HardSM , ハードなSMプレイを行う夜伽クラス。最早暴力ともいえる行為に、メイドは何を思うのか…… , MaidStatus/夜伽クラス/HardSM , MaidStatus/夜伽クラス/説明/HardSM
2310 , New , ハードSMプレイメイド , HardSM_add , ハードなSMプレイを行う夜伽クラス。最早暴力ともいえる行為に、メイドは何を思うのか…… , MaidStatus/夜伽クラス/HardSM_add , MaidStatus/夜伽クラス/説明/HardSM_add
2400 , New , 乱交プレイメイド , RankouPlay , ご主人様以外の男性と乱交する夜伽クラス。お慕いするご主人様の目の前で、ご主人様以外と…… , MaidStatus/夜伽クラス/RankouPlay , MaidStatus/夜伽クラス/説明/RankouPlay
2410 , New , 乱交プレイメイド , RankouPlay_add , ご主人様以外の男性と乱交する夜伽クラス。お慕いするご主人様の目の前で、ご主人様以外と…… , MaidStatus/夜伽クラス/RankouPlay_add , MaidStatus/夜伽クラス/説明/RankouPlay_add
2500 , New , ラブ奉仕メイド , LoveHoushi , ラブラブでいちゃいちゃなご奉仕を行う夜伽クラス。いつもお疲れのご主人様に、最大限の愛情奉仕を…… , MaidStatus/夜伽クラス/LoveHoushi , MaidStatus/夜伽クラス/説明/LoveHoushi
2510 , New , ラブ奉仕メイド , LoveHoushi_add , ラブラブでいちゃいちゃなご奉仕を行う夜伽クラス。いつもお疲れのご主人様に、最大限の愛情奉仕を…… , MaidStatus/夜伽クラス/LoveHoushi_add , MaidStatus/夜伽クラス/説明/LoveHoushi_add
2600 , New , マニアッククイーンメイド , Fukujyuunajirare , ご主人様を拘束したり詰り倒す夜伽クラス。それもこれも全てご主人様を愛しているからなのです。 , MaidStatus/夜伽クラス/Fukujyuunajirare , MaidStatus/夜伽 クラス/説明/Fukujyuunajirare
2610 , New , マニアッククイーンメイド , Fukujyuunajirare_add , ご主人様を拘束したり詰り倒す夜伽クラス。それもこれも全てご主人様を愛しているからなのです。 , MaidStatus/夜伽クラス/Fukujyuunajirare_add , MaidStatus/夜伽クラス/説明/Fukujyuunajirare_add
2700 , New , 激甘ラブメイド , Gekiamalove , ただひたすらご主人様といちゃいちゃラブラブエッチをする夜伽クラス。ほかに望むものなど何もない！ , MaidStatus/夜伽クラス/Gekiamalove , MaidStatus/夜伽クラス/説明/Gekiamalove
2800 , New , 逆転変態プレイメイド , GyakuAnalPlay , ついに一線を越え、メイドに犯されてしまう夜伽クラス。もう戻れない、背徳的なされる快楽を…… , MaidStatus/夜伽クラス/GyakuAnalPlay , MaidStatus/夜伽クラス/説明/GyakuAnalPlay
2810 , New , 逆転変態プレイメイド , GyakuAnalPlay_add , ついに一線を越え、メイドに犯されてしまう夜伽クラス。もう戻れない、背徳的なされる快楽を…… , MaidStatus/夜伽クラス/GyakuAnalPlay_add , MaidStatus/夜伽ク ラス/説明/GyakuAnalPlay_add
2900 , New , ＡＶ撮影プレイメイド , AVsatueiPlay , ＡＶ撮影プレイを行う夜伽クラス。ご主人様とのプレイをずっと残る動画にしちゃいましょう。 , MaidStatus/夜伽クラス/AVsatueiPlay , MaidStatus/夜伽クラス/説明/AVsatueiPlay
3000 , New , エクスタシーメイド , SexSofaPlay , セックスソファーを用いてメイドとプレイを行う夜伽クラス。メイドとのイチャイチャラブラブプレイを楽しみましょう！ , MaidStatus/夜伽クラス/SexSofaPlay , MaidStatus/ 夜伽クラス/説明/SexSofaPlay
3010 , New , エクスタシーメイド , SexSofaPlay_add , セックスソファーを用いてメイドとプレイを行う夜伽クラス。メイドとのイチャイチャラブラブプレイを楽しみましょう！ , MaidStatus/夜伽クラス/SexSofaPlay_add , MaidStatus/夜伽クラス/説明/SexSofaPlay_add
3020 , New , エクスタシーメイド , SexSofaPlay_add2 , セックスソファーを用いてメイドとプレイを行う夜伽クラス。メイドとのイチャイチャラブラブプレイを楽しみましょう！ , MaidStatus/夜伽クラス/SexSofaPlay_add2 , MaidStatus/夜伽クラス/説明/SexSofaPlay_add2
3100 , New , クライシスメイド , DaiRankouPlay , ご主人様と多人数の男性とで乱交する夜伽クラス。快楽の宴に身を任せて…… , MaidStatus/夜伽クラス/DaiRankouPlay , MaidStatus/夜伽クラス/説明/DaiRankouPlay
3110 , New , クライシスメイド , DaiRankouPlay_add , ご主人様と多人数の男性とで乱交する夜伽クラス。快楽の宴に身を任せて…… , MaidStatus/夜伽クラス/DaiRankouPlay_add , MaidStatus/夜伽クラス/説明/DaiRankouPlay_add
3120 , New , クライシスメイド , DaiRankouPlay_add2 , ご主人様と多人数の男性とで乱交する夜伽クラス。快楽の宴に身を任せて…… , MaidStatus/夜伽クラス/DaiRankouPlay_add2 , MaidStatus/夜伽クラス/説明/DaiRankouPlay_add2
3200 , New , 変態辱めセックスメイド , Hentaihazukasime_chi , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/Hentaihazukasime_chi , MaidStatus/夜伽クラス/説明/Hentaihazukasime_chi
3210 , New , ソープご奉仕セックスメイド , Sorpgohousi_chi , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/Sorpgohousi_chi , MaidStatus/夜伽クラス/説明/Sorpgohousi_chi
3220 , New , アブノーマル卑猥セックスメイド , HiwaiSex_chi , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/HiwaiSex_chi , MaidStatus/夜伽クラス/説明/HiwaiSex_chi
3230 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_chi , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/LoveLoveHoushi_chi , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_chi
3300 , New , ラブバインドメイド , LBPlay , ご主人様に拘束され、逃げられない状態でセックスをする夜伽クラス。メイドを独占し、愉しみましょう。 , MaidStatus/夜伽クラス/LBPlay , MaidStatus/夜伽クラス/説明/LBPlay
3310 , New , ラブバインドメイド , LBPlay_add , ご主人様に拘束され、逃げられない状態でセックスをする夜伽クラス。メイドを独占し、愉しみましょう。 , MaidStatus/夜伽クラス/LBPlay_add , MaidStatus/夜伽クラス/説明/LBPlay_add
3320 , New , ラブバインドメイド , LBPlay_add2 , ご主人様に拘束され、逃げられない状態でセックスをする夜伽クラス。メイドを独占し、愉しみましょう。 , MaidStatus/夜伽クラス/LBPlay_add2 , MaidStatus/夜伽クラス/説明/LBPlay_add2
3400 , New , ラブイチャ卑猥メイド , LoveItyaHiwai , ほんのちょっと変態チックな体位でセックスする夜伽クラス。恥ずかしい恰好でもご主人様となら……♪ , MaidStatus/夜伽クラス/LoveItyaHiwai , MaidStatus/夜伽クラス/説明/LoveItyaHiwai
3410 , New , ラブイチャ卑猥メイド , LoveItyaHiwai_add , ほんのちょっと変態チックな体位でセックスする夜伽クラス。恥ずかしい恰好でもご主人様となら……♪ , MaidStatus/夜伽クラス/LoveItyaHiwai_add , MaidStatus/夜 伽クラス/説明/LoveItyaHiwai_add
3420 , New , ラブイチャ卑猥メイド , LoveItyaHiwai_add2 , ほんのちょっと変態チックな体位でセックスする夜伽クラス。恥ずかしい恰好でもご主人様となら……♪ , MaidStatus/夜伽クラス/LoveItyaHiwai_add2 , MaidStatus/ 夜伽クラス/説明/LoveItyaHiwai_add2
3500 , New , らぶらぶ変態プレイメイド , LoveHentaiPlay , ご主人様とのらぶらぶセックス。ちょっとだけ変態なプレイは、大好きなご主人様との夜伽のスパイス。 , MaidStatus/夜伽クラス/LoveHentaiPlay , MaidStatus/夜伽 クラス/説明/LoveHentaiPlay
3510 , New , らぶらぶ変態プレイメイド , LoveHentaiPlay_add , ご主人様とのらぶらぶセックス。ちょっとだけ変態なプレイは、大好きなご主人様との夜伽のスパイス。 , MaidStatus/夜伽クラス/LoveHentaiPlay_add , MaidStatus/夜伽クラス/説明/LoveHentaiPlay_add
3520 , New , らぶらぶ変態プレイメイド , LoveHentaiPlay_add2 , ご主人様とのらぶらぶセックス。ちょっとだけ変態なプレイは、大好きなご主人様との夜伽のスパイス。 , MaidStatus/夜伽クラス/LoveHentaiPlay_add2 , MaidStatus/夜伽クラス/説明/LoveHentaiPlay_add2
3600 , New , ハードエロティックメイド , HardEroticPlay , ご主人様とメイド、二人だけの特別なハードエロなプレイをお楽しみ頂く夜伽クラス。乱暴なのも下品なのも、全てご主人様だけに。 , MaidStatus/夜伽クラス/HardEroticPlay , MaidStatus/夜伽クラス/説明/HardEroticPlay
3700 , New , らぶらぶメイド , loveloveplus , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/loveloveplus , MaidStatus/夜伽クラス/説明/loveloveplus
3710 , New , 欲情メイド , yokujyouplus , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/yokujyouplus , MaidStatus/夜伽クラス/説明/yokujyouplus
3720 , New , らぶらぶメイド , loveloveplus_add , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/loveloveplus_add , MaidStatus/夜伽 クラス/説明/loveloveplus_add
3730 , New , 欲情メイド , yokujyouplus_add , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/yokujyouplus_add , MaidStatus/夜伽ク ラス/説明/yokujyouplus_add
3800 , New , 発情淫語メイド , Hatujyouingo , ご主人様の為に恥ずかしくて下品な言葉を言っちゃうようになっちゃった夜伽クラス。こんな言葉は、ご主人様だけ……♪ , MaidStatus/夜伽クラス/Hatujyouingo , MaidStatus/夜 伽クラス/説明/Hatujyouingo

    */