using HarmonyLib;
using System;
using System.Collections.Generic;

namespace COM3D2.Lilly.Plugin
{
    internal class ScenarioSelectMgrPatch
    {
		static Dictionary<int, ScenarioData> m_AllScenarioData = new Dictionary<int, ScenarioData>();
		static Dictionary<string, string> m_PersonalConvertData = new Dictionary<string, string>();

        [HarmonyPatch(typeof(ScenarioSelectMgr), "InitScenarioData")]
        [HarmonyPostfix]
		public static void InitScenarioData()
		{
			if (m_AllScenarioData.Count > 0)
			{
				return;
			}
			HashSet<int> enabled_id_list = new HashSet<int>();
			Action<string> action = delegate (string file_name)
			{
				file_name += ".nei";
				if (!GameUty.FileSystem.IsExistentFile(file_name))
				{
					return;
				}
				MyLog.LogMessageS(".InitScenarioData:"+file_name);
				using (AFileBase afileBase2 = GameUty.FileSystem.FileOpen(file_name))
				{
					using (CsvParser csvParser2 = new CsvParser())
					{
						bool condition2 = csvParser2.Open(afileBase2);
						NDebug.Assert(condition2, file_name + "\nopen failed.");
						for (int k = 1; k < csvParser2.max_cell_y; k++)
						{
							if (csvParser2.IsCellToExistData(0, k))
							{
								int cellAsInteger2 = csvParser2.GetCellAsInteger(0, k);
								if (!enabled_id_list.Contains(cellAsInteger2))
								{
									enabled_id_list.Add(cellAsInteger2);
								}
							}
						}
					}
				}
			};
			action("selectable_scenario_id");
			for (int i = 0; i < GameUty.PathList.Count; i++)
			{
				action("selectable_scenario_id_" + GameUty.PathList[i]);
			}
			using (AFileBase afileBase = GameUty.FileSystem.FileOpen("select_scenario_data.nei"))
			{
				using (CsvParser csvParser = new CsvParser())
				{
					bool condition = csvParser.Open(afileBase);
					NDebug.Assert(condition, "select_scenario_data.nei]");
					for (int j = 1; j < csvParser.max_cell_y; j++)
					{
						if (csvParser.IsCellToExistData(0, j))
						{
							int cellAsInteger = csvParser.GetCellAsInteger(0, j);
							if (enabled_id_list.Contains(cellAsInteger))
							{
								if (!m_AllScenarioData.ContainsKey(cellAsInteger))
								{
									m_AllScenarioData.Add(cellAsInteger, new ScenarioData(csvParser, j));
								}
							}
						}
					}
				}
			}
			KasaiUtility.CsvReadY("personal_convert_data.nei", new Action<CsvParser, int>(ReadConvertData), 2, null);
		}

		private static void ReadConvertData(CsvParser csv, int cy)
		{
			m_PersonalConvertData.Add(csv.GetCellAsString(0, cy), csv.GetCellAsString(1, cy));
		}

	}
}