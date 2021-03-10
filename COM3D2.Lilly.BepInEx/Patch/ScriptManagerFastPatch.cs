using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class ScriptManagerFastPatch
    {
        // public bool LoadScript(string f_strFileName)
        [HarmonyPatch(typeof(ScriptManagerFast.KagParserFast), "LoadScript", new Type[] { typeof(string) })]
        [HarmonyPostfix]
        private static void LoadScriptPost(string f_strFileName) 
        {
            MyLog.LogMessageS("ScriptManagerFast.LoadScriptPost:" + f_strFileName);
        }
    }
}
