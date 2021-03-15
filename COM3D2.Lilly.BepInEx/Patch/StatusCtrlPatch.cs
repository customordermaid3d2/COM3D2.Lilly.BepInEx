using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class StatusCtrlPatch
    {
        public static StatusMgr m_mgr;
        public static GameObject m_goPanel;

        [HarmonyPatch(typeof(StatusCtrl), "Init")]
        [HarmonyPrefix]
        public static void Init(StatusMgr statusMgr, GameObject goStatusPanel)
        {
            MyLog.LogMessage(MyUtill.GetClassMethodName(MethodBase.GetCurrentMethod()));
            m_mgr = statusMgr;
            m_goPanel = goStatusPanel;
        }

        [HarmonyPatch(typeof(StatusCtrl), "SetData")]
        [HarmonyPrefix]
        public static void SetData(StatusCtrl.Status status)
        {
           MyLog.LogMessage(MyUtill.GetClassMethodName(MethodBase.GetCurrentMethod()));

           // this.m_lMaidClass.text = status.maidClassName;
           // this.m_lMaidClassLevel.text = status.maidClassLevel.ToString();
           // this.m_lMaidClassExp.text = ((status.maidClassExp >= 0) ? status.maidClassExp.ToString() : "-");
           // this.m_lMaidClassRequiredExp.text = ((status.maidClassRequiredExp >= 0) ? status.maidClassRequiredExp.ToString() : "-");
           // this.m_lYotogiClass.text = status.yotogiClassName;
           // this.m_lYotogiClassLevel.text = status.yotogiClassLevel.ToString();
           // this.m_lYotogiClassExp.text = ((status.yotogiClassExp >= 0) ? status.yotogiClassExp.ToString() : "-");
           // this.m_lYotogiClassRequiredExp.text = ((status.yotogiClassRequiredExp >= 0) ? status.yotogiClassRequiredExp.ToString() : "-");
        }
    }
}
