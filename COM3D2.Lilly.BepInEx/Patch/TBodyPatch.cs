using CM3D2.ExternalSaveData.Managed;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    /* TBody
            .BoneMorph_
                .List<BoneMorphLocal> bones
                    .Transform linkT

        int num = (int)TBody.hashSlotName[slotname];
        TBodySkin.Transform obj_tr= TBody.goSlot[num, subno];

        TMorphBone tmorphBone = (TMorphBone)this.bonemorph;

    this.boVisible_PANZU = this.goSlot[(int)TBody.hashSlotName["panz"]].boVisible;
    */
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

       // public static string text = "Bip01";
       // public static BoneMorph_ bonemorph;
       // public static GameObject m_Bones;
       // public static Transform m_trBones;
       // public static Transform Body;

        [HarmonyPatch(typeof(TBody), "LoadBody_R", typeof(string), typeof(Maid) , typeof(int) , typeof(bool) ) ,HarmonyPostfix  ]
        public static void LoadBody_R(string f_strModelFileName, Maid f_maid, int bodyVer, bool crcImport
            , TBody __instance
            )
        {
            MyLog.LogInfo("LoadBody_R"
                , f_strModelFileName
                , MaidUtill.GetMaidFullNale(f_maid)
                , bodyVer
                , crcImport
                );

            //m_trBones=__instance.m_trBones;
            //__instance.m_Bones;


            // __instance.m_Bones = UnityEngine.Object.Instantiate<GameObject>(gameObject);
            // __instance.m_Bones.name = gameObject.name;
            // __instance.m_trBones = this.m_Bones.transform;
            // CMT.SearchAndAddObj(this.m_trBones, this.m_dicTrans);
            // this.m_Animation = this.m_Bones.GetComponent<Animation>();

            //__instance.trBip = CMT.SearchObjName(this.m_trBones, text, true);
            //__instance.UpperArmR = CMT.SearchObjName(__instance.m_trBones, text + " R UpperArm", true);
            //__instance.UpperArmL = CMT.SearchObjName(__instance.m_trBones, text + " L UpperArm", true);
            //__instance.ForearmR = CMT.SearchObjName( __instance.m_trBones, text + " R Forearm", true);
            //__instance.ForearmL = CMT.SearchObjName( __instance.m_trBones, text + " L Forearm", true);

        }
        /*
        public Transform GetBone(string f_strBoneName)
        {
            return CMT.SearchObjName(this.m_trBones, f_strBoneName, true);
        }
        */

        [HarmonyPostfix, HarmonyPatch(typeof(TBody), "LateUpdate")]
        public static void LateUpdate(TBody __instance)
        {
            MyLog.LogInfo("TBody.LateUpdate"
            , MaidUtill.GetMaidFullNale(__instance.maid)
            );

            try
            {
                SetBoneUpdateAll(__instance , "Bip01 L Clavicle"
                                            , "Bip01 L UpperArm"
                                            , "Bip01 L Forearm");
                SetBoneUpdateAll(__instance , "Bip01 R Clavicle"
                                            , "Bip01 R UpperArm"
                                            , "Bip01 R Forearm");
                SetBoneUpdateAll(__instance , "Bip01 L Thigh"
                                            , "Bip01 L Calf"
                                            , "Bip01 L Foot");
                SetBoneUpdateAll(__instance , "Bip01 R Thigh"
                                            , "Bip01 R Calf"
                                            , "Bip01 R Foot");
            }
            catch (Exception e)
            {
                MyLog.LogInfo("TBody.LateUpdate"
                , e.ToString()
                );
            }
        }

        private static void SetBoneUpdateAll(TBody instance, params string[]  args)
        {
            for (int i = 0; i < args.Length - 1 ; i++)
            {
                SetBoneUpdate(instance, args[i], args[i+1]);
            }
        }

        public static void SetBoneUpdate(TBody __instance,string perent,string child)
        {
            MyLog.LogInfo("SetBone"
            , perent
            , child
            );
            Transform t = __instance.GetBone(child);
            float x = __instance.GetBone(perent).localScale.x;
            Vector3 b = new Vector3(x, 1f, 1f);
            Vector3 point = new Vector3(1f / x - 1f, 0f, 0f);
            Vector3 eulerAngles = t.localRotation.eulerAngles;
            Quaternion rotation = Quaternion.Euler(eulerAngles - new Vector3(180f, 180f, 180f));
            Vector3 vector = rotation * point;
            Vector3 a = new Vector3(1f, 1f, 1f) + new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
            Vector3 s1 = Vector3.Scale(a, b);
            t.localScale = s1;
        }



        public static void GetTbodyInfo()
        {

                Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);
                if (maid == null)
                {
                    return;
                }

                foreach (string item in TBody.m_strDefSlotNameCRC)
                {
                    MyLog.LogInfo("m_strDefSlotNameCRC"
                    , item
                    );
                }

                foreach (string item in TBody.m_strDefSlotName)
                {
                    MyLog.LogInfo("m_strDefSlotName"
                    , item
                    );
                }

                TBody body = maid.body0;

            try
            {
                BoneMorph_ bonemorph= body.bonemorph;
                foreach (BoneMorphLocal item in bonemorph.bones)
                {
                    Transform t = item.linkT;
                    MyLog.LogInfo("bonemorph.bones.linkT"
                    , t.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("bonemorph.bones:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, float> m_MorphBlendValues = body.m_MorphBlendValues;
                foreach (KeyValuePair<string, float> item in m_MorphBlendValues)
                {
                    MyLog.LogInfo("m_MorphBlendValues"
                    , item.Key
                    , item.Value
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_MorphBlendValues:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, Transform> m_dicBonesMR = body.m_dicBonesMR;
                foreach (KeyValuePair<string, Transform> item in m_dicBonesMR)
                {
                    MyLog.LogInfo("m_dicBonesMR"
                    , item.Key
                    , item.Value.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_dicBonesMR:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, Transform> m_dicTrans = body.m_dicTrans;
                foreach (KeyValuePair<string, Transform> item in m_dicTrans)
                {
                    MyLog.LogInfo("m_dicTrans"
                    , item.Key
                    , item.Value.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_dicTrans:"
                    + e.ToString()
                    );
            }
            
            try
            {
                Dictionary<string, Transform> m_dicBones = body.m_dicBones;
                foreach (KeyValuePair<string, Transform> item in m_dicBones)
                {
                    MyLog.LogInfo("m_dicBones"
                    , item.Key
                    , item.Value.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_dicBones:"
                    + e.ToString()
                    );
            }
            
            try
            {
                TBody.Slot goSlot = body.goSlot;

                for (int i = 0; i < goSlot.Count; i++)
                {
                    for (int j = 0; j < goSlot.CountChildren(i); j++)
                    //for (int j = 0; j < goSlot[i].Count; j++)
                    {
                        TBodySkin bodySkin= goSlot[i,j];
                        MyLog.LogInfo("TBody.Slot"                            
                        , bodySkin.Category
                        , bodySkin.SlotId
                        , bodySkin.m_subNo                        
                        , bodySkin.m_AttachName
                        , bodySkin.m_AttachSlotIdx
                        , bodySkin.m_AttachSlotSubIdx
                        , bodySkin.m_attachType
                        , bodySkin.m_BackAttachName 
                        , bodySkin.m_BackAttachSlotIdx 
                        , bodySkin.m_BackAttachSlotSubIdx 
                        );
                    }
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("TBody.Slot:"
                    + e.ToString()
                    );
            }



        }



    }
}


/*
[Info   :     Lilly] m_strDefSlotNameCRC , body
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , IK
[Info   :     Lilly] m_strDefSlotNameCRC , head
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , eye
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairF
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairR
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairS
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairS_2
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairT
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairT_2
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , wear
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , skirt
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , onepiece
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , mizugi
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , mizugi_top
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , mizugi_buttom
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , panz
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , slip
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , bra
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , stkg
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , shoes
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , headset
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , glove
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accHead
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accHead_2
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , hairAho
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accHana
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accHa
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accKami_1_
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accMiMiR
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accKamiSubR
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accNipR
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , HandItemR
[Info   :     Lilly] m_strDefSlotNameCRC , _IK_handR
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accKubi
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accKubiwa
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accHeso
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accUde
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accUde_2
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accAshi
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accAshi_2
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accSenaka
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accShippo
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accKoshi
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accAnl
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accVag
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , kubiwa
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , megane
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accXXX
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , chinko
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Pelvis
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , chikubi
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accFace
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accHat
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , kousoku_upper
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , kousoku_lower
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_naka
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_hara
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_face
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_mune
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_hip
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_ude
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , seieki_ashi
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accNipL
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , accMiMiL
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accKamiSubL
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accKami_2_
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , accKami_3_
[Info   :     Lilly] m_strDefSlotNameCRC , Bip01 Head
[Info   :     Lilly] m_strDefSlotNameCRC , Jyouhanshin
[Info   :     Lilly] m_strDefSlotNameCRC , HandItemL
[Info   :     Lilly] m_strDefSlotNameCRC , _IK_handL
[Info   :     Lilly] m_strDefSlotNameCRC , Uwagi
[Info   :     Lilly] m_strDefSlotNameCRC , underhair
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , asshair
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , moza
[Info   :     Lilly] m_strDefSlotNameCRC , _ROOT_
[Info   :     Lilly] m_strDefSlotNameCRC , Kahanshin
[Info   :     Lilly] m_strDefSlotNameCRC , end
[Info   :     Lilly] m_strDefSlotName , body
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , IK
[Info   :     Lilly] m_strDefSlotName , head
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , eye
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairF
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairR
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairS
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairS_2
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairT
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairT_2
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , wear
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , skirt
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , onepiece
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , mizugi
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , mizugi_top
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , mizugi_buttom
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , panz
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , slip
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , bra
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , stkg
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , shoes
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , headset
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , glove
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accHead
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accHead_2
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , hairAho
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accHana
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accHa
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accKami_1_
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accMiMiR
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accKamiSubR
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accNipR
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , HandItemR
[Info   :     Lilly] m_strDefSlotName , _IK_handR
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accKubi
[Info   :     Lilly] m_strDefSlotName , Bip01 Spine1a
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accKubiwa
[Info   :     Lilly] m_strDefSlotName , Bip01 Neck
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accHeso
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accUde
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accUde_2
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accAshi
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accAshi_2
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accSenaka
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accShippo
[Info   :     Lilly] m_strDefSlotName , Bip01 Spine
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accKoshi
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accAnl
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accVag
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , kubiwa
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , megane
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accXXX
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , chinko
[Info   :     Lilly] m_strDefSlotName , Bip01 Pelvis
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , chikubi
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accFace
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accHat
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , kousoku_upper
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , kousoku_lower
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , seieki_naka
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , seieki_hara
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , seieki_face
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , seieki_mune
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , seieki_hip
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , seieki_ude
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , seieki_ashi
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accNipL
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , accMiMiL
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accKamiSubL
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accKami_2_
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , accKami_3_
[Info   :     Lilly] m_strDefSlotName , Bip01 Head
[Info   :     Lilly] m_strDefSlotName , Jyouhanshin
[Info   :     Lilly] m_strDefSlotName , HandItemL
[Info   :     Lilly] m_strDefSlotName , _IK_handL
[Info   :     Lilly] m_strDefSlotName , Uwagi
[Info   :     Lilly] m_strDefSlotName , underhair
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , asshair
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , moza
[Info   :     Lilly] m_strDefSlotName , _ROOT_
[Info   :     Lilly] m_strDefSlotName , Kahanshin
[Info   :     Lilly] m_strDefSlotName , end
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Pelvis_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Foot
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_L
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Foot
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_R
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_R
[Info   :     Lilly] bonemorph.bones.linkT , Hip_L
[Info   :     Lilly] bonemorph.bones.linkT , Hip_R
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Hand
[Info   :     Lilly] bonemorph.bones.linkT , Kata_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Neck
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Neck_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Head
[Info   :     Lilly] bonemorph.bones.linkT , Eyepos_L
[Info   :     Lilly] bonemorph.bones.linkT , Eyepos_R
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Hand
[Info   :     Lilly] bonemorph.bones.linkT , Kata_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L_sub
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R_sub
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Pelvis_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Foot
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_L
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Foot
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_R
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_R
[Info   :     Lilly] bonemorph.bones.linkT , Hip_L
[Info   :     Lilly] bonemorph.bones.linkT , Hip_R
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Hand
[Info   :     Lilly] bonemorph.bones.linkT , Kata_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Neck
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Neck_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Head
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Hand
[Info   :     Lilly] bonemorph.bones.linkT , Kata_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L_sub
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R_sub
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L_sub
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R_sub
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Foot
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_L
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Foot
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_R
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_R
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Pelvis_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Skirt
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Pelvis_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_L
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Thigh_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Calf_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , momotwist_R
[Info   :     Lilly] bonemorph.bones.linkT , momoniku_R
[Info   :     Lilly] bonemorph.bones.linkT , Hip_L
[Info   :     Lilly] bonemorph.bones.linkT , Hip_R
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Neck
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Neck_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Head
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L_sub
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R_sub
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Pelvis_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Skirt
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine0a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 Spine1a_SCL_
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 L Hand
[Info   :     Lilly] bonemorph.bones.linkT , Kata_L
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Clavicle
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R UpperArm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Forearm
[Info   :     Lilly] bonemorph.bones.linkT , Bip01 R Hand
[Info   :     Lilly] bonemorph.bones.linkT , Kata_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L
[Info   :     Lilly] bonemorph.bones.linkT , Mune_L_sub
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R
[Info   :     Lilly] bonemorph.bones.linkT , Mune_R_sub
[Info   :     Lilly] m_MorphBlendValues , munel , 0.74
[Info   :     Lilly] m_MorphBlendValues , munes , 0
[Info   :     Lilly] m_MorphBlendValues , munetare , 0.05
[Info   :     Lilly] m_MorphBlendValues , regfat , 0.38
[Info   :     Lilly] m_MorphBlendValues , arml , 0.33
[Info   :     Lilly] m_MorphBlendValues , hara , 0.23
[Info   :     Lilly] m_MorphBlendValues , regmeet , 0.18
[Info   :     Lilly] m_MorphBlendValues , hipsize , 0.5
[Info   :     Lilly] m_MorphBlendValues , haran , 0
[Info   :     Lilly] m_MorphBlendValues , chikubih , 0.2
[Info   :     Lilly] m_MorphBlendValues , chikubik1 , 0
[Info   :     Lilly] m_MorphBlendValues , chikubik2 , 0
[Info   :     Lilly] m_MorphBlendValues , chikubik2_munes , 0
[Info   :     Lilly] m_MorphBlendValues , chikubir , 0.1
[Info   :     Lilly] m_MorphBlendValues , chikubiw , 0.2
[Info   :     Lilly] m_MorphBlendValues , nyurin1 , 0
[Info   :     Lilly] m_MorphBlendValues , nyurin2 , 0
[Info   :     Lilly] m_MorphBlendValues , nyurin3 , 0.2
[Info   :     Lilly] m_MorphBlendValues , nyurin4 , 0
[Info   :     Lilly] m_MorphBlendValues , nyurin5 , 0
[Info   :     Lilly] m_MorphBlendValues , nyurin6 , 0
[Info   :     Lilly] m_MorphBlendValues , nyurin7 , 0
[Info   :     Lilly] m_MorphBlendValues , nyurin8 , 0
[Info   :     Lilly] m_MorphBlendValues , chikubiweartotsu , 0
[Info   :     Lilly] m_MorphBlendValues , mabutaupin , 0.5
[Info   :     Lilly] m_MorphBlendValues , mabutaupin2 , 0.5
[Info   :     Lilly] m_MorphBlendValues , mabutaupmiddle , 0.5
[Info   :     Lilly] m_MorphBlendValues , mabutaupout , 0.5
[Info   :     Lilly] m_MorphBlendValues , mabutaupout2 , 1
[Info   :     Lilly] m_MorphBlendValues , mabutalowin , 1
[Info   :     Lilly] m_MorphBlendValues , mabutalowupmiddle , 0.5
[Info   :     Lilly] m_MorphBlendValues , mabutalowupout , 1
[Info   :     Lilly] m_MorphBlendValues , ha1 , 0
[Info   :     Lilly] m_MorphBlendValues , ha2 , 0
[Info   :     Lilly] m_MorphBlendValues , ha3 , 0
[Info   :     Lilly] m_MorphBlendValues , ha4 , 0
[Info   :     Lilly] m_MorphBlendValues , ha5 , 0
[Info   :     Lilly] m_MorphBlendValues , ha6 , 0
[Info   :     Lilly] m_MorphBlendValues , hitomishapeup , 0
[Info   :     Lilly] m_MorphBlendValues , hitomishapelow , 0
[Info   :     Lilly] m_MorphBlendValues , hitomishapein , 0
[Info   :     Lilly] m_MorphBlendValues , hitomishapeoutup , 0
[Info   :     Lilly] m_MorphBlendValues , hitomishapeoutlow , 0
[Info   :     Lilly] m_MorphBlendValues , hohoshape , 0
[Info   :     Lilly] m_MorphBlendValues , lipthick , 0
[Info   :     Lilly] m_MorphBlendValues , kuikomipants , 1
[Info   :     Lilly] m_MorphBlendValues , kuikomistkg , 1
[Info   :     Lilly] m_dicTrans , _BO_body001 , _BO_body001
[Info   :     Lilly] m_dicTrans , ArmL , ArmL
[Info   :     Lilly] m_dicTrans , Bip01 , Bip01
[Info   :     Lilly] m_dicTrans , Bip01 Footsteps , Bip01 Footsteps
[Info   :     Lilly] m_dicTrans , Bip01 Pelvis , Bip01 Pelvis
[Info   :     Lilly] m_dicTrans , Bip01 Pelvis_SCL_ , Bip01 Pelvis_SCL_
[Info   :     Lilly] m_dicTrans , _IK_anal , _IK_anal
[Info   :     Lilly] m_dicTrans , _IK_hipL , _IK_hipL
[Info   :     Lilly] m_dicTrans , _IK_hipR , _IK_hipR
[Info   :     Lilly] m_dicTrans , _IK_hutanari , _IK_hutanari
[Info   :     Lilly] m_dicTrans , _IK_vagina , _IK_vagina
[Info   :     Lilly] m_dicTrans , Bip01 L Thigh , Bip01 L Thigh
[Info   :     Lilly] m_dicTrans , Bip01 L Thigh_SCL_ , Bip01 L Thigh_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 L Calf , Bip01 L Calf
[Info   :     Lilly] m_dicTrans , Bip01 L Calf_SCL_ , Bip01 L Calf_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 L Foot , Bip01 L Foot
[Info   :     Lilly] m_dicTrans , _IK_footL , _IK_footL
[Info   :     Lilly] m_dicTrans , Bip01 L Toe0 , Bip01 L Toe0
[Info   :     Lilly] m_dicTrans , Bip01 L Toe01 , Bip01 L Toe01
[Info   :     Lilly] m_dicTrans , Bip01 L Toe0Nub , Bip01 L Toe0Nub
[Info   :     Lilly] m_dicTrans , Bip01 L Toe1 , Bip01 L Toe1
[Info   :     Lilly] m_dicTrans , Bip01 L Toe11 , Bip01 L Toe11
[Info   :     Lilly] m_dicTrans , Bip01 L Toe1Nub , Bip01 L Toe1Nub
[Info   :     Lilly] m_dicTrans , Bip01 L Toe2 , Bip01 L Toe2
[Info   :     Lilly] m_dicTrans , Bip01 L Toe21 , Bip01 L Toe21
[Info   :     Lilly] m_dicTrans , Bip01 L Toe2Nub , Bip01 L Toe2Nub
[Info   :     Lilly] m_dicTrans , momotwist_L , momotwist_L
[Info   :     Lilly] m_dicTrans , momoniku_L , momoniku_L
[Info   :     Lilly] m_dicTrans , momoniku_L_nub , momoniku_L_nub
[Info   :     Lilly] m_dicTrans , momotwist2_L , momotwist2_L
[Info   :     Lilly] m_dicTrans , momotwist_L_nub , momotwist_L_nub
[Info   :     Lilly] m_dicTrans , Bip01 R Thigh , Bip01 R Thigh
[Info   :     Lilly] m_dicTrans , Bip01 R Thigh_SCL_ , Bip01 R Thigh_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 R Calf , Bip01 R Calf
[Info   :     Lilly] m_dicTrans , Bip01 R Calf_SCL_ , Bip01 R Calf_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 R Foot , Bip01 R Foot
[Info   :     Lilly] m_dicTrans , _IK_footR , _IK_footR
[Info   :     Lilly] m_dicTrans , Bip01 R Toe0 , Bip01 R Toe0
[Info   :     Lilly] m_dicTrans , Bip01 R Toe01 , Bip01 R Toe01
[Info   :     Lilly] m_dicTrans , Bip01 R Toe0Nub , Bip01 R Toe0Nub
[Info   :     Lilly] m_dicTrans , Bip01 R Toe1 , Bip01 R Toe1
[Info   :     Lilly] m_dicTrans , Bip01 R Toe11 , Bip01 R Toe11
[Info   :     Lilly] m_dicTrans , Bip01 R Toe1Nub , Bip01 R Toe1Nub
[Info   :     Lilly] m_dicTrans , Bip01 R Toe2 , Bip01 R Toe2
[Info   :     Lilly] m_dicTrans , Bip01 R Toe21 , Bip01 R Toe21
[Info   :     Lilly] m_dicTrans , Bip01 R Toe2Nub , Bip01 R Toe2Nub
[Info   :     Lilly] m_dicTrans , momotwist_R , momotwist_R
[Info   :     Lilly] m_dicTrans , momoniku_R , momoniku_R
[Info   :     Lilly] m_dicTrans , momoniku_R_nub , momoniku_R_nub
[Info   :     Lilly] m_dicTrans , momotwist2_R , momotwist2_R
[Info   :     Lilly] m_dicTrans , momotwist_R_nub , momotwist_R_nub
[Info   :     Lilly] m_dicTrans , Hip_L , Hip_L
[Info   :     Lilly] m_dicTrans , Hip_L_nub , Hip_L_nub
[Info   :     Lilly] m_dicTrans , Hip_R , Hip_R
[Info   :     Lilly] m_dicTrans , Hip_R_nub , Hip_R_nub
[Info   :     Lilly] m_dicTrans , Bip01 Spine , Bip01 Spine
[Info   :     Lilly] m_dicTrans , Bip01 Spine_SCL_ , Bip01 Spine_SCL_
[Info   :     Lilly] m_dicTrans , _IK_hara , _IK_hara
[Info   :     Lilly] m_dicTrans , Bip01 Spine0a , Bip01 Spine0a
[Info   :     Lilly] m_dicTrans , Bip01 Spine0a_SCL_ , Bip01 Spine0a_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 Spine1 , Bip01 Spine1
[Info   :     Lilly] m_dicTrans , Bip01 Spine1_SCL_ , Bip01 Spine1_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 Spine1a , Bip01 Spine1a
[Info   :     Lilly] m_dicTrans , Bip01 Spine1a_SCL_ , Bip01 Spine1a_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 L Clavicle , Bip01 L Clavicle
[Info   :     Lilly] m_dicTrans , Bip01 L Clavicle_SCL_ , Bip01 L Clavicle_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 L UpperArm , Bip01 L UpperArm
[Info   :     Lilly] m_dicTrans , Bip01 L Forearm , Bip01 L Forearm
[Info   :     Lilly] m_dicTrans , Bip01 L Hand , Bip01 L Hand
[Info   :     Lilly] m_dicTrans , _IK_handL , _IK_handL
[Info   :     Lilly] m_dicTrans , Bip01 L Finger0 , Bip01 L Finger0
[Info   :     Lilly] m_dicTrans , Bip01 L Finger01 , Bip01 L Finger01
[Info   :     Lilly] m_dicTrans , Bip01 L Finger02 , Bip01 L Finger02
[Info   :     Lilly] m_dicTrans , Bip01 L Finger0Nub , Bip01 L Finger0Nub
[Info   :     Lilly] m_dicTrans , Bip01 L Finger1 , Bip01 L Finger1
[Info   :     Lilly] m_dicTrans , Bip01 L Finger11 , Bip01 L Finger11
[Info   :     Lilly] m_dicTrans , Bip01 L Finger12 , Bip01 L Finger12
[Info   :     Lilly] m_dicTrans , Bip01 L Finger1Nub , Bip01 L Finger1Nub
[Info   :     Lilly] m_dicTrans , Bip01 L Finger2 , Bip01 L Finger2
[Info   :     Lilly] m_dicTrans , Bip01 L Finger21 , Bip01 L Finger21
[Info   :     Lilly] m_dicTrans , Bip01 L Finger22 , Bip01 L Finger22
[Info   :     Lilly] m_dicTrans , Bip01 L Finger2Nub , Bip01 L Finger2Nub
[Info   :     Lilly] m_dicTrans , Bip01 L Finger3 , Bip01 L Finger3
[Info   :     Lilly] m_dicTrans , Bip01 L Finger31 , Bip01 L Finger31
[Info   :     Lilly] m_dicTrans , Bip01 L Finger32 , Bip01 L Finger32
[Info   :     Lilly] m_dicTrans , Bip01 L Finger3Nub , Bip01 L Finger3Nub
[Info   :     Lilly] m_dicTrans , Bip01 L Finger4 , Bip01 L Finger4
[Info   :     Lilly] m_dicTrans , Bip01 L Finger41 , Bip01 L Finger41
[Info   :     Lilly] m_dicTrans , Bip01 L Finger42 , Bip01 L Finger42
[Info   :     Lilly] m_dicTrans , Bip01 L Finger4Nub , Bip01 L Finger4Nub
[Info   :     Lilly] m_dicTrans , Foretwist1_L , Foretwist1_L
[Info   :     Lilly] m_dicTrans , Foretwist_L , Foretwist_L
[Info   :     Lilly] m_dicTrans , Uppertwist1_L , Uppertwist1_L
[Info   :     Lilly] m_dicTrans , Uppertwist_L , Uppertwist_L
[Info   :     Lilly] m_dicTrans , Kata_L , Kata_L
[Info   :     Lilly] m_dicTrans , Kata_L_nub , Kata_L_nub
[Info   :     Lilly] m_dicTrans , Bip01 Neck , Bip01 Neck
[Info   :     Lilly] m_dicTrans , Bip01 Neck_SCL_ , Bip01 Neck_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 Head , Bip01 Head
[Info   :     Lilly] m_dicTrans , _IK_hohoL , _IK_hohoL
[Info   :     Lilly] m_dicTrans , _IK_hohoR , _IK_hohoR
[Info   :     Lilly] m_dicTrans , Bip01 HeadNub , Bip01 HeadNub
[Info   :     Lilly] m_dicTrans , Bip01 R Clavicle , Bip01 R Clavicle
[Info   :     Lilly] m_dicTrans , Bip01 R Clavicle_SCL_ , Bip01 R Clavicle_SCL_
[Info   :     Lilly] m_dicTrans , Bip01 R UpperArm , Bip01 R UpperArm
[Info   :     Lilly] m_dicTrans , Bip01 R Forearm , Bip01 R Forearm
[Info   :     Lilly] m_dicTrans , Bip01 R Hand , Bip01 R Hand
[Info   :     Lilly] m_dicTrans , _IK_handR , _IK_handR
[Info   :     Lilly] m_dicTrans , Bip01 R Finger0 , Bip01 R Finger0
[Info   :     Lilly] m_dicTrans , Bip01 R Finger01 , Bip01 R Finger01
[Info   :     Lilly] m_dicTrans , Bip01 R Finger02 , Bip01 R Finger02
[Info   :     Lilly] m_dicTrans , Bip01 R Finger0Nub , Bip01 R Finger0Nub
[Info   :     Lilly] m_dicTrans , Bip01 R Finger1 , Bip01 R Finger1
[Info   :     Lilly] m_dicTrans , Bip01 R Finger11 , Bip01 R Finger11
[Info   :     Lilly] m_dicTrans , Bip01 R Finger12 , Bip01 R Finger12
[Info   :     Lilly] m_dicTrans , Bip01 R Finger1Nub , Bip01 R Finger1Nub
[Info   :     Lilly] m_dicTrans , Bip01 R Finger2 , Bip01 R Finger2
[Info   :     Lilly] m_dicTrans , Bip01 R Finger21 , Bip01 R Finger21
[Info   :     Lilly] m_dicTrans , Bip01 R Finger22 , Bip01 R Finger22
[Info   :     Lilly] m_dicTrans , Bip01 R Finger2Nub , Bip01 R Finger2Nub
[Info   :     Lilly] m_dicTrans , Bip01 R Finger3 , Bip01 R Finger3
[Info   :     Lilly] m_dicTrans , Bip01 R Finger31 , Bip01 R Finger31
[Info   :     Lilly] m_dicTrans , Bip01 R Finger32 , Bip01 R Finger32
[Info   :     Lilly] m_dicTrans , Bip01 R Finger3Nub , Bip01 R Finger3Nub
[Info   :     Lilly] m_dicTrans , Bip01 R Finger4 , Bip01 R Finger4
[Info   :     Lilly] m_dicTrans , Bip01 R Finger41 , Bip01 R Finger41
[Info   :     Lilly] m_dicTrans , Bip01 R Finger42 , Bip01 R Finger42
[Info   :     Lilly] m_dicTrans , Bip01 R Finger4Nub , Bip01 R Finger4Nub
[Info   :     Lilly] m_dicTrans , Foretwist1_R , Foretwist1_R
[Info   :     Lilly] m_dicTrans , Foretwist_R , Foretwist_R
[Info   :     Lilly] m_dicTrans , Uppertwist1_R , Uppertwist1_R
[Info   :     Lilly] m_dicTrans , Uppertwist_R , Uppertwist_R
[Info   :     Lilly] m_dicTrans , Kata_R , Kata_R
[Info   :     Lilly] m_dicTrans , Kata_R_nub , Kata_R_nub
[Info   :     Lilly] m_dicTrans , Mune_L , Mune_L
[Info   :     Lilly] m_dicTrans , _IK_muneL , _IK_muneL
[Info   :     Lilly] m_dicTrans , Mune_L_sub , Mune_L_sub
[Info   :     Lilly] m_dicTrans , Mune_L_nub , Mune_L_nub
[Info   :     Lilly] m_dicTrans , Mune_R , Mune_R
[Info   :     Lilly] m_dicTrans , _IK_muneR , _IK_muneR
[Info   :     Lilly] m_dicTrans , Mune_R_sub , Mune_R_sub
[Info   :     Lilly] m_dicTrans , Mune_R_nub , Mune_R_nub
[Info   :     Lilly] m_dicTrans , body , body
[Info   :     Lilly] m_dicTrans , center , center
[Info   :     Lilly] m_dicTrans , Hara , Hara
[Info   :     Lilly] m_dicTrans , MuneL , MuneL
[Info   :     Lilly] m_dicTrans , MuneTare , MuneTare
[Info   :     Lilly] m_dicTrans , RegFat , RegFat
[Info   :     Lilly] m_dicTrans , RegMeet , RegMeet
[Info   :     Lilly] TBody.Slot , body , body , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , head , head , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , eye , eye , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairF , hairF , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairR , hairR , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS , hairS , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS , hairS , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS , hairS , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS , hairS , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS , hairS , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS_2 , hairS_2 , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS_2 , hairS_2 , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS_2 , hairS_2 , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS_2 , hairS_2 , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairS_2 , hairS_2 , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT , hairT , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT , hairT , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT , hairT , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT , hairT , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT , hairT , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT_2 , hairT_2 , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT_2 , hairT_2 , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT_2 , hairT_2 , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT_2 , hairT_2 , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairT_2 , hairT_2 , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , wear , wear , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , skirt , skirt , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , onepiece , onepiece , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , mizugi , mizugi , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , mizugi_top , mizugi_top , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , mizugi_buttom , mizugi_buttom , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , panz , panz , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , slip , slip , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , bra , bra , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , stkg , stkg , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , shoes , shoes , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , headset , headset , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , headset , headset , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , headset , headset , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , headset , headset , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , headset , headset , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , glove , glove , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , glove , glove , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , glove , glove , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , glove , glove , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , glove , glove , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead , accHead , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead , accHead , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead , accHead , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead , accHead , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead , accHead , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead_2 , accHead_2 , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead_2 , accHead_2 , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead_2 , accHead_2 , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead_2 , accHead_2 , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHead_2 , accHead_2 , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairAho , hairAho , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairAho , hairAho , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairAho , hairAho , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairAho , hairAho , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , hairAho , hairAho , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHana , accHana , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHana , accHana , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHana , accHana , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHana , accHana , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHana , accHana , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHa , accHa , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_1_ , accKami_1_ , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_1_ , accKami_1_ , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_1_ , accKami_1_ , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_1_ , accKami_1_ , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_1_ , accKami_1_ , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiR , accMiMiR , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiR , accMiMiR , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiR , accMiMiR , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiR , accMiMiR , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiR , accMiMiR , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubR , accKamiSubR , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubR , accKamiSubR , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubR , accKamiSubR , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubR , accKamiSubR , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubR , accKamiSubR , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipR , accNipR , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipR , accNipR , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipR , accNipR , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipR , accNipR , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipR , accNipR , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , HandItemR , HandItemR , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubi , accKubi , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubi , accKubi , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubi , accKubi , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubi , accKubi , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubi , accKubi , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubiwa , accKubiwa , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubiwa , accKubiwa , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubiwa , accKubiwa , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubiwa , accKubiwa , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKubiwa , accKubiwa , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHeso , accHeso , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHeso , accHeso , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHeso , accHeso , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHeso , accHeso , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHeso , accHeso , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde , accUde , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde , accUde , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde , accUde , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde , accUde , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde , accUde , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde_2 , accUde_2 , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde_2 , accUde_2 , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde_2 , accUde_2 , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde_2 , accUde_2 , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accUde_2 , accUde_2 , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi , accAshi , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi , accAshi , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi , accAshi , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi , accAshi , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi , accAshi , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi_2 , accAshi_2 , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi_2 , accAshi_2 , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi_2 , accAshi_2 , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi_2 , accAshi_2 , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAshi_2 , accAshi_2 , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accSenaka , accSenaka , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accSenaka , accSenaka , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accSenaka , accSenaka , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accSenaka , accSenaka , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accSenaka , accSenaka , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accShippo , accShippo , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accShippo , accShippo , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accShippo , accShippo , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accShippo , accShippo , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accShippo , accShippo , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKoshi , accKoshi , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKoshi , accKoshi , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKoshi , accKoshi , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKoshi , accKoshi , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKoshi , accKoshi , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accAnl , accAnl , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accVag , accVag , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accVag , accVag , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accVag , accVag , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accVag , accVag , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accVag , accVag , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kubiwa , kubiwa , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kubiwa , kubiwa , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kubiwa , kubiwa , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kubiwa , kubiwa , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kubiwa , kubiwa , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , megane , megane , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , megane , megane , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , megane , megane , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , megane , megane , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , megane , megane , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accXXX , accXXX , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accXXX , accXXX , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accXXX , accXXX , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accXXX , accXXX , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accXXX , accXXX , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , chinko , chinko , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , chikubi , chikubi , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accFace , accFace , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accFace , accFace , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accFace , accFace , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accFace , accFace , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accFace , accFace , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHat , accHat , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHat , accHat , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHat , accHat , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHat , accHat , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accHat , accHat , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kousoku_upper , kousoku_upper , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , kousoku_lower , kousoku_lower , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_naka , seieki_naka , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_hara , seieki_hara , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_face , seieki_face , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_mune , seieki_mune , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_hip , seieki_hip , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_ude , seieki_ude , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , seieki_ashi , seieki_ashi , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipL , accNipL , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipL , accNipL , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipL , accNipL , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipL , accNipL , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accNipL , accNipL , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiL , accMiMiL , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiL , accMiMiL , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiL , accMiMiL , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiL , accMiMiL , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accMiMiL , accMiMiL , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubL , accKamiSubL , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubL , accKamiSubL , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubL , accKamiSubL , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubL , accKamiSubL , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKamiSubL , accKamiSubL , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_2_ , accKami_2_ , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_2_ , accKami_2_ , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_2_ , accKami_2_ , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_2_ , accKami_2_ , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_2_ , accKami_2_ , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_3_ , accKami_3_ , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_3_ , accKami_3_ , 1 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_3_ , accKami_3_ , 2 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_3_ , accKami_3_ , 3 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , accKami_3_ , accKami_3_ , 4 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , HandItemL , HandItemL , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , underhair , underhair , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , asshair , asshair , 0 ,  , 0 , 0 , NONE ,  , 0 , 0
[Info   :     Lilly] TBody.Slot , moza , moza , 0 ,  , 0 , 0 , NONE ,  , 0 , 0 
 */