using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class ScriptManagerPatch
    {        
        // System.Reflection.AmbiguousMatchException: Ambiguous match for ScriptManager::ExecScriptFile($) ---> System.Reflection.AmbiguousMatchException: Ambiguous matching in method resolution
        // public void ExecScriptFile(string file_name, ref TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile", new Type[]{ typeof(string)})]
        [HarmonyPostfix]
        static void ExecScriptFilePost1(string file_name,  ScriptManager __instance)// ref TJSVariant result,
        {
            MyLog.Log("ScriptManager.ExecScriptFilePost1:" + file_name);
        }

        // 정상 처리
        // public void ExecScriptFile(string file_name, ref TJSVariant result)
        // [Error  :  HarmonyX] Failed to process patch System.Void COM3D2.Lilly.Plugin.ScriptManagerPatch.ExecScriptFilePost2(System.String, ScriptManager) - Could not find method ExecScriptFile with (System.String, TJSVariant) parameters in type ScriptManager
        // [Warning: HarmonyX] AccessTools.DeclaredMethod: Could not find method for type ScriptManager and name ExecScriptFile and parameters(System.String, TJSVariant)
        // [Warning: HarmonyX] AccessTools.Method: Could not find method for type ScriptManager and name ExecScriptFile and parameters(System.String, TJSVariant)
        // [Error: HarmonyX] Failed to process patch System.Void COM3D2.Lilly.Plugin.ScriptManagerPatch.ExecScriptFilePost2(System.String, TJSVariant&, ScriptManager) - Could not find method ExecScriptFile with(System.String, TJSVariant) parameters in type ScriptManager
    [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile", 
            new Type[] { typeof(string), typeof(TJSVariant) },
            new ArgumentType[] {ArgumentType.Normal, ArgumentType.Ref}
            )]
        [HarmonyPostfix]
        static void ExecScriptFilePost2(string file_name, ref TJSVariant result,  ScriptManager __instance)// ref TJSVariant result,
        {
            MyLog.Log("ScriptManager.ExecScriptFilePost2:" + file_name);
        }
    }
}
