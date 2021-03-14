using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
	/// <summary>
	/// 메이드 목록에서 스테이터스 버튼 클릭시 나오는 화면 관련?
	/// </summary>
    class StatusMgrPatch
    {
		/// <summary>
		/// 스테이터스 버튼 클릭시 나오는 화면
		/// </summary>
		/// <param name="___m_maid"></param>
		[HarmonyPatch(typeof(StatusMgr), "OpenStatusPanel")]
		[HarmonyPrefix]
		static void OpenStatusPanel(StatusMgr __instance, Maid ___m_maid)
		{
			if (___m_maid == null)
			{
				MyLog.LogWarningS("StatusMgr.OpenStatusPanel:null");
				return;
			}
            MyLog.LogMessageS("StatusMgr.OpenStatusPanel" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);
			//MaidStatusUtill.SetMaidStatus(___m_maid);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="maid"></param>
		[HarmonyPatch(typeof(StatusMgr), "UpdateMaidStatus")]
		[HarmonyPrefix]
		static void UpdateMaidStatusPre(Maid maid)
		{
			if (maid == null)
			{
				MyLog.LogErrorS("StatusMgr.UpdateMaidStatusPre:null");
				return;
			}
			MyLog.LogMessageS("StatusMgr.UpdateMaidStatusPre"+ maid.status.charaName.name1 +" , "+ maid.status.charaName.name2);
			//MaidStatusUtill.SetMaidStatus(maid);
		}

		/// <summary>
		/// 원본 코드 그대로 복사해본거. 버그 터짐
		/// </summary>
		/// <param name="___m_maid"></param>
		/// <param name="__result"></param>
		/// <returns></returns>
        [HarmonyPatch(typeof(StatusMgr), "LoadData")]
        [HarmonyPrefix]
        static void LoadDataPre(ref Maid ___m_maid)//, StatusCtrl.Status __result
		{
			if (___m_maid == null)
			{
				MyLog.LogErrorS("StatusMgr.LoadDataPre:null");
				return;
			}
			MyLog.LogMessageS("StatusMgr.LoadDataPre" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);
			//MaidStatusUtill.SetMaidStatus(___m_maid);
		}
    }
}
