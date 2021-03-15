using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    class YotogiSkillSystemPatch
	{
		protected SortedDictionary<int, YotogiSkillData> skillDatas_;
		protected SortedDictionary<int, YotogiSkillData> oldSkillDatas_;
		/*
		public YotogiSkillSystem(Status status)
		{
			this.status = status;
			this.skillDatas_ = new SortedDictionary<int, YotogiSkillData>();
			this.datas = new ReadOnlySortedDictionary<int, YotogiSkillData>(this.skillDatas_);
			this.oldSkillDatas_ = new SortedDictionary<int, YotogiSkillData>();
			this.oldDatas = new ReadOnlySortedDictionary<int, YotogiSkillData>(this.oldSkillDatas_);
		}*/

		public YotogiSkillData Add(Skill.Data data)
		{
			if (data == null)
			{
				return null;
			}
			YotogiSkillData yotogiSkillData = null;
			if (!this.skillDatas_.ContainsKey(data.id))
			{
				yotogiSkillData = new YotogiSkillData();
				yotogiSkillData.data = data;
				yotogiSkillData.oldData = data.oldData;
				yotogiSkillData.expSystem.SetExreienceList(new List<int>(data.skill_exp_table));
				this.skillDatas_.Add(data.id, yotogiSkillData);
				if (yotogiSkillData.oldData != null)
				{
					this.oldSkillDatas_.Add(data.id, yotogiSkillData);
				}
			}
			return yotogiSkillData;
		}
	}
}
