using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class ProfileCtrlPapch
    {
        /*
            Allows to edit maid names in Maid Edit after they have been created.
            Needs ScriptLoader to work.
        */

        [HarmonyPatch(typeof(ProfileCtrl), "SetEnableInput")]
        [HarmonyPrefix]
        public static void OnSetEnableInput(ref bool enabledInput)
        {
            enabledInput = true;
        }
    }
}
