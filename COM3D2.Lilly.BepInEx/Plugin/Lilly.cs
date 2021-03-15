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
        public static Lilly lilly;
        /// <summary>
        /// Harmony.CreateAndPatchAll 처리할 대상을 담는 리스트
        /// </summary>
        public static List<Type> lists = new List<Type>();//patch용
        public static List<Type> listd = new List<Type>();//patch용

        public static Dictionary<Type,bool> isLogOnOff = new Dictionary<Type, bool>();//patch용
        public static bool isLogOnOffAll = true;

        /// <summary>
        ///  SceneManager.sceneLoaded += 관리하기 편하게 이벤트 사용
        /// </summary>
        static event Action<Scene, LoadSceneMode> actioOnSceneLoaded ;//plugin용

        ThreadPlugin threadPlugin;
        public static Dictionary<Type, Harmony> harmonys=new Dictionary<Type, Harmony>();

        public Lilly()
        {
            lilly = this;

            MyLog.LogMessage("MainPlugin()");

            threadPlugin = new ThreadPlugin();

            SetHarmonyList();
            SetHarmonyPatch();
            SetHarmonyisOnOff();

            SetOnSceneLoaded();
        }

        private void SetHarmonyisOnOff()
        {
            foreach (var item in lists)
            {
                isLogOnOff.Add(item,true);
            }
            foreach (var item in listd)
            {
                isLogOnOff.Add(item,false);
            }
        }

        /// <summary>
        /// OnSceneLoaded+= 할것들. 관리하기 편하게 이벤트 사용
        /// 사실 SceneManager.sceneLoaded += this.OnSceneLoaded; 이거 직접 써도 되...
        /// </summary>
        private void SetOnSceneLoaded()
        {   
            actioOnSceneLoaded += (GearMenuAddPlugin.OnSceneLoaded);
            actioOnSceneLoaded += (SceneMaidManagementPlugin.OnSceneLoaded);
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
                    MyLog.LogMessage("Plugin:" + item.Name);
                    if (!harmonys.ContainsKey(item))
                    {
                        harmonys.Add(item, Harmony.CreateAndPatchAll(item, null));
                    }
                }
                catch (Exception e)
                {
                    MyLog.LogError("Plugin:" + e.ToString());
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
                    MyLog.LogMessage("Plugin:" + item.Name);
                    Harmony harmony;
                    if (harmonys.TryGetValue(item, out harmony))
                    {
                        harmonys.Remove(item);
                        harmony.UnpatchSelf();
                    }
                }
                catch (Exception e)
                {
                    MyLog.LogError("Plugin:" + e.ToString());
                }
            }
        }

        private void SetHarmonyList()
        {
            listd = new List<Type>() {
                //typeof(CsvParserPatch),
                //typeof(GameUtyPatch)
            };

            lists.Add(typeof(AudioSourceMgrPatch));
            lists.Add(typeof(BgMgrPatch));
            //lists.Add(typeof(CameraMainPatch));
            lists.Add(typeof(CharacterMgrPatch));
            //lists.Add(typeof(DeskManagerPatch));
            //lists.Add(typeof(GameMainPatch));
            //lists.Add(typeof(KasizukiMainMenuPatch));
            lists.Add(typeof(MaidManagementMainPatch));// 메이드 관리
            lists.Add(typeof(CsvCommonIdManagerPatch));
            lists.Add(typeof(ClassChangePanelPatch));
            lists.Add(typeof(JobClassPatch));
            lists.Add(typeof(YotogiSkillSystemPatch));
            lists.Add(typeof(YotogiClassSystemPatch));
            lists.Add(typeof(YotogiSkillDataPatch));
            lists.Add(typeof(MaidPatch));
            lists.Add(typeof(ProfileCtrlPatch));
            lists.Add(typeof(PersonalPatch)); // basicDatas 정보 얻기 위해서
            //lists.Add(typeof(StatusPatch));// 오류/떠서 사용 포기
            //lists.Add(typeof(MotionWindowPatch));//포토모드 모션창
            //lists.Add(typeof(PhotoMotionDataPatch));
            //lists.Add(typeof(PopupAndTabListPatch));

            //lists.Add(typeof(SaveAndLoadCtrlPatch));
            //lists.Add(typeof(ScenarioDataPatch));//
            //lists.Add(typeof(SceneADVPatch));
            lists.Add(typeof(SceneEditPatch)); //자꾸 오류남?
            //lists.Add(typeof(SceneMgrPatch));
            //lists.Add(typeof(ScenarioSelectMgrPatch));// 이벤트 기초 목록 관련
            lists.Add(typeof(SceneScenarioSelectPatch));
            lists.Add(typeof(ScoutManagerPatch));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
            //lists.Add(typeof(ScriptManagerFastPatch));
            lists.Add(typeof(ScriptManagerPatch));
            lists.Add(typeof(SkillPatch));// 밤시중 스테이지 선택후 스킬 목록 가져오면서 작동
            lists.Add(typeof(StatusMgrPatch));//메이드 관리의 스텟 화면
            lists.Add(typeof(StatusCtrlPatch));//메이드 관리의 스텟 화면에 값 주입
            //lists.Add(typeof(TJSScriptPatch));
        }

        //-----------------------------------------------

        public void Awake()
        {
            MyLog.LogDebug("Awake");
            MyLog.LogInfo("Awake");
            MyLog.LogMessage("Awake");
            MyLog.LogWarning("Awake");
            MyLog.LogFatal("Awake");
            MyLog.LogError("Awake");

            SetConfig();

            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        //-----------------------------------------------

        // 커오메용
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MyLog.LogMessage("OnSceneLoaded: " + scene.name + " , " + SceneManager.GetActiveScene().buildIndex +" , "+ scene.isLoaded);
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
            MyLog.LogMessage("Paths.ConfigPath:" + Paths.ConfigPath);
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
