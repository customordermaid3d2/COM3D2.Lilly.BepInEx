using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class PhotoMotionDataPatch : MyLog
    {
        private ConfigEntry<string> path;
        ConfigFile customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "lilly_config.cfg"), true);

        PhotoMotionDataPatch() : base()
        {
            LogMessage("PhotoMotionData");
            path = customFile.Bind("PhotoMotionDataPatch",   // The section under which the option is shown
                 "path",  // The key of the configuration option in the configuration file
                 @"PhotoModeData\MyPose", // The default value
                 "불러올 포즈 폴더 위치 지정"); // Description of the option to show in the config file
        }


        /// <summary>
        /// 포토 모드에서 모션 목록 만듬
        /// </summary>
        /// <param name="__instance"></param>
        //public static void Create()
        [HarmonyPatch(typeof(PhotoMotionData), "Create")]
        [HarmonyPostfix]
        public static void CreatePost0(PhotoMotionData __instance) // string __m_BGMName 못가져옴
        {
            LogMessageS("PhotoMotionData.Create");

        }
    }
}
