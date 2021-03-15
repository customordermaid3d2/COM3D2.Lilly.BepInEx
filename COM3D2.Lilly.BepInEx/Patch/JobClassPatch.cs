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
    class JobClassPatch
    {
		public static CsvCommonIdManager commonIdManager;
		public static Dictionary<int, JobClass.Data> basicDatas;

		[HarmonyPatch(typeof(JobClass), "CreateData")]
        [HarmonyPostfix]
		// MaidStatus.JobClass
		// Token: 0x06001D95 RID: 7573 RVA: 0x000DD810 File Offset: 0x000DBC10
		public static void CreateData(CsvCommonIdManager ___commonIdManager, Dictionary<int, JobClass.Data> ___basicDatas)
		{
			if (commonIdManager != null)
			{
				return;
			}
			MyLog.LogMessage(MyUtill.GetClassMethodName(MethodBase.GetCurrentMethod()));

			commonIdManager = ___commonIdManager;
			basicDatas = ___basicDatas;

			return;
			/*
			JobClass.commonIdManager = new CsvCommonIdManager("maid_status_jobclass", "ジョブクラス", CsvCommonIdManager.Type.IdAndUniqueName, null);
			JobClass.basicDatas = new Dictionary<int, JobClass.Data>();
			string[] array = new string[]
			{
		"list",
		"acquired_condition",
		"bonus",
		"experiences"
			};
			KeyValuePair<AFileBase, CsvParser>[] array2 = new KeyValuePair<AFileBase, CsvParser>[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				string text = "maid_status_jobclass_" + array[i] + ".nei";
				AFileBase afileBase = GameUty.FileSystem.FileOpen(text);
				CsvParser csvParser = new CsvParser();
				bool condition = csvParser.Open(afileBase);
				NDebug.Assert(condition, text + "\nopen failed.");
				array2[i] = new KeyValuePair<AFileBase, CsvParser>(afileBase, csvParser);
			}
			foreach (KeyValuePair<string, int> keyValuePair in JobClass.commonIdManager.nameMap)
			{
				string key = keyValuePair.Key;
				JobClass.Data value = new JobClass.Data(key, array2[0].Value, array2[1].Value, array2[2].Value, array2[3].Value);
				JobClass.basicDatas.Add(keyValuePair.Value, value);
			}
			foreach (KeyValuePair<AFileBase, CsvParser> keyValuePair2 in array2)
			{
				keyValuePair2.Value.Dispose();
				keyValuePair2.Key.Dispose();
			}
			if (GameUty.FileSystemOld.IsExistentFile("maid_class_enabled_list.nei"))
			{
				Dictionary<string, string> enabledIdListOld = new Dictionary<string, string>();
				Action<string> action = delegate (string file_name)
				{
					file_name += ".nei";
					if (!GameUty.FileSystemOld.IsExistentFile(file_name))
					{
						return;
					}
					using (AFileBase afileBase3 = GameUty.FileSystemOld.FileOpen(file_name))
					{
						using (CsvParser csvParser3 = new CsvParser())
						{
							bool condition3 = csvParser3.Open(afileBase3);
							NDebug.Assert(condition3, file_name + "\nopen failed.");
							for (int n = 1; n < csvParser3.max_cell_y; n++)
							{
								if (csvParser3.IsCellToExistData(0, n))
								{
									string cellAsString2 = csvParser3.GetCellAsString(0, n);
									string cellAsString3 = csvParser3.GetCellAsString(1, n);
									if (!enabledIdListOld.ContainsKey(cellAsString2))
									{
										enabledIdListOld.Add(cellAsString2, cellAsString3);
									}
								}
							}
						}
					}
				};
				action("maid_class_enabled_list");
				array = new string[]
				{
			"infotext",
			"acquisition_data",
			"bonus_status",
			"exp_list"
				};
				array2 = new KeyValuePair<AFileBase, CsvParser>[array.Length];
				for (int k = 0; k < array2.Length; k++)
				{
					string text2 = "maid_class_" + array[k] + ".nei";
					AFileBase afileBase2 = GameUty.FileSystemOld.FileOpen(text2);
					CsvParser csvParser2 = new CsvParser();
					bool condition2 = csvParser2.Open(afileBase2);
					NDebug.Assert(condition2, text2 + "\nopen failed.");
					array2[k] = new KeyValuePair<AFileBase, CsvParser>(afileBase2, csvParser2);
				}
				Dictionary<int, KeyValuePair<string, string>> dictionary = new Dictionary<int, KeyValuePair<string, string>>();
				Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
				for (int l = 1; l < array2[0].Value.max_cell_y; l++)
				{
					int num = l - 1;
					if (!JobClass.basicDatas.ContainsKey(num))
					{
						string cellAsString = array2[0].Value.GetCellAsString(0, l);
						string text3 = (!enabledIdListOld.ContainsKey(cellAsString)) ? cellAsString : enabledIdListOld[cellAsString];
						JobClass.Data value2 = new JobClass.Data(cellAsString, text3, array2[0].Value, array2[1].Value, array2[2].Value, array2[3].Value);
						JobClass.basicDatas.Add(num, value2);
						dictionary.Add(num, new KeyValuePair<string, string>(cellAsString, text3));
						dictionary2.Add(cellAsString, num);
					}
					else
					{
						JobClass.basicDatas[num].SetClassType(AbstractClassData.ClassType.Share);
					}
				}
				Dictionary<int, KeyValuePair<string, string>> dictionary3 = new Dictionary<int, KeyValuePair<string, string>>(JobClass.commonIdManager.idMap);
				Dictionary<string, int> dictionary4 = new Dictionary<string, int>(JobClass.commonIdManager.nameMap);
				HashSet<int> hashSet = new HashSet<int>(JobClass.commonIdManager.enabledIdList);
				foreach (KeyValuePair<int, KeyValuePair<string, string>> keyValuePair3 in dictionary)
				{
					dictionary3.Add(keyValuePair3.Key, keyValuePair3.Value);
					dictionary4.Add(keyValuePair3.Value.Key, keyValuePair3.Key);
				}
				foreach (KeyValuePair<string, string> keyValuePair4 in enabledIdListOld)
				{
					if (dictionary2.ContainsKey(keyValuePair4.Key) && !hashSet.Contains(dictionary2[keyValuePair4.Key]))
					{
						hashSet.Add(dictionary2[keyValuePair4.Key]);
					}
				}
				JobClass.commonIdManager = new CsvCommonIdManager(dictionary3, dictionary4, hashSet, CsvCommonIdManager.Type.IdAndUniqueName);
				foreach (KeyValuePair<AFileBase, CsvParser> keyValuePair5 in array2)
				{
					keyValuePair5.Value.Dispose();
					keyValuePair5.Key.Dispose();
				}
			}
			*/
		}
	}
}
