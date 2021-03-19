using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PlayerStatusPatch
{
    class StatusPatch
    {
        // public ReadOnlyDictionary<int, NightWorkState> night_works_state_dic { get; private set; }

        [HarmonyPatch(typeof(PlayerStatus.Status), "AddFlag")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void AddFlag(string flagName, int value)
        {
            MyLog.LogInfo("AddFlag", flagName, value);
        }

        [HarmonyPatch(typeof(PlayerStatus.Status), "AddHaveItem")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void AddHaveItem(string itemName)
        {
            MyLog.LogInfo("AddHaveItem", itemName);
        }

        [HarmonyPatch(typeof(PlayerStatus.Status), "AddHavePartsItem")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void AddHavePartsItem(string itemName)
        {
            MyLog.LogInfo("AddHavePartsItem", itemName);
        }

        [HarmonyPatch(typeof(PlayerStatus.Status), "AddHaveTrophy")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void AddHaveTrophy(int trophyId)
        {
            Trophy.Data data = Trophy.GetData(trophyId);
            MyLog.LogInfo("AddHavePartsItem"
                , trophyId
                ,data.name
                ,data.maidPoint
                ,data.rarity
                ,data.bonusText
                ,data.infoText                
                );
        }
    }
}
