using HarmonyLib;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    internal class CsvParserPatch
    {
        // public unsafe override string GetCellAsString(int cell_x, int cell_y)
        [HarmonyPatch(typeof(CsvParser), "GetCellAsString")]
        [HarmonyPostfix]
        static void GetCellAsString(int cell_x, int cell_y, string __result)
        {

            MyLog.LogMessage("CsvParser.GetCellAsString:" + __result.ToString());

        }

        // public bool Open(AFileBase file)
        [HarmonyPatch(typeof(CsvParser), "Open")] 
        [HarmonyPostfix]
        static void Open(AFileBase file)
        {
            //MyLog.LogMessageS("CsvParser.GetCellAsString:" + file.ToString());
        }

    }
}