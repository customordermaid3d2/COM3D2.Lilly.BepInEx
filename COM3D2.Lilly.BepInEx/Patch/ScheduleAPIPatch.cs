using HarmonyLib;
using MaidStatus;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin
{
    class ScheduleAPIPatch
    {
        // ScheduleAPI

        // public static bool VisibleNightWork(int workId, Maid maid = null, bool checkFinish = true)
        // public static bool EnableNightWork(int workId, Maid maid = null, bool calledTargetCheck = true, bool withMaid = true)
        // public static bool EnableNoonWork(int workId, Maid maid = null)
        /// <summary>
		/// 스케즐 버튼 듸우기?
		/// </summary>
		/// <param name="__result"></param>
		/// <param name="workId"></param>
		/// <param name="maid"></param> null 잇을수 있음
		/// <returns></returns>
		[HarmonyPatch(typeof(ScheduleAPI), "VisibleNightWork") ]
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNightWork")]
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNoonWork")]
        [HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public static bool ScheduleAPIWork( out bool __result, int workId, Maid maid)
        {
            __result = true;

				MyLog.LogMessage("ScheduleAPIWork:" + SceneFreeModeSelectManager.IsFreeMode, workId, maid != null ? MaidUtill.GetMaidFullNale(maid) : "" );

			return false; // SceneFreeModeSelectManager.IsFreeMode;
        }

		public static void SetAllWork()
        {
			ReadOnlyDictionary<int, NightWorkState> night_works_state_dic = GameMain.Instance.CharacterMgr.status.night_works_state_dic;
            foreach (var item in night_works_state_dic)
            {
				NightWorkState nightWorkState = item.Value;
				nightWorkState.finish= true;
			}

			//if (night_works_state_dic.ContainsKey(this.vip_data_.id) && night_works_state_dic[this.vip_data_.id].finish)
			//{
			//	this.is_enabled_ = true;
			//}

			foreach (Maid maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
			{
				//maid.status.SetFlag("_YotogiPlayed", 1);
				foreach (ScheduleCSVData.Yotogi yotogi in ScheduleCSVData.YotogiData.Values)
				{
					MyLog.LogMessage("SetAllWork:" + MaidUtill.GetMaidFullNale(maid)
						, yotogi.id
						);
					if (DailyMgr.IsLegacy)
					{
						maid.status.OldStatus.SetFlag("_PlayedNightWorkId" + yotogi.id, 1);
					}
					else
					{
						maid.status.SetFlag("_PlayedNightWorkId" + yotogi.id, 1);
					}
					//ScheduleCSVData.YotogiType yotogiType = yotogi.yotogiType;
					//if (yotogiType == ScheduleCSVData.YotogiType.Vip || yotogiType == ScheduleCSVData.YotogiType.VipCall)
					//{
					//}
				}
				if (DailyMgr.IsLegacy)
				{
					maid.status.OldStatus.SetFlag("_PlayedNightWorkVip", 1);
				}
				else
				{
					maid.status.SetFlag("_PlayedNightWorkVip", 1);
				}

			}

		}


		public static void SetAllWork1()
        {
			//var scheduleBases = ScheduleCSVData.AllData;
			foreach (var item in ScheduleCSVData.AllData)
			{
				var scheduleBase = item.Value;
				foreach (Maid scheduleSlot in GameMain.Instance.CharacterMgr.GetStockMaidList())
				{
				//ScheduleAPI.EnableNightWork(value.id, null, true, true)

					ScheduleTaskCtrl.TaskType type = scheduleBase.type;
					if (type == ScheduleTaskCtrl.TaskType.Yotogi)
					{
						ScheduleCSVData.Yotogi yotogi = (ScheduleCSVData.Yotogi)scheduleBase;
						//if (BackupParamAccessor.YotogiPlayed(j, scene_ID))
						{
							//scheduleSlot.status.SetFlag("_YotogiPlayed", 1);
						}
						//else
						{
							//	scheduleSlot.status.SetFlag("_YotogiPlayed", 0);
						}
						//ScheduleAPI.YotogiResultSimulateParam param = ScheduleAPI.YotogiResultSimulate(scheduleSlot, num);
						//ScheduleAPI.AddYotogiWorkResultParam(yotogi, scheduleSlot, param);
						//BackupParamAccessor.BackupParam(new int?(j), sceneId_f);
						if (DailyMgr.IsLegacy)
						{
							scheduleSlot.status.OldStatus.SetFlag("_PlayedNightWorkId" + scheduleSlot.status.nightWorkId.ToString(), 1);
						}
						else
						{
							scheduleSlot.status.SetFlag("_PlayedNightWorkId" + scheduleSlot.status.nightWorkId.ToString(), 1);
						}
						ScheduleCSVData.YotogiType yotogiType = yotogi.yotogiType;
						if (yotogiType == ScheduleCSVData.YotogiType.Vip || yotogiType == ScheduleCSVData.YotogiType.VipCall)
						{
							if (DailyMgr.IsLegacy)
							{
								scheduleSlot.status.OldStatus.SetFlag("_PlayedNightWorkVip", 1);
							}
							else
							{
								scheduleSlot.status.SetFlag("_PlayedNightWorkVip", 1);
							}
						}
						ScheduleCSVData.YotogiType yotogiType2 = yotogi.yotogiType;
						if (yotogiType2 == ScheduleCSVData.YotogiType.Vip || yotogiType2 == ScheduleCSVData.YotogiType.VipCall)
						{
							if (GameMain.Instance.CharacterMgr.status.night_works_state_dic.ContainsKey(item.Key))
							{
								GameMain.Instance.CharacterMgr.status.night_works_state_dic[item.Key].finish = true;
							}
						}
							
					}
				}
			}
		}

		/// <summary>
		/// 분석용
		/// </summary>
		/// <param name="workId"></param>
		/// <param name="maid"></param>
		/// <param name="checkFinish"></param>
		/// <returns></returns>
		public static bool VisibleNightWork(int workId, Maid maid , bool checkFinish )
		{
			ScheduleCSVData.Yotogi yotogi = ScheduleCSVData.YotogiData[workId];
			switch (yotogi.yotogiType)
			{
				case ScheduleCSVData.YotogiType.Vip:
				case ScheduleCSVData.YotogiType.VipCall:
					{
						NightWorkState nightWorksState = GameMain.Instance.CharacterMgr.status.GetNightWorksState(workId);
						//if (nightWorksState == null)
						{
							//return false;
						}
						//nightWorksState.finish = true;
						//if (checkFinish && nightWorksState.finish)
						{
							//if (DailyMgr.IsLegacy)
							{
								//return false;
							}
							//if (GameMain.Instance.CharacterMgr.status.clubGrade < 5)
							{
								//return false;
							}
							//ScheduleCSVData.vipFullOpenDay = 0;
							//if (GameMain.Instance.CharacterMgr.status.days < ScheduleCSVData.vipFullOpenDay)
							{
								//return false;
							}
						}
						break;
					}
				case ScheduleCSVData.YotogiType.Travel:
					//return false;
					break;
				case ScheduleCSVData.YotogiType.EasyYotogi:
					{
						//if (yotogi.easyYotogi == null)
						{
							//return false;
						}
						//int trophyId = yotogi.easyYotogi.trophyId;
						//if (!GameMain.Instance.CharacterMgr.status.IsHaveTrophy(trophyId))
						{
							//return false;
						}
						break;
					}
			}
			if (yotogi.condPackage.Count > 0)
			{
				for (int i = 0; i < ScheduleCSVData.YotogiData[workId].condPackage.Count; i++)
				{
					if (!PluginData.IsEnabled(ScheduleCSVData.YotogiData[workId].condPackage[i]))
					{
						return false;
					}
				}
			}
			if (yotogi.condManVisibleFlag1.Count > 0)
			{
				for (int j = 0; j < yotogi.condManVisibleFlag1.Count; j++)
				{
					if (GameMain.Instance.CharacterMgr.status.GetFlag(yotogi.condManVisibleFlag1[j]) < 1)
					{
						return false;
					}
				}
			}
			if (maid != null)
			{
				if (ScheduleCSVData.YotogiData[workId].condMainChara && !maid.status.mainChara)
				{
					return false;
				}
				if (yotogi.condPersonal.Count > 0)
				{
					bool flag = false;
					for (int k = 0; k < yotogi.condPersonal.Count; k++)
					{
						if (maid.status.personal.id == yotogi.condPersonal[k])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (yotogi.subMaidUnipueName != string.Empty)
				{
					if (maid.status.heroineType != HeroineType.Sub)
					{
						return false;
					}
					if (yotogi.subMaidUnipueName != maid.status.subCharaData.uniqueName)
					{
						return false;
					}
				}
				else if (maid.status.heroineType == HeroineType.Sub)
				{
					return false;
				}
			}
			return true;
		}

	}
}
