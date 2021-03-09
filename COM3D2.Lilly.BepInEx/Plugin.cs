﻿using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace COM3D2.Lilly.Plugin
{
    // 이벤트 함수의 실행 순서
    // https://docs.unity3d.com/kr/current/Manual/ExecutionOrder.html

    [BepInPlugin("COM3D2.Lilly.BepInEx", "Lilly", "1.0.0.3")]
    
    public class Plugin : BaseUnityPlugin
    {

        public Plugin()
        {
            MyLog.Log("Plugin()");

            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

            /*
            List<Type> list=new List<Type>();

            list.Add(typeof(CharacterMgrPatch));
            list.Add(typeof(AudioSourceMgrPatch));
            list.Add(typeof(MaidPatch));
            list.Add(typeof(SceneScenarioSelectPatch));
            list.Add(typeof(BgMgrPatch));
            list.Add(typeof(GameMainPatch));
            list.Add(typeof(SaveAndLoadCtrlPatch));
            list.Add(typeof(SkillPatch));
            list.Add(typeof(ScriptManagerPatch));
            list.Add(typeof(KasizukiMainMenuPatch));

            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                try
                {
                    MyLog.Log("Plugin:"+ item.Name);
                    Harmony.CreateAndPatchAll(item, null);
                }
                catch (Exception e)
                {
                    MyLog.LogError("Plugin:"+ e.ToString());
                }
            }
            */
            
        }

        //-----------------------------------------------

        public void Awake()
        {
            MyLog.Log("Awake");

            SetConfig();

            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        //-----------------------------------------------

        // 커오메용
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MyLog.Log("OnSceneLoaded: " + scene.name + " , " + SceneManager.GetActiveScene().buildIndex  );
            // SceneManager.GetActiveScene().name;
            
            switch (scene.name)
            {
                case "SceneYotogi":// 밤시중 선택
                    break;
                default:
                    break;
            }
        }

        //-----------------------------------------------
        // 설정파일 내뵤내기용. 정상 테스트 완료
        // BepInEx\config\org.bepinex.plugins.Lilly.cfg:
        private ConfigEntry<string> configGreeting;
        private ConfigEntry<bool> configDisplayGreeting;

        /// <summary>
        /// 설정파일 세팅
        /// </summary>
        private void SetConfig()
        {
            // 설정 파일 내보내기1. COM3D2.Lilly.BepInEx.cfg
            // https://bepinex.github.io/bepinex_docs/master/articles/dev_guide/plugin_tutorial/3_configuration.html
            configGreeting = Config.Bind("General",   // The section under which the option is shown
                             "GreetingText",  // The key of the configuration option in the configuration file
                             "Hello, world!", // The default value
                             "A greeting text to show when the game is launched"); // Description of the option to show in the config file

            configDisplayGreeting = Config.Bind("General.Toggles",
                                                "DisplayGreeting",
                                                true,
                                                "Whether or not to show the greeting text");

            // 설정 파일 내보내기2
            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "lilly_config.cfg"), true);
            MyLog.Log("Paths.ConfigPath:" + Paths.ConfigPath);
            var userName = customFile.Bind("General",
                "UserName",
                "Deuce",
                "Name of the user");
        }
        //-----------------------------------------------

        public void OnEnable()
        {

        }

        //-----------------------------------------------

        public void Reset()
        {

        }

        //-----------------------------------------------

        public void Start()
        {

        }

        //-----------------------------------------------

        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {

            }
        }

        //-----------------------------------------------

        public void LateUpdate()
        {

        }

        //-----------------------------------------------

        public void OnGUI()
        {

        }

        //-----------------------------------------------

        public void OnDisable()
        {

        }

        //-----------------------------------------------

        public void OnDestroy()
        {

        }
    }
}
