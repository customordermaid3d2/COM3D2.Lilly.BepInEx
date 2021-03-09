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
	/*
	[Error: Unity Log][eTJSError]シナリオファイル N3_PMG_0008.ks が見つかりません
	ファイル : gp002p5_PMG_main_0002.ks 行 : 26
	[Error  : Unity Log] [eTJSError]シナリオファイル N3_PMG_0008.ks が見つかりません
	ファイル : gp002p5_PMG_main_0002.ks   行 : 26
	// 이 오류좀 해결하자
	*/
	class KasizukiMainMenuPatch
	{
		// private void StartFree(Maid targetMaid, RoomData.Data targetRoom, ManData.Data targetMan, PlayData.Data targetPlay)

		[HarmonyPatch(typeof(KasizukiMainMenu), "StartSenario")]// private void StartSenario(Maid targetMaid, RoomData.Data targetRoom)
		[HarmonyPostfix]
		static void StartSenarioPre(Maid targetMaid, RoomData.Data targetRoom, KasizukiMainMenu __instance)
		{
			MyLog.Log("KasizukiMainMenu.StartSenarioPre:");

			ManData.Data data = ManData.GetData(GameMain.Instance.KasizukiMgr.GetNowManType());
			MyLog.Log(string.Format("푸 속 모드의 시나리오를 시작합니다\r\n選択状態↓\r\nメイド：{0}\r\n選択した部屋：{1}\r\n選択した男：{2}\r\n", new object[]
			{
				targetMaid.status.fullNameJpStyle,
				targetRoom.drawName,
				data.drawName
			}));
			PlayData.Data optimalConditionData = PlayData.GetOptimalConditionData(targetMaid, targetRoom, data.manType);
			string scenarioFileNameReplace = optimalConditionData.GetScenarioFileNameReplace(targetMaid);
			MyLog.Log(string.Format("호출 시나리오는「{0}」되었습니다", new object[]
			{
				scenarioFileNameReplace
			}));
			if (!GameUty.FileSystem.IsExistentFile(scenarioFileNameReplace + ".ks"))
			{
				MyLog.Log(string.Format("시나리오 파일「{0}」찾을 수 없습니다。\n 시나리오 호출하지 않습니다。", scenarioFileNameReplace + ".ks"));
				//return;
			}
			SceneKasizukiMainMenu sceneKasizukiMainMenu = __instance.parent_mgr as SceneKasizukiMainMenu;
			if (!sceneKasizukiMainMenu.moveScreen.IsExistNextLabel())
			{
				sceneKasizukiMainMenu.moveScreen.SetNextLabel(sceneKasizukiMainMenu.strScriptReturnLabel);
			}
			__instance.Finish();
		}

		[HarmonyPatch(typeof(KasizukiMainMenu), "StartFree")]   // private void StartFree(Maid targetMaid, RoomData.Data targetRoom, ManData.Data targetMan, PlayData.Data targetPlay)
																//[HarmonyFinalizer]
		[HarmonyPrefix]
		// ref Exception __exception, 없다고함
		static bool StartFreePre( Maid targetMaid, RoomData.Data targetRoom ,KasizukiMainMenu __instance)
		{   
			MyLog.LogError("KasizukiMainMenu.StartFreePre");

			return true; // Skip original?
		}
	}
}
