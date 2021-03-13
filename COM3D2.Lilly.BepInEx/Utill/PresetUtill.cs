using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    public static class PresetUtill
    {

        /// <summary>
        /// 프리셋에서 불러온 메뉴들을 반납
        /// </summary>
        /// <param name="f_prest"></param>
        /// <returns></returns>
        public static MaidProp[] getMaidProp(CharacterMgr.Preset f_prest)
        {
            // f_prest.strFileName
            //
            MaidProp[] array;
            if (f_prest.ePreType == CharacterMgr.PresetType.Body)
            {
                array = (from mp in f_prest.listMprop
                         where (1 <= mp.idx && mp.idx <= 80) || (115 <= mp.idx && mp.idx <= 122)
                         select mp).ToArray<MaidProp>();
            }
            else if (f_prest.ePreType == CharacterMgr.PresetType.Wear)
            {
                array = (from mp in f_prest.listMprop
                         where 81 <= mp.idx && mp.idx <= 110
                         select mp).ToArray<MaidProp>();
            }
            else
            {
                array = (from mp in f_prest.listMprop
                         where (1 <= mp.idx && mp.idx <= 110) || (115 <= mp.idx && mp.idx <= 122)
                         select mp).ToArray<MaidProp>();
            }

            return array;
        }
               
        /// <summary>
        /// 프리셋 목록
        /// </summary>
        /// <returns></returns>
        public static List<CharacterMgr.Preset> GetPresetAll()
        {
            return  GameMain.Instance.CharacterMgr.PresetListLoad();
        }

        // 원본 코드
        /*
        public List<CharacterMgr.Preset> PresetListLoad()
        {
            List<CharacterMgr.Preset> list = new List<CharacterMgr.Preset>();
            string presetDirectory = this.PresetDirectory;
            if (!Directory.Exists(presetDirectory))
            {
                Directory.CreateDirectory(presetDirectory);
            }
            foreach (string f_strFileName in Directory.GetFiles(presetDirectory, "*.preset"))
            {
                CharacterMgr.Preset item = this.PresetLoad(f_strFileName);
                list.Add(item);
            }
            return list;
        }
        */

        public class PresetMy
        {
            public string fileName;
            public List<MaidProp> maidProps;

            public PresetMy(string fileName, List<MaidProp> maidProps)
            {
                this.fileName = fileName;
                this.maidProps = maidProps;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void GetUniqueProc()
        {
            List<CharacterMgr.Preset> list = GameMain.Instance.CharacterMgr.PresetListLoad();

            List<PresetMy> listw = (
                from p in list
                where p.ePreType == CharacterMgr.PresetType.Wear
                select new PresetMy(p.strFileName,p.listMprop)
                ).ToList();

            var lw = listw.GroupBy(item => item.maidProps).ToDictionary(grp => grp.Key, grp => grp.ToList());

        }

    }
}
