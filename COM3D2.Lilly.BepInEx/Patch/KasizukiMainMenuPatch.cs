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
			MyLog.LogMessageS("KasizukiMainMenu.StartSenarioPre:");
		}

		[HarmonyPatch(typeof(KasizukiMainMenu), "StartFree")]   // private void StartFree(Maid targetMaid, RoomData.Data targetRoom, ManData.Data targetMan, PlayData.Data targetPlay)
																//[HarmonyFinalizer]
		[HarmonyPrefix]
		// ref Exception __exception, 없다고함
		static void StartFreePre( Maid targetMaid, RoomData.Data targetRoom ,KasizukiMainMenu __instance)
		{   
			MyLog.LogErrorS("KasizukiMainMenu.StartFreePre");

		}
	}
}
