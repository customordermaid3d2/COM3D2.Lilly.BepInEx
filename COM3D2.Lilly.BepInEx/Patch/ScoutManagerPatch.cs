﻿using HarmonyLib;
using scoutmode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class ScoutManagerPatch
    {
        // 스카우트 모드의 필요사항 (메이드 수 등등)을 해제. 원본이 하모니 못먹음
        [HarmonyPatch(typeof(ScoutManager), "isModeEnabled", MethodType.Getter)]
        [HarmonyPrefix]
        public static bool GetIsScoutMode(ref bool __result)
        {
            MyLog.LogMessageS("ScoutManager.GetIsScoutMode");
            __result = Product.type == Product.Type.JpAdult && PluginData.IsEnabled("GP001FB");
            return false;
        }
    }
}
