using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.IO;
using System.Linq;

namespace COM3D2.Lilly.BepInEx
{
    [BepInPlugin("COM3D2.Lilly.BepInEx", "Lilly", "1.0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        // BepInEx\config\org.bepinex.plugins.Lilly.cfg:
        private ConfigEntry<string> configGreeting;
        private ConfigEntry<bool> configDisplayGreeting;

        public Plugin()
        {
            MyLog.Log("Plugin()");
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌
            Harmony.CreateAndPatchAll(typeof(CharacterMgrPatch));
            Harmony.CreateAndPatchAll(typeof(AudioSourceMgrPatch));
        }

        // https://bepinex.github.io/bepinex_docs/master/articles/dev_guide/plugin_tutorial/2_plugin_start.html
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            MyLog.Log("Awake()");

            // 설정 파일 내보내기1
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
            MyLog.Log("Paths.ConfigPath:"+ Paths.ConfigPath);
            var userName = customFile.Bind("General",
                "UserName",
                "Deuce",
                "Name of the user");

        }
    }
}
