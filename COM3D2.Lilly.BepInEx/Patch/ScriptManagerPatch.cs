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
            MyLog.LogMessageS("ScriptManager.ExecScriptFilePost1:" + file_name);
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
            MyLog.LogMessageS("ScriptManager.ExecScriptFilePost2:" + file_name);
        }        

        // public void EvalScript(string eval_str, TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "EvalScript", new Type[] { typeof(string), typeof(TJSVariant) } )]
        [HarmonyPostfix]
        static void EvalScriptPost1(string eval_str, TJSVariant result)
        {
            MyLog.LogMessageS("ScriptManager.EvalScriptPost1:" + eval_str + " , " + result.AsString());
            
        }

        // public void EvalScript(string eval_str, TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "EvalScript", new Type[] { typeof(string) } )]
        [HarmonyPostfix]
        static void EvalScriptPost2(string eval_str)
        {
            // ScriptManager.EvalScriptPost2:global.__skill_command_file.add(%['file'=>'C1_RR_CA001f.ks','label'=>'*RR2','rrlock'=>'false']);
            MyLog.LogMessageS("ScriptManager.EvalScriptPost2:" + eval_str );
        }

        // private void TJSFuncLearningYotogiSkill(TJSVariantRef[] tjs_param, TJSVariantRef result)
        [HarmonyPatch(typeof(ScriptManager), "TJSFuncLearningYotogiSkill" )]
        [HarmonyPostfix]
        static void TJSFuncLearningYotogiSkill(TJSVariantRef[] tjs_param, TJSVariantRef result)
        {
            MyLog.LogMessageS("ScriptManager.TJSFuncLearningYotogiSkill:"  );
            
            //NDebug.Assert(tjs_param.Length == 2, "LearningYotogiSkill args count error.");
            if (tjs_param.Length == 2)
            {
                MyLog.LogErrorS("LearningYotogiSkill args count error.");
            }
            // NDebug.Assert(tjs_param[0].type == TJSVariantRef.Type.tvtInteger && tjs_param[1].type == TJSVariantRef.Type.tvtInteger, "error.");
            if (tjs_param[0].type == TJSVariantRef.Type.tvtInteger && tjs_param[1].type == TJSVariantRef.Type.tvtInteger)
            {
                MyLog.LogErrorS("error.");
            }            
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(tjs_param[0].AsInteger());
            if (maid == null)
            {
                MyLog.LogErrorS("maid is null. LearningYotogiSkill");
                return;
            }
            // int skillId = tjs_param[1].AsInteger();
            // maid.status.yotogiSkill.Add(skillId);
        }
        
        // 메이드 새로 만들때?
        // TJSFuncCreateNewMaid
        // private void TJSFuncCreateNewMaid(TJSVariantRef[] tjs_param, TJSVariantRef result)
        //[HarmonyPatch(typeof(ScriptManager), "TJSFuncCreateNewMaid" )]
        //[HarmonyPostfix]
        //static void TJSFuncCreateNewMaid()
        //{
        //    MyLog.LogMessageS("ScriptManager.TJSFuncCreateNewMaid."  );
        //
        //    MaidStatus.MaidStatusAll();
        //}
    }

}
