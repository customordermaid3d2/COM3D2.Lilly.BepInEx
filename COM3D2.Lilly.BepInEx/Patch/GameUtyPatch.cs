using HarmonyLib;
using System;

namespace COM3D2.Lilly.Plugin
{
    internal class GameUtyPatch
    {
        /// <summary>
        /// 일반 파일 오픈. 스크립트를 찿아야 하는데
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="priorityFileSystem"></param>
        // public static AFileBase FileOpen(string fileName, AFileSystemBase priorityFileSystem = null)
        [HarmonyPatch(typeof(GameUty), "FileOpen", new Type[] { typeof(string), typeof(AFileSystemBase) })]
        [HarmonyPostfix]
        static void FileOpen(string fileName, AFileSystemBase priorityFileSystem)
        {
            MyLog.LogMessage(  "GameUty.FileOpen:" + fileName );           
        }
    }
}