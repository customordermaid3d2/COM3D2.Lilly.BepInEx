using Kasizuki;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    class MaidUtill
    {
        // ProfileCtrl component = gameObject2.GetComponent<ProfileCtrl>();
        // Status status = (Status)typeof(ProfileCtrl).GetField("m_maidStatus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(component);
        // Dictionary<string, Personal.Data> dictionary = (Dictionary<string, Personal.Data>)typeof(ProfileCtrl).GetField("m_dicPersonal", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

        /*
        Maid.Status
            Status() / 
        		Feature.CreateData();
			    Propensity.CreateData();
			    YotogiClass.CreateData();
			    JobClass.CreateData();
			    Personal.CreateData();
			    SubMaid.CreateData();
			    PersonalEventBlocker.CreateData();
                Personal.CreateData();        
                    Personal.commonIdManager = new CsvCommonIdManager("maid_status_personal", "性格", CsvCommonIdManager.Type.IdAndUniqueName, null);
                Personal.Data
                    id
                    uniqueName
                    termName
                this.SetPersonal(Personal.GetData("Muku"));
        */

        public static string GetMaidFullNale(Maid maid)
        {
            StringBuilder s = new StringBuilder();
            if (maid.status !=null)
            {
                s.Append(        maid.status.firstName);
                s.Append(" , " + maid.status.lastName);
                if (maid.status.personal!=null)
                {
                    s.Append(" , " + maid.status.personal.id);
                    s.Append(" , " + maid.status.personal.replaceText);
                    s.Append(" , " + maid.status.personal.uniqueName );
                    s.Append(" , " + maid.status.personal.drawName   );
                }

            }
            return s.ToString();
        }


    }
}
