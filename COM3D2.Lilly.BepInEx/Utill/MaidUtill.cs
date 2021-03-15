﻿using MaidStatus;
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
            MyLog.LogInfo("Application.installerName : " + Application.installerName);
            MyLog.LogInfo("Application.version : " + Application.version);
            MyLog.LogInfo("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfo("Application.companyName : " + Application.companyName);

            MyLog.LogInfo("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogInfo("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogInfo("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogInfo("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

            try
            {
                foreach (KeyValuePair<int, KeyValuePair<string, string>> i in PersonalPatch.commonIdManager.idMap)
                {
                    MyLog.LogMessage("idMap:" + i.Key + " , " + i.Value.Key + " , " + i.Value.Value);
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("idMap:" + e.ToString());
            }

            try
            {
                foreach (var item in PersonalPatch.commonIdManager.nameMap)
                {
                    MyLog.LogMessage("nameMap:" + item);
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("nameMap:" + e.ToString());
            }

            try
            {
                foreach (var item in PersonalPatch.basicDatas)
                {
                    MyLog.LogMessage("basicDatas:" + item.Key + " : " + item.Value.drawName + " : " + item.Value.id + " : " + item.Value.replaceText + " : " + item.Value.termName + " : " + item.Value.uniqueName);//
                                                                                                                                                                                                                     // competitiveMotionFileVictory  경쟁적인 모션 파일 승리
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("basicDatas:" + e.ToString());
            }

            try
            {
                foreach (var item in ProfileCtrlPatch.m_dicPersonal)
                {
                    MyLog.LogMessage("m_dicPersonal:" + item.Key + " : " + item.Value.drawName + " : " + item.Value.id + " : " + item.Value.replaceText + " : " + item.Value.termName + " : " + item.Value.uniqueName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("m_dicPersonal:" + e.ToString());
            }

            try
            {
                foreach (var item in YotogiClass.GetAllDatas(false))
                {
                    MyLog.LogMessage("YotogiClass:"  +   item.id + " : " + item.uniqueName + " : " +  item.termExplanatoryText + " : " + item.drawName + " : " + item.explanatoryText);// + " : " +  item.termName
                    
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("m_dicPersonal:" + e.ToString());
            }

            // [Message: Lilly] YotogiClass: らぶらぶメイド: ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 : 3700 : MaidStatus / 夜伽クラス / 説明 / loveloveplus : MaidStatus / 夜伽クラス / loveloveplus : loveloveplus
            // [Message: Lilly] YotogiClass: 欲情メイド: ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… : 3710 : MaidStatus / 夜伽クラス / 説明 / yokujyouplus : MaidStatus / 夜伽クラス / yokujyouplus : yokujyouplus
            // [Message: Lilly] YotogiClass: らぶらぶメイド: ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 : 3720 : MaidStatus / 夜伽クラス / 説明 / loveloveplus_add : MaidStatus / 夜伽クラス / loveloveplus_add : loveloveplus_add
            // [Message: Lilly] YotogiClass: 欲情メイド: ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… : 3730 : MaidStatus / 夜伽クラス / 説明 / yokujyouplus_add : MaidStatus / 夜伽クラス / yokujyouplus_add : yokujyouplus_add
            // [Message: Lilly] YotogiClass: 発情淫語メイド: ご主人様の為に恥ずかしくて下品な言葉を言っちゃうようになっちゃった夜伽クラス。こんな言葉は、ご主人様だけ……♪ : 3800 : MaidStatus / 夜伽クラス / 説明 / Hatujyouingo : MaidStatus / 夜伽クラス / Hatujyouingo : Hatujyouingo

            foreach (KeyValuePair<int, ClassUnit> item in ClassChangePanelPatch.job_class_unit_dic_)
            {
                ClassUnit value = item.Value;
                JobClass.Data data = JobClass.GetData(value.maid_class_type);
                MyLog.LogMessage("JobClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
            }

            // 20 , New , コンシェルジュメイド , Concierge , ベッドメイクやドア係など、ホテルで働く際に必要な技術の習熟を表すジョブメイドクラス。ホテルを建設する事で習得可能。特別な記念日のフォローなども行います。 , MaidStatus / ジョブクラス / Concierge , MaidStatus / ジョブクラス / 説明 / Concierge
            // 60 , New , セラピストメイド , Therapist , 施術全般や健康に関わる事など、リフレで働く際に必要な技術の習熟を表すジョブメイドクラス。リフレを建設する事で習得可能。お客様の身も心も癒します。 , MaidStatus / ジョブク ラス / Therapist , MaidStatus / ジョブクラス / 説明 / Therapist
            // 100 , New , ナイトメイド , Night , 女王様やM嬢など、SMクラブで働く際に必要な技術の習熟を表すジョブメイドクラス。SMクラブを建設する事で習得可能。背徳的でアブノーマルな奉仕をお客様に。 , MaidStatus / ジョブクラス / Night , MaidStatus / ジョブクラス / 説明 / Night

            foreach (KeyValuePair<int, ClassUnit> item in ClassChangePanelPatch.yotogi_class_unit_dic_)
            {
                ClassUnit value = item.Value;
                YotogiClass.Data data = YotogiClass.GetData(value.yotogi_class_type);
                MyLog.LogMessage("YotogiClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
            }

            // 39 , Old , ヒーリングメイド , Healing , メイドから癒やしと快楽を与えられたいときの夜伽クラス。時には優しく、時には厳しいご奉仕を貴方へ。 , MaidStatus / 夜伽クラス / Healing , MaidStatus / 夜伽クラス / 説明 / Healing
            // 40 , Old , オビディエントメイド , Obedient , 嗜虐的な欲望、その全てをメイドに与えたいときの夜伽クラス。もっと拘束して、もっと激しく苛め倒す。 , MaidStatus / 夜伽クラス / Obedient , MaidStatus / 夜伽クラス / 説明 / Obedient
            // 42 , New , 変態辱めセックスメイド , Hentaihazukasime , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus / 夜伽クラス / Hentaihazukasime , MaidStatus / 夜伽クラス / 説明 / Hentaihazukasime
            // 43 , New , ソープご奉仕セックスメイド , Sorpgohousi , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus / 夜伽クラス / Sorpgohousi , MaidStatus / 夜伽クラス / 説明 / Sorpgohousi
            // 44 , Share , 変態辱めセックスメイド , Hentaihazukasime_old , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して……おしっこも出しちゃいます。 , MaidStatus / 夜伽クラス / Hentaihazukasime_old , MaidStatus / 夜伽クラス / 説明 / Hentaihazukasime_old
            // 45 , Share , ソープご奉仕セックスメイド , Sorpgohousi_old , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ……？ , MaidStatus / 夜伽クラス / Sorpgohousi_old , MaidStatus / 夜伽クラス / 説明 / Sorpgohousi_old





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
