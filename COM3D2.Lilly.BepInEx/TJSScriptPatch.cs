using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class TJSScriptPatch
    {
        // public void EvalScript(string eval_str)
        [HarmonyPatch(typeof(TJSScript), "EvalScript",new Type[] { typeof(string) })]
        [HarmonyPatch(typeof(TJSScript), "EvalScript",new Type[] { typeof(string) , typeof(TJSVariant) })]
        [HarmonyPostfix]
        private static void EvalScriptPost1(string eval_str) // string __m_BGMName 못가져옴
        {
            MyLog.Log("TJSScript.EvalScriptPost1:" + eval_str);
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }

        [HarmonyPatch(typeof(TJSScript), "EvalScript",new Type[] { typeof(AFileBase) })]
        [HarmonyPostfix]
        private static void EvalScriptPost2(AFileBase file) // string __m_BGMName 못가져옴
        {
            MyLog.Log("TJSScript.EvalScriptPost2:" + file.ToString());
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }
    }
}
