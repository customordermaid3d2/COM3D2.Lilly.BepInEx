using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// ks 스크립트 관련?
    /// </summary>
    class ScriptManagerPatch
    {
        // GameMain.Instance.ScriptMgr.EvalScript("&tf['scenario_file_name'] = '" + text + "';");
        // public void EvalScript(string eval_str, TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "EvalScript", new Type[] { typeof(string) })]
        [HarmonyPostfix]
        static void EvalScript(string eval_str)
        {
            // ScriptManager.EvalScriptPost2:global.__skill_command_file.add(%['file'=>'C1_RR_CA001f.ks','label'=>'*RR2','rrlock'=>'false']);
            MyLog.LogMessageS("ScriptManager.EvalScript:" + eval_str);
        }

        // public void EvalScript(string eval_str, TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "EvalScript", new Type[] { typeof(string), typeof(TJSVariant) })]
        [HarmonyPostfix]
        static void EvalScriptPost1(string eval_str, TJSVariant result)
        {
            MyLog.LogMessageS("ScriptManager.EvalScriptPost1:" + eval_str + " , " + result.AsString());
        }


        /// <summary>
        ///  쓰는데가 없음
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="__instance"></param>
        // System.Reflection.AmbiguousMatchException: Ambiguous match for ScriptManager::ExecScriptFile($) ---> System.Reflection.AmbiguousMatchException: Ambiguous matching in method resolution
        // public void ExecScriptFile(string file_name, ref TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile", new Type[] { typeof(string) })]
        [HarmonyPostfix]
        static void ExecScriptFilePost1(string file_name, ScriptManager __instance)// ref TJSVariant result,
        {
            MyLog.LogMessageS("ScriptManager.ExecScriptFilePost1:" + file_name);
        }

        /// <summary>
        /// 쓰는데가 없음
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="result"></param>
        /// <param name="__instance"></param>
        // 정상 처리
        // public void ExecScriptFile(string file_name, ref TJSVariant result)
        // [Error  :  HarmonyX] Failed to process patch System.Void COM3D2.Lilly.Plugin.ScriptManagerPatch.ExecScriptFilePost2(System.String, ScriptManager) - Could not find method ExecScriptFile with (System.String, TJSVariant) parameters in type ScriptManager
        // [Warning: HarmonyX] AccessTools.DeclaredMethod: Could not find method for type ScriptManager and name ExecScriptFile and parameters(System.String, TJSVariant)
        // [Warning: HarmonyX] AccessTools.Method: Could not find method for type ScriptManager and name ExecScriptFile and parameters(System.String, TJSVariant)
        // [Error: HarmonyX] Failed to process patch System.Void COM3D2.Lilly.Plugin.ScriptManagerPatch.ExecScriptFilePost2(System.String, TJSVariant&, ScriptManager) - Could not find method ExecScriptFile with(System.String, TJSVariant) parameters in type ScriptManager
        [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile",
                new Type[] { typeof(string), typeof(TJSVariant) },
                new ArgumentType[] { ArgumentType.Normal, ArgumentType.Ref }
                )]
        [HarmonyPostfix]
        static void ExecScriptFilePost2(string file_name, ref TJSVariant result, ScriptManager __instance)// ref TJSVariant result,
        {
            MyLog.LogMessageS("ScriptManager.ExecScriptFilePost2:" + file_name);
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

        /// <summary>
        /// 결혼 여부 설정
        /// </summary>
        /// <param name="tjs_param"></param>
        /// <param name="result"></param>
        // private void OldTJSFuncIsMarriage(TJSVariantRef[] tjs_param, TJSVariantRef result)
        [HarmonyPatch(typeof(ScriptManager), "OldTJSFuncIsMarriage")]
        [HarmonyPostfix]
        static void OldTJSFuncIsMarriage(TJSVariantRef[] tjs_param, TJSVariantRef result)
        {
            // 이게 결혼 여부 설정
            int nMaidNo = tjs_param[0].AsInteger();
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(nMaidNo);
            MyLog.LogMessageS("OldTJSFuncIsMarriage." + MaidUtill.GetMaidFullNale(maid));
            //if (maid != null && maid.status.OldStatus != null)
            //{
            //    result.SetBool(maid.status.OldStatus.isMarriage);
            //}
            //else
            //{
            //    result.SetBool(false);
            //}
        }

        // private void OldTJSFuncIsNewWifeFlag(TJSVariantRef[] tjs_param, TJSVariantRef result)
        [HarmonyPatch(typeof(ScriptManager), "OldTJSFuncIsNewWifeFlag")]
        [HarmonyPostfix]
        static void OldTJSFuncIsNewWifeFlag(TJSVariantRef[] tjs_param, TJSVariantRef result)
        {
            int nMaidNo = tjs_param[0].AsInteger();
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(nMaidNo);
            //if (maid != null && maid.status.OldStatus != null)
            //{
            //    result.SetBool(maid.status.OldStatus.isNewWife);
            //}
            //else
            //{
            //    result.SetBool(false);
            //}
            MyLog.LogMessageS("OldTJSFuncIsNewWifeFlag." + MaidUtill.GetMaidFullNale(maid));
        }


        // private void TJSFuncSetMaidCondition(TJSVariantRef[] tjs_param, TJSVariantRef result)
        [HarmonyPatch(typeof(ScriptManager), "TJSFuncSetMaidCondition")]
        [HarmonyPostfix]
        static void TJSFuncSetMaidCondition(TJSVariantRef[] tjs_param, TJSVariantRef result)
        {
            int nMaidNo = tjs_param[0].AsInteger();
            string a = tjs_param[1].AsString();
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(nMaidNo);

            MyLog.LogMessageS("TJSFuncSetMaidCondition: " + MaidUtill.GetMaidFullNale(maid));
        }

        // 	public void TJSFuncCreateStockMaidLoopData(TJSVariantRef[] tjs_param, TJSVariantRef result)
        [HarmonyPatch(typeof(ScriptManager), "TJSFuncCreateStockMaidLoopData")]
        [HarmonyPostfix]
        static void TJSFuncCreateStockMaidLoopData(TJSVariantRef[] tjs_param, TJSVariantRef result, List<Maid> ___stock_maid_loop_list)
        {
            // List<Maid> stock_maid_loop_list = new List<Maid>();
            // //this.stock_maid_loop_list.Clear();
            // CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
            // for (int i = 0; i < characterMgr.GetStockMaidCount(); i++)
            // {
            //     //this.stock_maid_loop_list.Add(characterMgr.GetStockMaid(i));
            // }
            // List<Maid> list = stock_maid_loop_list;
            // Comparison<Maid> f__mgcache0= new Comparison<Maid>(CharacterSelectManager.SortMaidStandard);           
            // list.Sort(f__mgcache0);
            // if (result != null)
            // {
            //     result.SetInteger(stock_maid_loop_list.Count);
            // }
            foreach (var maid in ___stock_maid_loop_list)
            {
               MyLog.LogMessageS("TJSFuncCreateStockMaidLoopData: " + MaidUtill.GetMaidFullNale(maid));
            }
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
