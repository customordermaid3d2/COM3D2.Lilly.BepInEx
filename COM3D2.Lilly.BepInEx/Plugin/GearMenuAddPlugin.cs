﻿using BepInEx;
using COM3D2API;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{

    //[BepInPlugin("COM3D2.Lilly.BepInEx", "Lilly", "1.0.0.4")]
    static class GearMenuAddPlugin //: BaseUnityPlugin
    {
        public static byte[] png = Convert.FromBase64String(
"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");

        //static private UnityEngine.GameObject menuButton;
        public static bool displayUI = false;
        public static bool patchOnOff = true;
        static string name;

        private static bool buttonAdded1 = false;

        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MyLog.LogMessageS("GearMenuAddPlugin.OnSceneLoaded: " + scene.name + " , " + SceneManager.GetActiveScene().buildIndex + " , " + scene.isLoaded);
            // SceneManager.GetActiveScene().name;

            //Add the button
            if (GameMain.Instance == null || GameMain.Instance.SysShortcut==null)
            {
                return;
            }

            if ( !buttonAdded1)
            {
                name = "그냥 테스트용";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback), name, png);                
                name = "Lilly관련 패치 온오프";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback_Patch), name, png);                
                name = "SetMaidStatusAll";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback_SetMaidStatusAll), name, png);                
                name = "SetScenarioDataAll";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback_SetScenarioDataAll), name, png);                
                name = "RemoveEventEndFlagAll";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback_RemoveEventEndFlagAll), name, png);                
                name = "아이템 장착 콘솔 로그 표시 여부";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback_MaidPatch), name, png);                
                buttonAdded1 = true;
            }
        }

        private static void OnMenuButtonClickCallback_MaidPatch()
        {
            MaidPatch.isOnOff = !MaidPatch.isOnOff;
        }

        private static void OnMenuButtonClickCallback()
        {
            //Open/Close the UI
            if (displayUI)
            {
                Lilly.lilly.DelHarmonyPatch(Lilly.lilly.listd);
            }
            else
            {
                Lilly.lilly.SetHarmonyPatch(Lilly.lilly.listd);
            }
            displayUI = !displayUI;

            MyLog.LogMessageS("OnMenuButtonClickCallback:" + displayUI);

        }

        private static void OnMenuButtonClickCallback_Patch()
        {
            if (patchOnOff)
            {
                Lilly.lilly.DelHarmonyPatch();
            }
            else
            {
                Lilly.lilly.SetHarmonyPatch();
            }
            patchOnOff = !patchOnOff;

            MyLog.LogMessageS("OnMenuButtonClickCallback_Patch:" + patchOnOff);
        }



        private static void OnMenuButtonClickCallback_SetMaidStatusAll()
        {
            MyLog.LogMessageS("OnMenuButtonClickCallback_SetMaidStatusAll");            
            MaidStatusUtill.SetMaidStatusAll();
        }

        private static void OnMenuButtonClickCallback_SetScenarioDataAll()
        {
            MyLog.LogMessageS("OnMenuButtonClickCallback_SetScenarioDataAll");            
            ScenarioDataUtill.SetScenarioDataAll();
        }
        
        private static void OnMenuButtonClickCallback_RemoveEventEndFlagAll()
        {
            MyLog.LogMessageS("OnMenuButtonClickCallback_RemoveEventEndFlagAll");            
            ScenarioDataUtill.RemoveEventEndFlagAll();
        }



    }
}
