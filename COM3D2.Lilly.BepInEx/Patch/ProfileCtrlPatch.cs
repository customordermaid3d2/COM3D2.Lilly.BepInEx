using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 성격 정보가 있나?
    /// MaidUtill.GetGameInfo 용으로 남겨둠
    /// </summary>
    class ProfileCtrlPatch
    {
        /// <summary>
        ///  초기화
        /// </summary>
        /// <param name="__instance"></param>
        //[HarmonyPatch(typeof(ProfileCtrl), "Init")]
        //[HarmonyPostfix]
        private static void Init(ProfileCtrl __instance, Status ___m_maidStatus) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("ProfileCtrl.Init:");
        }

        //[HarmonyPatch(typeof(ProfileCtrl), "SetEnableInput")]
        //[HarmonyPrefix]
        public static void OnSetEnableInput(ref bool enabledInput)
        {
            enabledInput = true;
        }

        public static ProfileCtrl instance;
        public static Dictionary<string, Personal.Data> m_dicPersonal;

        /// <summary>
        /// 성격 관련?
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="selectValue"></param>
        /// <param name="___m_dicPersonal"></param>
        /// <param name="___m_maidStatus"></param>
        [HarmonyPatch(typeof(ProfileCtrl), "SetPersonal")]
        [HarmonyPrefix]
        public static void SetPersonal(ProfileCtrl __instance, string selectValue, Dictionary<string, Personal.Data> ___m_dicPersonal, Status ___m_maidStatus)
        {
            instance = __instance;
            m_dicPersonal = ___m_dicPersonal;

            Personal.Data data;
            ___m_dicPersonal.TryGetValue(selectValue, out data);

            StringBuilder s = new StringBuilder();

            s.Append(" , " + selectValue);
            s.Append(" , " + data.id);
            s.Append(" , " + data.replaceText);
            s.Append(" , " + data.uniqueName);
            s.Append(" , " + data.drawName);

            MyLog.LogMessage("SetPersonal: "+s.ToString());


            //Personal.Data data;
            //if (___m_dicPersonal.TryGetValue(selectValue, out data))
            //{
            //    ___m_maidStatus.SetPersonal(data);
            //    if (SceneEdit.Instance != null && (SceneEdit.Instance.modeType == SceneEdit.ModeType.OriginalChara || SceneEdit.Instance.modeType == SceneEdit.ModeType.MainChara || SceneEdit.Instance.modeType == SceneEdit.ModeType.ScoutChara))
            //    {
            //        ___m_maidStatus.additionalRelation = AdditionalRelation.Vigilance;
            //        if ((data.id == 190 || data.id == 200) && ___m_maidStatus.additionalRelation == AdditionalRelation.Vigilance)
            //        {
            //            ___m_maidStatus.additionalRelation = AdditionalRelation.Null;
            //        }
            //    }
            //    Debug.Log(string.Concat(new object[]
            //    {
            //    "保存された性格:",
            //    this.m_maidStatus.personal,
            //    " = ",
            //    selectValue
            //    }));
            //}
        }
    }
}
