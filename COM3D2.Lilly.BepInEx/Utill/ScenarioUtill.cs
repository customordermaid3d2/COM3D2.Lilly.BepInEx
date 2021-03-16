using MaidStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 이벤트 관련
    /// </summary>
    public static class ScenarioUtill
    {
        static CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
        //static ScriptManager﻿ scriptManager = new ScriptManager();

        /// <summary>
        /// 모든 이벤트 처리용
        /// </summary>
        public static void SetScenarioDataAll()
        {
            MyLog.LogDebug("ScenarioDataUtill.SetScenarioDataAll st"); 
            
            try
            {
                // 병렬 처리
                foreach (var scenarioData in GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData())
                {
                // MyLog.LogMessageS(".SetScenarioDataAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + scenarioData.IsPlayable + " , " + scenarioData.Title); ;
                if (scenarioData.IsPlayable)
                    {
                        bool b;
                        //MyLog.LogMessageS(".m_EventMaid");
                        foreach (var maid in scenarioData.GetEventMaidList())
                        {
                            b = maid.status.GetEventEndFlag(scenarioData.ID);
                            MyLog.LogMessage(".SetEventEndFlagAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + maid.status.firstName + " , " + maid.status.lastName + " , " + b + " , " + scenarioData.ScenarioScript.Contains("_Marriage") + " , " + scenarioData.Title); ;
                            if (!b)
                            {
                                maid.status.SetEventEndFlag(scenarioData.ID, true);
                                if (scenarioData.ScenarioScript.Contains("_Marriage"))
                                {
                                    maid.status.specialRelation = SpecialRelation.Married;
                                    maid.status.relation = Relation.Lover;
                                    maid.status.OldStatus.isMarriage = true;
                                    maid.status.OldStatus.isNewWife = true;
                                    //SetMaidCondition(0, '嫁');
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll : " + e.ToString());
            }

            return;
        }

        public static void SetScenarioAll()
        {
            SetEveryday(FreeModeItemEveryday.ScnearioType.Nitijyou);
            SetEveryday(FreeModeItemEveryday.ScnearioType.Story);
            SetFlagLifeMode();
            SetItemVip();

            foreach (var data in GameMain.Instance.CharacterMgr.status.flags)
            {
                MyLog.LogMessage("flags"
                    , data.Key
                    , data.Value
                    );
            }
        }

        private static void SetItemVip()
        {
            SortedDictionary<int, ScheduleCSVData.Yotogi> vip_data_dic_ = new SortedDictionary<int, ScheduleCSVData.Yotogi>();
            Dictionary<Personal.Data, HashSet<int>> personal_enabled_dic_ = new Dictionary<Personal.Data, HashSet<int>>();
            SortedDictionary<int, string> vip_data_filename_dic_ = new SortedDictionary<int, string>();
            SortedDictionary<int, int> vip_subherioin_dic_ = new SortedDictionary<int, int>();
            foreach (Personal.Data key in Personal.GetAllDatas(false))
            {
                personal_enabled_dic_.Add(key, new HashSet<int>());
            }
            SetItemVipSub(AbstractFreeModeItem.GameMode.COM3D);
            if (GameUty.IsEnabledCompatibilityMode)
            {
                SetItemVipSub(AbstractFreeModeItem.GameMode.CM3D2);
            }
        }

        private static void SetItemVipSub(AbstractFreeModeItem.GameMode gameMode)
        {

            AFileBase afileBase = null;
            if (gameMode == AbstractFreeModeItem.GameMode.CM3D2)
            {
                afileBase = GameUty.FileSystemOld.FileOpen("recollection_vip2.nei");
            }
            else if (gameMode == AbstractFreeModeItem.GameMode.COM3D)
            {
                afileBase = GameUty.FileSystem.FileOpen("recollection_vip2.nei");
            }
            using (afileBase)
            {
                using (CsvParser csvParser = new CsvParser())
                {
                    bool condition = csvParser.Open(afileBase);
                    for (int i = 1; i < csvParser.max_cell_y; i++)
                    {
                        if (csvParser.IsCellToExistData(0, i))
                        {
                            int cellAsInteger = csvParser.GetCellAsInteger(0, i);
                            int cellAsInteger2 = csvParser.GetCellAsInteger(1, i);
                            string s =csvParser.GetCellAsString(2, i);
                            //    ScheduleCSVData.ScheduleBase scheduleBase = ScheduleCSVData.AllData[cellAsInteger2];
                            MyLog.LogMessage("ItemVip:" 
                                , cellAsInteger
                                , cellAsInteger2
                                , s
                                ); 

                        }
                    }
                }
            }
        }

        private static void SetEveryday(FreeModeItemEveryday.ScnearioType type)
        {
            string fileName = string.Empty;
            string fixingFlagText;
            if (type == FreeModeItemEveryday.ScnearioType.Nitijyou)
            {
                fileName = "recollection_normal2.nei";
                fixingFlagText = "シーン鑑賞_一般_フラグ_";
            }
            else
            {
                if (type != FreeModeItemEveryday.ScnearioType.Story)
                {
                    return;
                }
                fileName = "recollection_story.nei";
                fixingFlagText = "シーン鑑賞_メイン_フラグ_";
            }
            SetEverydaySub(type, fileName, AbstractFreeModeItem.GameMode.COM3D, fixingFlagText);
            if (GameUty.IsEnabledCompatibilityMode && type == FreeModeItemEveryday.ScnearioType.Nitijyou)
            {
                SetEverydaySub(type, fileName, AbstractFreeModeItem.GameMode.CM3D2, fixingFlagText);
            }
        }

        private static void SetEverydaySub(FreeModeItemEveryday.ScnearioType type, string fileName, AbstractFreeModeItem.GameMode gameMode, string fixingFlagText)
        {
           // HashSet<int> hashSet;
            AFileBase afileBase;
            if (gameMode == AbstractFreeModeItem.GameMode.CM3D2)
            {
                afileBase = GameUty.FileSystemOld.FileOpen(fileName);
            }
            else
            {
                if (gameMode != AbstractFreeModeItem.GameMode.COM3D)
                {
                    return;
                }
                afileBase = GameUty.FileSystem.FileOpen(fileName);
            }
            using (afileBase)
            {
                using (CsvParser csvParser = new CsvParser())
                {
                    bool condition = csvParser.Open(afileBase);
                    NDebug.Assert(condition, fileName + "\nopen failed.");
                    for (int i = 1; i < csvParser.max_cell_y; i++)
                    {
                        if (csvParser.IsCellToExistData(0, i))
                        {
                            int cellAsInteger = csvParser.GetCellAsInteger(0, i);

                                int num = 1;
                                if (gameMode != AbstractFreeModeItem.GameMode.CM3D2 || type != FreeModeItemEveryday.ScnearioType.Nitijyou )
                                {
                                    string name = csvParser.GetCellAsString(num++, i);
                                    string call_file_name = csvParser.GetCellAsString(num++, i);
                                    string check_flag_name = csvParser.GetCellAsString(num++, i);
                                    if (gameMode == AbstractFreeModeItem.GameMode.COM3D)
                                    {
                                    bool netorare = (csvParser.GetCellAsString(num++, i) == "○");
                                    }
                                    string info_text = csvParser.GetCellAsString(num++, i);
                                    List<string> list = new List<string>();
                                    for (int j = 0; j < 9; j++)
                                    {
                                        if (csvParser.IsCellToExistData(num, i))
                                        {
                                            list.Add(csvParser.GetCellAsString(num, i));
                                        }
                                        num++;
                                    }
                                    int subHerionID = csvParser.GetCellAsInteger(num++, i);
                                    while (csvParser.IsCellToExistData(num, 0))
                                    {
                                        if (csvParser.GetCellAsString(num, i) == "○")
                                        {
                                            string cellAsString = csvParser.GetCellAsString(num, 0);
                                            Personal.Data data = Personal.GetData(cellAsString);
                                        }
                                        num++;
                                    }
                                GameMain.Instance.CharacterMgr.status.SetFlag(fixingFlagText + check_flag_name, 1);
                                MyLog.LogMessage("Everyday"
                                , check_flag_name
                                , call_file_name
                                , cellAsInteger
                                , name
                                , info_text
                                );
                            }
                        }
                    }
                }
            }
        }

        private static void SetFlagLifeMode()
        {
            string text = "recollection_life_mode.nei";
            using (AFileBase afileBase = GameUty.FileSystem.FileOpen(text))
            {
                using (CsvParser csv = new CsvParser())
                {
                    bool condition = csv.Open(afileBase);
                    for (int y = 1; y < csv.max_cell_y; y++)
                    {
                        if (csv.IsCellToExistData(0, y))
                        {
                            int cellAsInteger = csv.GetCellAsInteger(0, y);

                            int num = 0;
                            int m_ID = csv.GetCellAsInteger(num++, y);
                            int  m_LifeModeDataID = csv.GetCellAsInteger(num++, y);
                            string m_Title = csv.GetCellAsString(num++, y);
                            string m_PlayFileName = csv.GetCellAsString(num++, y);
                            string m_Text = csv.GetCellAsString(num++, y);
                            List<string> list = new List<string>();
                            while (num < csv.max_cell_x && csv.IsCellToExistData(num, y))
                            {
                                list.Add(csv.GetCellAsString(num++, y));
                            }
                            // if (freeModeItemLifeMode.m_IsAllEnabledPersonal)
                            // {
                            //     FreeModeItemLifeMode.m_DataDic.Add(cellAsInteger, freeModeItemLifeMode);
                            // }
                            MyLog.LogMessage("life_mode"
                            , m_ID
                            , m_LifeModeDataID
                            , m_Title
                            , m_PlayFileName
                            , m_Text
                            );
                        }
                    }
                }
            }
        }


        /// <summary>
        ///  CheckPlayableCondition 참고
        /// </summary>
        public static void RemoveEventEndFlagAll()
        {

            Action<Maid> action = delegate (Maid maid)
            {
                maid.status.RemoveEventEndFlagAll();
            };
            for (int j = 0; j < characterMgr.GetStockMaidCount(); j++)
            {
                Maid stockMaid = characterMgr.GetStockMaid(j);
                MyLog.LogMessage(".RemoveEventEndFlagAll:" + stockMaid.status.firstName + " , " + stockMaid.status.lastName); ;
                action(stockMaid);
            }

        }

        public static void RemoveEventEndFlag(Maid maid)
        {
            if (maid!=null)
            {
                MyLog.LogMessage(".RemoveEventEndFlag:" + maid.status.firstName + " , " + maid.status.lastName); ;
                maid.status.RemoveEventEndFlagAll();
            }
          
        }

        //public void RemoveEventMaid(Maid maid, bool not_again = false)
        //{
        //    if (this.m_EventMaid.Contains(maid))
        //    {
        //        this.m_EventMaid.Remove(maid);
        //        if (not_again)
        //        {
        //            maid.status.SetEventEndFlag(this.ID, true);
        //        }
        //    }
        //}

    }
}
