using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin
{


    class PersonalPatch
    {
        // Personal
        public static CsvCommonIdManager commonIdManager;
        public static Dictionary<int, Personal.Data> basicDatas;

        // 스테틱은 생성자가 없어서 사용 불가능한듯
        [HarmonyPatch(typeof(Personal),MethodType.Constructor)]        
        static void Constructor(CsvCommonIdManager ___commonIdManager, Dictionary<int, Personal.Data> ___basicDatas)//Personal __instance, 
        {
            commonIdManager = ___commonIdManager;
            basicDatas = ___basicDatas;
        }

        PersonalPatch()
        {
            Type type = typeof(Personal);

            System.Reflection.FieldInfo[] fields = type.GetFields();
            
            foreach (var field in fields)
            {
                MyLog.LogDebug(string.Format("{0}.{1} {2}", type.Name, field.Name, field.FieldType));
            }

            // 효율때문에
            FieldInfo info = type.GetField("commonIdManager", BindingFlags.NonPublic | BindingFlags.Static);
            commonIdManager = (CsvCommonIdManager)info.GetValue(null);

            FieldInfo info2 = type.GetField("basicDatas", BindingFlags.NonPublic | BindingFlags.Static);
            basicDatas = (Dictionary<int, Personal.Data>)info2.GetValue(null);
        }

        /// <summary>
        /// 쓸데없이 로그 많음.
        /// </summary>
        /// <param name="___commonIdManager"></param>
        /// <param name="___basicDatas"></param>
        // public static int uniqueNameToId(string name)
        [HarmonyPatch(typeof(Personal), "CreateData")]
        [HarmonyPostfix]
        private static void CreateData(CsvCommonIdManager ___commonIdManager, Dictionary<int, Personal.Data> ___basicDatas) // Personal __instance,
        {
            if (commonIdManager != null)
            {
                return;
            }

            MyLog.LogMessage("CreateData." );

            commonIdManager = ___commonIdManager;
            basicDatas = ___basicDatas;
                       

            return;

            // 쓸데없이 로그 많음
            foreach (KeyValuePair<int, KeyValuePair<string, string>> i in commonIdManager.idMap)
           {
               MyLog.LogMessage("idMap:"+ i.Key + " , " + i.Value.Key + " , " + i.Value.Value);
           }
           
           foreach (var item in commonIdManager.nameMap)
           {
               MyLog.LogMessage("nameMap:" + item);
           }
           foreach (var item in basicDatas)
           {
               MyLog.LogMessage("basicDatas:" + item.Key + " : " + item.Value.drawName + " : " + item.Value.id + " : " + item.Value.replaceText + " : " + item.Value.termName + " : " + item.Value.uniqueName);//
               // competitiveMotionFileVictory  경쟁적인 모션 파일 승리
           }

            return;

            if (commonIdManager != null)
            {
                return;
            }
            commonIdManager = new CsvCommonIdManager("maid_status_personal", "性格", CsvCommonIdManager.Type.IdAndUniqueName, null);
            basicDatas = new Dictionary<int, Personal.Data>();
            string[] array = new string[]
            {
                "list",
                "feature_condition"
            };
            KeyValuePair<AFileBase, CsvParser>[] array2 = new KeyValuePair<AFileBase, CsvParser>[array.Length];
            for (int i = 0; i < array2.Length; i++)
            {
                string text = "maid_status_personal_" + array[i] + ".nei";
                AFileBase afileBase = GameUty.FileSystem.FileOpen(text);
                CsvParser csvParser = new CsvParser();
                bool condition = csvParser.Open(afileBase);
                NDebug.Assert(condition, text + "\nopen failed.");
                array2[i] = new KeyValuePair<AFileBase, CsvParser>(afileBase, csvParser);
            }
            basicDatas = new Dictionary<int, Personal.Data>();
            HashSet<int> additionalRelationEnabledIdList = new HashSet<int>();
            CsvCommonIdManager.ReadEnabledIdList(GameUty.FileSystem, GameUty.PathList, "maid_status_personal_additionalrelation_enabled_list", ref additionalRelationEnabledIdList);
            foreach (KeyValuePair<int, KeyValuePair<string, string>> keyValuePair in commonIdManager.idMap)
            {
                basicDatas.Add(keyValuePair.Key, new Personal.Data(keyValuePair.Key, array2[0].Value, array2[1].Value, additionalRelationEnabledIdList));
            }
        }


        // public static int uniqueNameToId(string name)
        //[HarmonyPatch(typeof(Personal), "uniqueNameToId")]
        //[HarmonyPostfix]
        private static void uniqueNameToId( string name) // Personal __instance,
        {
            MyLog.LogMessage("Personal.uniqueNameToId:" + name);
        }
    }
}
