using BepInEx;
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
    
    [BepInPlugin("COM3D2.Lilly.Plugin", "Lilly", "21.3.13")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함
    public class Lilly : BaseUnityPlugin 
    {
        MyLog log;

        public static Lilly lilly;
        /// <summary>
        /// Harmony.CreateAndPatchAll 처리할 대상을 담는 리스트
        /// </summary>
        public List<Type> lists = new List<Type>();//patch용
        public List<Type> listd = new List<Type>();//patch용
        /// <summary>
        ///  SceneManager.sceneLoaded += 관리하기 편하게 이벤트 사용
        /// </summary>
        event Action<Scene, LoadSceneMode> actioOnSceneLoaded ;//plugin용

        ThreadPlugin threadPlugin;
        public static Dictionary<Type, Harmony> harmonys=new Dictionary<Type, Harmony>();

        public Lilly()
        {
            lilly = this;

            MyLog.LogMessageS("MainPlugin()");

            log = new MyLog(this.GetType().Name);

            threadPlugin = new ThreadPlugin();

            SetHarmonyList();
            SetHarmonyPatch();
            SetOnSceneLoaded();
        }

        /// <summary>
        /// OnSceneLoaded+= 할것들. 관리하기 편하게 이벤트 사용
        /// 사실 SceneManager.sceneLoaded += this.OnSceneLoaded; 이거 직접 써도 되...
        /// </summary>
        private void SetOnSceneLoaded()
        {   
            actioOnSceneLoaded += (GearMenuAddPlugin.OnSceneLoaded);
            //actioOnSceneLoaded+=(threadPlugin.OnSceneLoaded);//정상 작동
        }

        /// <summary>
        ///  Harmony.CreateAndPatchAll 모음
        /// </summary>
        public void SetHarmonyPatch()
        {
            SetHarmonyPatch(lists);
        }

        public void SetHarmonyPatch(List<Type> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음
            
            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                try
                {
                    log.LogMessage("Plugin:" + item.Name);
                    if (!harmonys.ContainsKey(item))
                    {
                        harmonys.Add(item, Harmony.CreateAndPatchAll(item, null));
                    }
                }
                catch (Exception e)
                {
                    log.LogError("Plugin:" + e.ToString());
                }
            }
        }

        public void DelHarmonyPatch()
        {
            DelHarmonyPatch(lists);
        }
        public void DelHarmonyPatch(List<Type> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음

            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                try
                {
                    log.LogMessage("Plugin:" + item.Name);
                    Harmony harmony;
                    if (harmonys.TryGetValue(item, out harmony))
                    {
                        harmonys.Remove(item);
                        harmony.UnpatchSelf();
                    }
                }
                catch (Exception e)
                {
                    log.LogError("Plugin:" + e.ToString());
                }
            }
        }

        private void SetHarmonyList()
        {
            listd = new List<Type>() {
                typeof(CsvParserPatch),
                typeof(GameUtyPatch)
            };

            lists.Add(typeof(AudioSourceMgrPatch));
            lists.Add(typeof(BgMgrPatch));
            lists.Add(typeof(CameraMainPatch));
            lists.Add(typeof(CharacterMgrPatch));
            lists.Add(typeof(DeskManagerPatch));
            lists.Add(typeof(GameMainPatch));
            lists.Add(typeof(KasizukiMainMenuPatch));
            lists.Add(typeof(MaidManagementMainPatch));
            lists.Add(typeof(MaidPatch));
            lists.Add(typeof(MotionWindowPatch));
            lists.Add(typeof(PhotoMotionDataPatch));
            lists.Add(typeof(PopupAndTabListPatch));
            lists.Add(typeof(ProfileCtrlPapch));
            lists.Add(typeof(SaveAndLoadCtrlPatch));
            lists.Add(typeof(ScenarioDataPatch));//
            lists.Add(typeof(SceneADVPatch));
            lists.Add(typeof(SceneEditPatch)); //자꾸 오류남?
            lists.Add(typeof(SceneMgrPatch));
            lists.Add(typeof(ScenarioSelectMgrPatch));
            lists.Add(typeof(SceneScenarioSelectPatch));
            lists.Add(typeof(ScoutManagerPatch));
            lists.Add(typeof(ScriptManagerFastPatch));
            lists.Add(typeof(ScriptManagerPatch));
            lists.Add(typeof(SkillPatch));
            lists.Add(typeof(StatusMgrPatch));
            lists.Add(typeof(TJSScriptPatch));
        }

        //-----------------------------------------------

        public void Awake()
        {
            MyLog.LogDebugS("Awake");
            MyLog.LogInfoS("Awake");
            MyLog.LogMessageS("Awake");
            MyLog.LogWarningS("Awake");
            MyLog.LogFatalS("Awake");
            MyLog.LogErrorS("Awake");

            SetConfig();

            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        //-----------------------------------------------

        // 커오메용
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MyLog.LogMessageS("OnSceneLoaded: " + scene.name + " , " + SceneManager.GetActiveScene().buildIndex +" , "+ scene.isLoaded);
            // SceneManager.GetActiveScene().name;

            switch (scene.name)
            {
                case "SceneYotogi":// 밤시중 선택
                    break;
                case "SceneEdit":// 메이드 에딧
                    
                    break;
                default:
                    break;
            }

            // 관리하기 편하게 이벤트 한번에 부름
            actioOnSceneLoaded(scene, mode);            
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
            log.LogMessage("Paths.ConfigPath:" + Paths.ConfigPath);
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
