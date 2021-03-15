using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class GameMainPatch
    {
        // public void LoadScene(string f_strSceneName)
        [HarmonyPatch(typeof(GameMain), "LoadScene")]
        [HarmonyPostfix]
        private static void LoadScenePost(string f_strSceneName, GameMain __instance)
        {
            MyLog.LogMessage("LoadScenePost:" + f_strSceneName);
            //__instance.
        }

        // public void SceneActivate(string f_strSceneName)
        [HarmonyPatch(typeof(GameMain), "SceneActivate")]
        [HarmonyPostfix]
        private static void SceneActivatePost(string f_strSceneName)
        {
            MyLog.LogMessage("SceneActivatePost:" + f_strSceneName);
        }
    }
}
