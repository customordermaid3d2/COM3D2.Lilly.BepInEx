using Kasizuki;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class GameUtill
    {
        static List<AbstractFreeModeItem> scneario = new List<AbstractFreeModeItem>();

        public static void GetGameInfo()
        {
            MyLog.LogInfo("=== GetGameInfo st ===");

            MyLog.LogInfo("Application.installerName : " + Application.installerName);
            MyLog.LogInfo("Application.version : " + Application.version);
            MyLog.LogInfo("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfo("Application.companyName : " + Application.companyName);

            MyLog.LogInfo();

            MyLog.LogInfo("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogInfo("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogInfo("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogInfo("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

            MyLog.LogInfo();

            try
            {
                foreach (var item in Feature.GetAllDatas(false))
                {
                    MyLog.LogMessage("Feature:", item.id, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Feature:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var item in Personal.GetAllDatas(false))
                {
                    MyLog.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Personal:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var item in Propensity.GetAllDatas(false))
                {
                    MyLog.LogMessage("Propensity:", item.id, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Propensity:" + e.ToString());
            }
            MyLog.LogInfo();

            // [Message: Lilly] YotogiClass: らぶらぶメイド: ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 : 3700 : MaidStatus / 夜伽クラス / 説明 / loveloveplus : MaidStatus / 夜伽クラス / loveloveplus : loveloveplus
            // [Message: Lilly] YotogiClass: 欲情メイド: ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… : 3710 : MaidStatus / 夜伽クラス / 説明 / yokujyouplus : MaidStatus / 夜伽クラス / yokujyouplus : yokujyouplus
            // [Message: Lilly] YotogiClass: らぶらぶメイド: ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 : 3720 : MaidStatus / 夜伽クラス / 説明 / loveloveplus_add : MaidStatus / 夜伽クラス / loveloveplus_add : loveloveplus_add
            // [Message: Lilly] YotogiClass: 欲情メイド: ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… : 3730 : MaidStatus / 夜伽クラス / 説明 / yokujyouplus_add : MaidStatus / 夜伽クラス / yokujyouplus_add : yokujyouplus_add
            // [Message: Lilly] YotogiClass: 発情淫語メイド: ご主人様の為に恥ずかしくて下品な言葉を言っちゃうようになっちゃった夜伽クラス。こんな言葉は、ご主人様だけ……♪ : 3800 : MaidStatus / 夜伽クラス / 説明 / Hatujyouingo : MaidStatus / 夜伽クラス / Hatujyouingo : Hatujyouingo
            try
            {
                foreach (var data in JobClass.GetAllDatas(false))
                {
                    MyLog.LogMessage("JobClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("JobClass:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in YotogiClass.GetAllDatas(false))
                {
                    MyLog.LogMessage("YotogiClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("YotogiClass:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in PlayData.GetAllDatas(false))
                {
                    MyLog.LogMessage("PlayData"
                        , data.ID
                        , data.drawName
                        , data.drawNameTerm
                        );
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("PlayData:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in RoomData.GetAllDatas(false))
                {
                    MyLog.LogMessage("RoomData"
                        , data.ID
                        , data.upwardRoomID
                        , data.uniqueName
                        , data.isEnableNTR
                        , data.isOnlyNTR
                        , data.drawName
                        , data.drawNameTerm
                        , data.explanatoryTextTerm
                        , data.facilityTypeID
                        , data.facilityDefaultName
                        );
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("RoomData:" + e.ToString());
            }
            MyLog.LogInfo();


            try
            {
                foreach (var data in YotogiStage.GetAllDatas(false))
                {
                    MyLog.LogMessage("YotogiStage"
                        , data.id
                        , data.uniqueName
                        , data.drawName
                        );
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("YotogiStage:" + e.ToString());
            }
            MyLog.LogInfo();

            // new 로 받아서 가져오기때문에 성능 낭비 발생
            try
            {
                if (scneario == null)
                {
                    // var newArray = Array.ConvertAll(array, item => (NewType)item);
                    // AbstractFreeModeItem  / protected static HashSet<int> GetEnabledIdList() / 에서 처리하자
                    scneario.AddRange(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Story, null).ConvertAll(item => (AbstractFreeModeItem)item));
                    scneario.AddRange(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Nitijyou, null).ConvertAll(item => (AbstractFreeModeItem)item));
                    scneario.AddRange(FreeModeItemLifeMode.CreateItemList(true).ConvertAll(item => (AbstractFreeModeItem)item));
                    scneario.AddRange(FreeModeItemVip.CreateItemVipList(null).ConvertAll(item => (AbstractFreeModeItem)item));
                    // 이걸 가져올 방법이 없어서 이렇게 씀
                    //scneario.AddRange(FreeModeItemLifeMode.CreateItemList(true));
                    //scneario.AddRange(FreeModeItemVip.CreateItemVipList(null));
                }
                foreach (var data in scneario)
                {
                    MyLog.LogMessage("scneario"
                        , data.item_id
                        , data.is_enabled
                        , data.play_file_name
                        , data.title
                        , data.text
                        , data.type
                        , MyUtill.Join(" / ", data.condition_texts)
                        , data.titleTerm
                        , data.textTerm
                        , MyUtill.Join(" , ", data.condition_text_terms)
                        );
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("scneario:" + e.ToString());
            }
            MyLog.LogInfo();

            /*
            FreeModeItemList freemode_item_list_= UTY.GetChildObject(base.root_obj, "FreeModeItemList", false).GetComponent<FreeModeItemList>(); ;

            bool isEnabled;

                isEnabled = freemode_item_list_.SetList(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Story, null).ToArray());
            

                isEnabled = freemode_item_list_.SetList(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Nitijyou, this.maid_.status).ToArray());
            

                isEnabled = freemode_item_list_.SetList(FreeModeItemLifeMode.CreateItemList(true).ToArray());
            */

            MyLog.LogInfo("=== GetGameInfo ed ===");
        }

        // Application.installerName :
        // Application.version : 1.0
        // Application.unityVersion : 5.6.4p2
        // Application.companyName : KISS
        // CharacterMgr.MaidStockMax : 999
        // CharacterMgr.ActiveMaidSlotCount : 18
        // CharacterMgr.NpcMaidCreateCount : 3
        // CharacterMgr.ActiveManSloatCount : 6
        // idMap:10 , Pure , 純真で健気な妹系
        // idMap:20 , Cool , クールで寡黙
        // idMap:30 , Pride , プライドが高く負けず嫌い
        // idMap:40 , Yandere , 病的な程一途な大和撫子
        // idMap:50 , Anesan , 母性的なお姉ちゃん
        // idMap:60 , Genki , 健康的でスポーティなボクっ娘
        // idMap:70 , Sadist , Ｍ心を刺激するドＳ女王様
        // idMap:80 , Muku , 無垢
        // idMap:90 , Majime , 真面目
        // idMap:100 , Rindere , 凜デレ
        // idMap:110 , Silent , 文学少女
        // idMap:120 , Devilish , 小悪魔
        // idMap:130 , Ladylike , おしとやか
        // idMap:140 , Secretary , メイド秘書
        // idMap:150 , Sister , ふわふわ妹
        // idMap:160 , Curtness , 無愛想
        // idMap:170 , Missy , お嬢様
        // idMap:180 , Childhood , 幼馴染
        // idMap:190 , Masochist , ド変態ドＭ
        // idMap:200 , Crafty , 腹黒
        // nameMap:[Pure, 10]
        // nameMap:[Cool, 20]
        // nameMap:[Pride, 30]
        // nameMap:[Yandere, 40]
        // nameMap:[Anesan, 50]
        // nameMap:[Genki, 60]
        // nameMap:[Sadist, 70]
        // nameMap:[Muku, 80]
        // nameMap:[Majime, 90]
        // nameMap:[Rindere, 100]
        // nameMap:[Silent, 110]
        // nameMap:[Devilish, 120]
        // nameMap:[Ladylike, 130]
        // nameMap:[Secretary, 140]
        // nameMap:[Sister, 150]
        // nameMap:[Curtness, 160]
        // nameMap:[Missy, 170]
        // nameMap:[Childhood, 180]
        // nameMap:[Masochist, 190]
        // nameMap:[Crafty, 200]
        // basicDatas:10 : 純真で健気な妹系 : 10 : C : MaidStatus/性格タイプ/Pure : Pure
        // basicDatas:20 : クールで寡黙 : 20 : B : MaidStatus/性格タイプ/Cool : Cool
        // basicDatas:30 : プライドが高く負けず嫌い : 30 : A : MaidStatus/性格タイプ/Pride : Pride
        // basicDatas:40 : 病的な程一途な大和撫子 : 40 : D : MaidStatus/性格タイプ/Yandere : Yandere
        // basicDatas:50 : 母性的なお姉ちゃん : 50 : E : MaidStatus/性格タイプ/Anesan : Anesan
        // basicDatas:60 : 健康的でスポーティなボクっ娘 : 60 : F : MaidStatus/性格タイプ/Genki : Genki
        // basicDatas:70 : Ｍ心を刺激するドＳ女王様 : 70 : G : MaidStatus/性格タイプ/Sadist : Sadist
        // basicDatas:80 : 無垢 : 80 : A1 : MaidStatus/性格タイプ/Muku : Muku
        // basicDatas:90 : 真面目 : 90 : B1 : MaidStatus/性格タイプ/Majime : Majime
        // basicDatas:100 : 凜デレ : 100 : C1 : MaidStatus/性格タイプ/Rindere : Rindere
        // basicDatas:110 : 文学少女 : 110 : D1 : MaidStatus/性格タイプ/Silent : Silent
        // basicDatas:120 : 小悪魔 : 120 : E1 : MaidStatus/性格タイプ/Devilish : Devilish
        // basicDatas:130 : おしとやか : 130 : F1 : MaidStatus/性格タイプ/Ladylike : Ladylike
        // basicDatas:140 : メイド秘書 : 140 : G1 : MaidStatus/性格タイプ/Secretary : Secretary
        // basicDatas:150 : ふわふわ妹 : 150 : H1 : MaidStatus/性格タイプ/Sister : Sister
        // basicDatas:160 : 無愛想 : 160 : J1 : MaidStatus/性格タイプ/Curtness : Curtness
        // basicDatas:170 : お嬢様 : 170 : K1 : MaidStatus/性格タイプ/Missy : Missy
        // basicDatas:180 : 幼馴染 : 180 : L1 : MaidStatus/性格タイプ/Childhood : Childhood
        // basicDatas:190 : ド変態ドＭ : 190 : M1 : MaidStatus/性格タイプ/Masochist : Masochist
        // basicDatas:200 : 腹黒 : 200 : N1 : MaidStatus/性格タイプ/Crafty : Crafty

    }
}
