using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class FreeModeItemLifeModePatch
    {
        // FreeModeItemLifeMode


        [HarmonyPatch(typeof(FreeModeItemLifeMode), "is_enabled", MethodType.Getter)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled(FreeModeItemLifeMode __instance, bool __result)
        {
            __result = true;
            AbstractFreeModeItemPatch.OutMsg("FreeModeItemLifeMode.get_is_enabled");
			return;

			if (GameMain.Instance.LifeModeMgr.GetScenarioExecuteCount(__instance.m_LifeModeData.ID) <= 0)
			{
				//return false;
			}
			List<Maid> lifeModeAllMaidList = GameMain.Instance.LifeModeMgr.lifeModeAllMaidList;
			using (Dictionary<int, string>.Enumerator enumerator = __instance.m_LifeModeData.dataMaidPersonalUniqueNameAndActiveSlotDic.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, string> personalSlotPair = enumerator.Current;
					if (!lifeModeAllMaidList.Any((Maid maid) => maid.status.personal.uniqueName == personalSlotPair.Value))
					{
						//return false;
					}
				}
			}
			//return true;

		}

    }
}
