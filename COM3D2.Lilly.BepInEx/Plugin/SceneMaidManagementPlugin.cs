using COM3D2API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    class SceneMaidManagementPlugin
    {        
        private static bool isPossible ;
        static string name;
        private static bool buttonAdded1 = false;


        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name== "SceneScenarioSelect")
            {
                isPossible = true;
            }
            else
            {
                isPossible = false;
            }

            if (!buttonAdded1)
            {
                name = "SceneScenarioSelectPlugin";
                SystemShortcutAPI.AddButton(name, new Action(OnMenuButtonClickCallback_SceneScenarioSelectPlugin), name, GearMenuAddPlugin.png);
                buttonAdded1 = true;
            }


        }


        private static void OnMenuButtonClickCallback_SceneScenarioSelectPlugin()
        {
            SceneMaidManagementPlugin.RemoveEventEndFlag();
        }

        public static void RemoveEventEndFlag()
        {
            if (isPossible)
            {
                ScenarioDataUtill.RemoveEventEndFlag(SceneEdit.Instance.maid);            
            }
        }

    }
}
