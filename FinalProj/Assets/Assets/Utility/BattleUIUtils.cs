using UnityEngine;
using System.Collections;
using Globals;
using GUIScripts;


namespace Utils
{
    public class BattleUIUtils
    {
        public static void ToggleBattleUI()
        {
            foreach (GameObject but in GlobalGameInfo.BattleUI)
            {
                PanelHandler pan = but.GetComponentInParent<PanelHandler>();
                pan.TogglePanel(but);
            }
        }
    }   
}

