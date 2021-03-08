using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.BepInEx
{
    // 파일 저장 관련
    class SaveAndLoadCtrlPatch
    {
        // public void LoadData()
        [HarmonyPatch(typeof(SaveAndLoadCtrl), "LoadData")]
        [HarmonyPrefix]// 나중에 __m_loadDataNo 지워짐
        private static void LoadDataPre(SaveAndLoadCtrl __instance, string __m_loadDataNo)
        {
            MyLog.Log("SaveAndLoadCtrl.LoadDataPre:" + __m_loadDataNo);
            
        }

        // private SaveAndLoadCtrl.LoadDataUnit GetLoadDataUnitByKey(string key)
        [HarmonyPatch(typeof(SaveAndLoadCtrl), "GetLoadDataUnitByKey")]
        [HarmonyPostfix]// 나중에 __m_loadDataNo 지워짐
        private static void GetLoadDataUnitByKeyPost(SaveAndLoadCtrl __instance, string key, SaveAndLoadCtrl.LoadDataUnit __result)
        {
            MyLog.Log("SaveAndLoadCtrl.GetLoadDataUnitByKeyPost:" + key 
                + " , " + __result.managerName 
                + " , " + __result.pageNo 
                + " , " + __result.serialNo 
                + " , " + __result.uiComment);            
        }
    }
}
