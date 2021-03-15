using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    class MaidUtill
    {
        // ProfileCtrl component = gameObject2.GetComponent<ProfileCtrl>();
        // Status status = (Status)typeof(ProfileCtrl).GetField("m_maidStatus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(component);
        // Dictionary<string, Personal.Data> dictionary = (Dictionary<string, Personal.Data>)typeof(ProfileCtrl).GetField("m_dicPersonal", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

        /*
        Maid.Status
            Status() / 
        		Feature.CreateData();
			    Propensity.CreateData();
			    YotogiClass.CreateData();
			    JobClass.CreateData();
			    Personal.CreateData();
			    SubMaid.CreateData();
			    PersonalEventBlocker.CreateData();
                Personal.CreateData();        
                    Personal.commonIdManager = new CsvCommonIdManager("maid_status_personal", "性格", CsvCommonIdManager.Type.IdAndUniqueName, null);
                Personal.Data
                    id
                    uniqueName
                    termName
                this.SetPersonal(Personal.GetData("Muku"));
        */
        MaidUtill()
        {

        }

        static Maid maid;

        public static string GetMaidFullNale(Maid maid)
        {
            StringBuilder s = new StringBuilder();
            if (maid.status !=null)
            {
                s.Append(        maid.status.firstName);
                s.Append(" , " + maid.status.lastName);
                if (maid.status.personal!=null)
                {
                    s.Append(" , " + maid.status.personal.id);
                    s.Append(" , " + maid.status.personal.replaceText);
                    s.Append(" , " + maid.status.personal.uniqueName );
                    s.Append(" , " + maid.status.personal.drawName   );
                }

            }
            return s.ToString();
        }

        public static void GetGameInfo()
        {
            MyLog.LogInfoS("Application.installerName : " + Application.installerName);
            MyLog.LogInfoS("Application.version : " + Application.version);
            MyLog.LogInfoS("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfoS("Application.companyName : " + Application.companyName);

            MyLog.LogInfoS("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogInfoS("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogInfoS("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogInfoS("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

            try
            {
                foreach (KeyValuePair<int, KeyValuePair<string, string>> i in PersonalPatch.commonIdManager.idMap)
                {
                    MyLog.LogMessageS("idMap:" + i.Key + " , " + i.Value.Key + " , " + i.Value.Value);
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessageS("idMap:" + e.ToString());
            }

            try
            {
                foreach (var item in PersonalPatch.commonIdManager.nameMap)
                {
                    MyLog.LogMessageS("nameMap:" + item);
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessageS("nameMap:" + e.ToString());
            }

            try
            {
                foreach (var item in PersonalPatch.basicDatas)
                {
                    MyLog.LogMessageS("basicDatas:" + item.Key + " : " + item.Value.drawName + " : " + item.Value.id + " : " + item.Value.replaceText + " : " + item.Value.termName + " : " + item.Value.uniqueName);//
                                                                                                                                                                                                                     // competitiveMotionFileVictory  경쟁적인 모션 파일 승리
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessageS("basicDatas:" + e.ToString());
            }

            try
            {
                foreach (var item in ProfileCtrlPatch.m_dicPersonal)
                {
                    MyLog.LogMessageS("m_dicPersonal:" + item.Key + " : " + item.Value.drawName + " : " + item.Value.id + " : " + item.Value.replaceText + " : " + item.Value.termName + " : " + item.Value.uniqueName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessageS("m_dicPersonal:" + e.ToString());
            }

            try
            {
                foreach (var item in YotogiClass.GetAllDatas(false))
                {
                    MyLog.LogMessageS("YotogiClass:"  +   item.id + " : " + item.uniqueName + " : " +  item.termExplanatoryText + " : " + item.drawName + " : " + item.explanatoryText);// + " : " +  item.termName
                    
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessageS("m_dicPersonal:" + e.ToString());
            }

            // [Message: Lilly] YotogiClass: らぶらぶメイド: ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 : 3700 : MaidStatus / 夜伽クラス / 説明 / loveloveplus : MaidStatus / 夜伽クラス / loveloveplus : loveloveplus
            // [Message: Lilly] YotogiClass: 欲情メイド: ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… : 3710 : MaidStatus / 夜伽クラス / 説明 / yokujyouplus : MaidStatus / 夜伽クラス / yokujyouplus : yokujyouplus
            // [Message: Lilly] YotogiClass: らぶらぶメイド: ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 : 3720 : MaidStatus / 夜伽クラス / 説明 / loveloveplus_add : MaidStatus / 夜伽クラス / loveloveplus_add : loveloveplus_add
            // [Message: Lilly] YotogiClass: 欲情メイド: ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… : 3730 : MaidStatus / 夜伽クラス / 説明 / yokujyouplus_add : MaidStatus / 夜伽クラス / yokujyouplus_add : yokujyouplus_add
            // [Message: Lilly] YotogiClass: 発情淫語メイド: ご主人様の為に恥ずかしくて下品な言葉を言っちゃうようになっちゃった夜伽クラス。こんな言葉は、ご主人様だけ……♪ : 3800 : MaidStatus / 夜伽クラス / 説明 / Hatujyouingo : MaidStatus / 夜伽クラス / Hatujyouingo : Hatujyouingo



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
