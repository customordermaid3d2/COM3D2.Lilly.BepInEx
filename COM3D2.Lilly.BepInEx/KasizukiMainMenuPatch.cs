using HarmonyLib;
using Kasizuki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
	// [Error: Unity Log][eTJSError]シナリオファイル N3_PMG_0008.ks が見つかりません
	// ファイル : gp002p5_PMG_main_0002.ks 行 : 26
	// 이 오류좀 해결하자
	class KasizukiMainMenuPatch
	{
		// private void StartFree(Maid targetMaid, RoomData.Data targetRoom, ManData.Data targetMan, PlayData.Data targetPlay)

		[HarmonyPatch(typeof(KasizukiMainMenu), "StartFree")]
		[HarmonyPatch(typeof(KasizukiMainMenu), "StartSenario")]
		[HarmonyPostfix]
		static void StartFreePost(Maid targetMaid, RoomData.Data targetRoom, KasizukiMainMenu __instance)
		{
			MyLog.Log("KasizukiMainMenu.StartFreePost:");
		}

		[HarmonyPatch(typeof(KasizukiMainMenu), "StartFree")]   // private void StartFree(Maid targetMaid, RoomData.Data targetRoom, ManData.Data targetMan, PlayData.Data targetPlay)
		[HarmonyPatch(typeof(KasizukiMainMenu), "StartSenario")]// private void StartSenario(Maid targetMaid, RoomData.Data targetRoom)
																//[HarmonyFinalizer]
		[HarmonyPrefix]
		// ref Exception __exception, 없다고함
		static bool StartFreePre( Maid targetMaid, RoomData.Data targetRoom ,KasizukiMainMenu __instance)
		{   
			MyLog.LogError("KasizukiMainMenu.StartFreePre");
			//MyLog.LogError("KasizukiMainMenu.StartFreePre:" + __exception.ToString());
			//__exception = null;

			ManData.Data data = ManData.GetData(GameMain.Instance.KasizukiMgr.GetNowManType());
			Debug.LogFormat("傅きモードのシナリオを開始します\r\n選択状態↓\r\nメイド：{0}\r\n選択した部屋：{1}\r\n選択した男：{2}\r\n", new object[]
			{
				targetMaid.status.fullNameJpStyle,
				targetRoom.drawName,
				data.drawName
			});
			PlayData.Data optimalConditionData = PlayData.GetOptimalConditionData(targetMaid, targetRoom, data.manType);
			string scenarioFileNameReplace = optimalConditionData.GetScenarioFileNameReplace(targetMaid);
			Debug.LogFormat("呼び出すシナリオは「{0}」になりました", new object[]
			{
				scenarioFileNameReplace
			});
			if (!GameUty.FileSystem.IsExistentFile(scenarioFileNameReplace + ".ks"))
			{
				NDebug.Warning(string.Format("シナリオファイル「{0}」が見つかりません。\nシナリオ呼び出しは行いません。", scenarioFileNameReplace + ".ks"));
				//return;
				scenarioFileNameReplace = Regex.Replace(scenarioFileNameReplace, @"[a-z0-9]*_", "a_");
			}
			SceneKasizukiMainMenu sceneKasizukiMainMenu = __instance.parent_mgr as SceneKasizukiMainMenu;
			if (!sceneKasizukiMainMenu.moveScreen.IsExistNextLabel())
			{
				sceneKasizukiMainMenu.moveScreen.SetNextLabel(sceneKasizukiMainMenu.strScriptReturnLabel);
			}
			__instance.Finish();

			return false; // Skip original
		}
	}
}
