using COM3D2.Lilly.Plugin;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    class SkillPatch
    {
        [HarmonyPatch(typeof(Skill.Data), "IsExecStage", new Type[] { typeof(YotogiStage.Data) })]
        [HarmonyPostfix]
        static void IsExecStagePost(YotogiStage.Data stageData, ref bool __result, Skill.Data __instance)
        {   // ■ Skill.Data.IsExecStagePostBGM022.ogg , bigsight_night , System.String[] , SceneYotogi/背景タイプ/bigsight_night , bigsight_night
           // MyLog.LogMessageS("Skill.Data.IsExecStagePost:" + stageData.bgmFileName 
           //     + " , " + stageData.drawName 
           //     + " , " + stageData.prefabName 
           //     + " , " + stageData.termName 
           //     + " , " + stageData.uniqueName);
        }
    }
}
