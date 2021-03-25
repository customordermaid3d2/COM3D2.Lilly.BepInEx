using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Patch
{
    /// <summary>
    ///  참고용
    /// </summary>
    class TBodyPatch
    {
        // public static void AddItem(global::TBody tb, global::MPN mpn, int subno, string slotname, string filename, string AttachSlot, string AttachName, bool f_bTemp, int version)
        // public        void AddItem(                          MPN mpn, string slotname, string filename, string AttachSlot, string AttachName, bool f_bTemp)
        // public        void AddItem(                          MPN mpn, int subno, string slotname, string filename, string AttachSlot, string AttachName, bool f_bTemp, int version)

        //public void ChangeTex(string slotname,                int matno, string prop_name, string filename,                                    Dictionary<string, byte[]> dicModTexData,         MaidParts.PARTS_COLOR f_ePartsColorId =         MaidParts.PARTS_COLOR.NONE)
        //public void ChangeTex(string slotname, int subPropNo, int matno, string prop_name, string filename, global::System.Collections.Generic.Dictionary<string, byte[]> dicModTexData, global::MaidParts.PARTS_COLOR f_ePartsColorId = global::MaidParts.PARTS_COLOR.NONE)
        public static void FaceTypeHook(global::TBody self, ref string slotname, ref int subPropNo, ref int matno, ref string prop_name, ref string filename, ref global::System.Collections.Generic.Dictionary<string, byte[]> dicModTexData, ref global::MaidParts.PARTS_COLOR f_ePartsColorId)
        {

        }
    }
}
