using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.BepInEx
{
    class GameMainPatch
    {
        // public void LoadScene(string f_strSceneName)
        [HarmonyPatch(typeof(GameMain), "LoadScene")]
        [HarmonyPostfix]
        private static void LoadScenePost(string f_strSceneName)
        {
            MyLog.Log("LoadScenePost:" + f_strSceneName);
        }

        // public void SceneActivate(string f_strSceneName)
        [HarmonyPatch(typeof(GameMain), "SceneActivate")]
        [HarmonyPostfix]
        private static void SceneActivatePost(string f_strSceneName)
        {
            MyLog.Log("SceneActivatePost:" + f_strSceneName);
        }
    }
}
