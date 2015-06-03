using UnityEngine;
using System.Collections;
using Globals;
using GUIScripts;
using System;


namespace Utils
{
    public class BattleUIUtils
    {
        private static GameObject effectBtnPanel;
        private static GameObject targetBtnPanel;
        public static void ToggleBattleUI()
        {
            foreach (GameObject but in GlobalGameInfo.BattleUI)
            {
                PanelHandler.StaticTogglePanel(but);
            }
        }

        private static void SetEffectBtnPanel()
        {
            GameObject tmp = GameObject.FindGameObjectWithTag("EffectButtonsPanel");
            if(tmp!=null)
            {
                effectBtnPanel = tmp;
            }
            else
            {
                Debug.LogWarning("BatteUIUtils: EffectButtonsPanel can't be found.");
            }
        }

        private static void SetTargetBtnPanel()
        {
            GameObject tmp = GameObject.FindGameObjectWithTag("TargetButtonsPanel");
            if(tmp != null)
            {
                targetBtnPanel = tmp;
            }
            else
            {
                Debug.LogWarning("BattleUIUtils: TargetButtonsPanel can't be found.");
            }
        }


        public static void ToggleEffectSubPanels()
        {
            SetEffectBtnPanel();
            SetTargetBtnPanel();
            PanelHandler.StaticTogglePanel(effectBtnPanel);
            PanelHandler.StaticTogglePanel(targetBtnPanel);
        }

        public static void SetEffectSubPanelsToOff()
        {
            SetEffectBtnPanel();
            SetTargetBtnPanel();
            effectBtnPanel.SetActive(false);
            targetBtnPanel.SetActive(false);
        }
    }   
}

