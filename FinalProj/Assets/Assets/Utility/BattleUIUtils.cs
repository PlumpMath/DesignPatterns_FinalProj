using UnityEngine;
using System.Collections;
using Globals;
using GUIScripts;
using System;


namespace Utils
{
    public class BattleUIUtils: MonoBehaviour
    {
        [SerializeField]
        private GameObject _EffectGUIPrefab;
        [SerializeField]
        private GameObject _AttackGUIPrefab;

        private static BattleUIUtils _battleUiSingleton = new BattleUIUtils();
        private static GameObject effectBtnPanel;
        private static GameObject targetBtnPanel;
        public static void ToggleBattleUI()
        {
            foreach (GameObject but in GlobalGameInfo.BattleUI)
            {
                PanelHandler.StaticTogglePanel(but);
            }
        }
        void Start()
        {
            _battleUiSingleton._EffectGUIPrefab = _EffectGUIPrefab;
            _battleUiSingleton._AttackGUIPrefab = _AttackGUIPrefab;
        }

        public static void InstantiateBattleUI()
        {
            GameObject MainHUD = GameObject.FindGameObjectWithTag("MainHUD");
            GameObject effectGUI = Instantiate(_battleUiSingleton._EffectGUIPrefab);
            effectGUI.transform.SetParent(MainHUD.transform, false);
            GameObject attackGUI = Instantiate(_battleUiSingleton._AttackGUIPrefab);
            attackGUI.transform.SetParent(MainHUD.transform, false);
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
                //this warning can be ignored if it isn't causing issues
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
                //this warning can be ignored if it isn't causing issues
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

