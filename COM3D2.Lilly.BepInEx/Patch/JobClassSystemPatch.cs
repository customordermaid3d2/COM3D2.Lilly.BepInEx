using HarmonyLib;
using MaidStatus;
using MaidStatus.CsvData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin
{
    class JobClassSystemPatch : IClassSystem<JobClass.Data>
    {
        public override ReadOnlySortedDictionary<int, ClassData<JobClass.Data>> datas { get; protected set; }
        public override ReadOnlySortedDictionary<int, ClassData<JobClass.Data>> oldDatas { get; protected set; }
        public override Status status { get; protected set; }

		// MaidStatus.JobClassSystem
		/// <summary>
		/// 원본 코드
		/// </summary>
		/// <param name="data"></param>
		/// <param name="setOpenFlag"></param>
		/// <param name="updateBonusStatus"></param>
		/// <returns></returns>
		public override ClassData<JobClass.Data> Add(JobClass.Data data, bool setOpenFlag, bool updateBonusStatus )
		{
			if (data != null)
			{
				if (!JobClass.IsEnabled(data.id))
				{
					MyLog.LogError(string.Concat(new string[]
					{
				"メイド[",
				this.status.fullNameJpStyle,
				"]はジョブクラス[",
				data.drawName,
				"을 기억하려고했지만 유효하지 않기 때문에 기억할 수 없습니다"
					}));
					return null;
				}
				if (!data.learnConditions.isLearnPossiblePersonal(this.status.personal))
				{
					MyLog.LogError(string.Concat(new string[]
					{
				"メイド[",
				this.status.fullNameJpStyle,
				"]はジョブクラス[",
				data.drawName,
				"을 기억하려고했지만 현재의 성격 [",
				this.status.personal.drawName,
				"]는 기억할 수 없습니다"
					}));
					return null;
				}
			}
			ClassData<JobClass.Data> classData = null;
			if (data != null)
			{
				if (data.classType != AbstractClassData.ClassType.Old)
				{
					if (!this.classDatas_.ContainsKey(data.id))
					{
						classData = new ClassData<JobClass.Data>(new Action(this.status.UpdateClassBonusStatus));
						classData.data = data;
						classData.expSystem.SetExreienceList(new List<int>(data.experiences));
						this.classDatas_.Add(data.id, classData);
					}
					classData = this.classDatas_[data.id];
					if (data.classType == AbstractClassData.ClassType.Share && !this.oldClassDatas_.ContainsKey(classData.data.id))
					{
						this.oldClassDatas_.Add(classData.data.id, classData);
					}
					if (this.classDatas_.Count == 1)
					{
						this.status.ChangeJobClass(classData.data);
					}
					if (setOpenFlag)
					{
						GameMain.Instance.CharacterMgr.status.AddJobClassOpenFlag(data.id);
					}
				}
				else if (data.classType == AbstractClassData.ClassType.Old && !this.oldClassDatas_.ContainsKey(data.id))
				{
					classData = new ClassData<JobClass.Data>(new Action(this.status.UpdateClassBonusStatus));
					classData.data = data;
					classData.expSystem.SetExreienceList(new List<int>(data.experiences));
					this.oldClassDatas_.Add(data.id, classData);
				}
				if (updateBonusStatus)
				{
					this.status.UpdateClassBonusStatus();
				}
			}
			return classData;
		}


		public override void Deserialize(BinaryReader binary, int version)
        {
            throw new NotImplementedException();
        }

        public override JobClass.Data IdToClass(int id)
        {
            throw new NotImplementedException();
        }

        public override void Remove(JobClass.Data data, bool updateBonusStatus )
        {
            throw new NotImplementedException();
        }

        public override void Serialize(BinaryWriter binary)
        {
            throw new NotImplementedException();
        }

        protected override List<JobClass.Data> GetAllBaseClassData()
        {
            throw new NotImplementedException();
        }
        //[HarmonyPatch(typeof(JobClassSystem), "Awake")]
        //[HarmonyPostfix]

    }
}
