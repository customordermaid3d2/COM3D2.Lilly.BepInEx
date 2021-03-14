using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

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

            foreach (KeyValuePair<int, KeyValuePair<string, string>> i in PersonalPatch.commonIdManager.idMap)
            {
                MyLog.LogMessageS("idMap:" + i.Key + " , " + i.Value.Key + " , " + i.Value.Value);
            }

            foreach (var item in PersonalPatch.commonIdManager.nameMap)
            {
                MyLog.LogMessageS("nameMap:" + item);
            }

            foreach (var item in PersonalPatch.basicDatas)
            {
                MyLog.LogMessageS("basicDatas:" + item.Key + " : " + item.Value.drawName + " : " + item.Value.id + " : " + item.Value.replaceText + " : " + item.Value.termName + " : " + item.Value.uniqueName);//
                                                                                                                                                                                                                 // competitiveMotionFileVictory  경쟁적인 모션 파일 승리
            }


        }

        // 10 , Pure , 純真で健気な妹系
        // 20 , Cool , クールで寡黙
        // 30 , Pride , プライドが高く負けず嫌い
        // 40 , Yandere , 病的な程一途な大和撫子
        // 50 , Anesan , 母性的なお姉ちゃん
        // 60 , Genki , 健康的でスポーティなボクっ娘
        // 70 , Sadist , Ｍ心を刺激するドＳ女王様
        // 80 , Muku , 無垢
        // 90 , Majime , 真面目
        // 100 , Rindere , 凜デレ
        // 110 , Silent , 文学少女
        // 120 , Devilish , 小悪魔
        // 130 , Ladylike , おしとやか
        // 140 , Secretary , メイド秘書
        // 150 , Sister , ふわふわ妹
        // 160 , Curtness , 無愛想
        // 170 , Missy , お嬢様
        // 180 , Childhood , 幼馴染
        // 190 , Masochist , ド変態ドＭ
        // 200 , Crafty , 腹黒

        // [Pure, 10]
        // [Cool, 20]
        // [Pride, 30]
        // [Yandere, 40]
        // [Anesan, 50]
        // [Genki, 60]
        // [Sadist, 70]
        // [Muku, 80]
        // [Majime, 90]
        // [Rindere, 100]
        // [Silent, 110]
        // [Devilish, 120]
        // [Ladylike, 130]
        // [Secretary, 140]
        // [Sister, 150]
        // [Curtness, 160]
        // [Missy, 170]
        // [Childhood, 180]
        // [Masochist, 190]
        // [Crafty, 200]

        // 10 : 純真で健気な妹系 : 10 : C : MaidStatus/性格タイプ/Pure : Pure
        // 20 : クールで寡黙 : 20 : B : MaidStatus/性格タイプ/Cool : Cool
        // 30 : プライドが高く負けず嫌い : 30 : A : MaidStatus/性格タイプ/Pride : Pride
        // 40 : 病的な程一途な大和撫子 : 40 : D : MaidStatus/性格タイプ/Yandere : Yandere
        // 50 : 母性的なお姉ちゃん : 50 : E : MaidStatus/性格タイプ/Anesan : Anesan
        // 60 : 健康的でスポーティなボクっ娘 : 60 : F : MaidStatus/性格タイプ/Genki : Genki
        // 70 : Ｍ心を刺激するドＳ女王様 : 70 : G : MaidStatus/性格タイプ/Sadist : Sadist
        // 80 : 無垢 : 80 : A1 : MaidStatus/性格タイプ/Muku : Muku
        // 90 : 真面目 : 90 : B1 : MaidStatus/性格タイプ/Majime : Majime
        // 100 : 凜デレ : 100 : C1 : MaidStatus/性格タイプ/Rindere : Rindere
        // 110 : 文学少女 : 110 : D1 : MaidStatus/性格タイプ/Silent : Silent
        // 120 : 小悪魔 : 120 : E1 : MaidStatus/性格タイプ/Devilish : Devilish
        // 130 : おしとやか : 130 : F1 : MaidStatus/性格タイプ/Ladylike : Ladylike
        // 140 : メイド秘書 : 140 : G1 : MaidStatus/性格タイプ/Secretary : Secretary
        // 150 : ふわふわ妹 : 150 : H1 : MaidStatus/性格タイプ/Sister : Sister
        // 160 : 無愛想 : 160 : J1 : MaidStatus/性格タイプ/Curtness : Curtness
        // 170 : お嬢様 : 170 : K1 : MaidStatus/性格タイプ/Missy : Missy
        // 180 : 幼馴染 : 180 : L1 : MaidStatus/性格タイプ/Childhood : Childhood
        // 190 : ド変態ドＭ : 190 : M1 : MaidStatus/性格タイプ/Masochist : Masochist
        // 200 : 腹黒 : 200 : N1 : MaidStatus/性格タイプ/Crafty : Crafty
    }
}
