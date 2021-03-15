using MaidStatus;
using MaidStatus.CsvData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using wf;

namespace COM3D2.Lilly.Plugin
{

	class YotogiClassSystemPatch : IClassSystem<YotogiClass.Data>
	{
		public override Status status { get; protected set; }
        public override ReadOnlySortedDictionary<int, ClassData<YotogiClass.Data>> datas { get ; protected set ; }
        public override ReadOnlySortedDictionary<int, ClassData<YotogiClass.Data>> oldDatas { get; protected set; }

		// 

		/// <summary>
		/// 분석용 쓰지 말것
		/// Maid.(Status)status.(YotogiClassSystem)yotogiClass
		/// </summary>
		/// <param name="data"></param>
		/// <param name="setOpenFlag"></param>
		/// <param name="updateBonusStatus"></param>
		/// <returns></returns>
		// MaidStatus.YotogiClassSystem
		// Token: 0x06001D66 RID: 7526 RVA: 0x000DBDA0 File Offset: 0x000DA1A0
		public override ClassData<YotogiClass.Data> Add(YotogiClass.Data data, bool setOpenFlag, bool updateBonusStatus )
		{
			if (data != null)
			{
				if (!YotogiClass.IsEnabled(data.id))
				{
					MyLog.LogErrorS(string.Concat(new string[]
					{
						"메이드[",
						this.status.fullNameJpStyle,
						"]는 야가 클래스[",
						data.drawName,
						"]을 기억하려고했지만 유효하지 않기 때문에 기억할 수 없습니다"
					}));
					return null;
				}
				if (!data.learnConditions.isLearnPossiblePersonal(this.status.personal))
				{
					Debug.LogError(string.Concat(new string[]
					{
						"메이드[",
						this.status.fullNameJpStyle,
						"]는 야가 클래스[",
						data.drawName,
						"]을 기억하려고했지만 현재의 성격[",
						this.status.personal.drawName,
						"]는 기억할 수 없습니다"
					}));
					return null;
				}
			}
			ClassData<YotogiClass.Data> classData = null;
			if (data != null)
			{
				if (data.classType != AbstractClassData.ClassType.Old)
				{
					if (!this.classDatas_.ContainsKey(data.id))
					{
						classData = new ClassData<YotogiClass.Data>(new Action(this.status.UpdateClassBonusStatus));
						classData.levelLock = true;
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
						this.status.ChangeYotogiClass(classData.data);
					}
					if (setOpenFlag)
					{
						GameMain.Instance.CharacterMgr.status.AddYotogiClassOpenFlag(data.id);
					}
				}
				else if (data.classType == AbstractClassData.ClassType.Old && !this.oldClassDatas_.ContainsKey(data.id))
				{
					classData = new ClassData<YotogiClass.Data>(new Action(this.status.UpdateClassBonusStatus));
					classData.levelLock = true;
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

        public override YotogiClass.Data IdToClass(int id)
        {
			return (!YotogiClass.Contains(id)) ? null : YotogiClass.GetData(id);
		}

        public override void Remove(YotogiClass.Data data, bool updateBonusStatus )
        {
            throw new NotImplementedException();
        }

        public override void Serialize(BinaryWriter binary)
        {
            throw new NotImplementedException();
        }

        protected override List<YotogiClass.Data> GetAllBaseClassData()
        {
			return YotogiClass.GetAllDatas(true);
		}
    }
}
