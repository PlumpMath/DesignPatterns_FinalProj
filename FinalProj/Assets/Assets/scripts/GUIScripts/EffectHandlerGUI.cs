using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CharacterWeaponFramework;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Globals;

namespace GUIScripts
{
    public class EffectHandlerGUI : TemplatePanelHandlerGUI
    {
        /*[SerializeField]
        private GameObject _EffectButton;*/
        [SerializeField]
        private GameObject _TargetsPanel;
        private List<Button> _Buttons;

        /*private const int ButtonHeight = 30;
        private const int ButtonWidth = 160;
        private const int MaxPanelHeight = ButtonHeight * 10;*/

        
        void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);
            _Buttons = new List<Button>();
        }

        /*void Start()
        {
            int numButtons = ConstructButtons();
            ResizePanelToButtons(numButtons);
        }*/

        protected override int ConstructButtons()
        {
            int i = 0;

            TargetsPanelHandlerGUI targetsPanel = _TargetsPanel.GetComponent<TargetsPanelHandlerGUI>();
            for (i = 0; i < GlobalGameInfo.EffFact.FactSize; i++)
            {
                GameObject butObj = Instantiate<GameObject>(_Button);
                
                butObj.transform.SetParent(this.gameObject.transform, false);
                RectTransform butTrans = butObj.GetComponent<RectTransform>();
                butTrans.anchoredPosition3D = new Vector3(ButtonWidth / 2.0f, -(i * ButtonHeight) - (ButtonHeight / 2.0f), 0);


                EffectButtonInfo info = butObj.GetComponent<EffectButtonInfo>();
                Button but = butObj.GetComponent<Button>();
                info.DisplayString = GlobalGameInfo.EffFact.GetDisplayString(i);
                info.InternalNameString = GlobalGameInfo.EffFact.GetInternalName(i);
                //GameObject TargetButtonsPanel = GameObject.FindGameObjectWithTag("TargetButtonsPanel");
                but.onClick.AddListener(
                    () =>
                    {
                        targetsPanel.AddEffectToButtons(info.InternalNameString); 
                        
                    });

                info.Btn = but;

                _Buttons.Add(but);
            }

            return i;
        }

        /*private void ResizePanelToButtons(int numButtons)
        {
            float tmp = (2 * ButtonWidth) - (ButtonWidth / 2.0f);
            RectTransform trans = this.GetComponent<RectTransform>();
            trans.sizeDelta = new Vector2(ButtonWidth, (numButtons * ButtonHeight));

            float temp = numButtons * ButtonHeight / 2;

            trans.anchoredPosition3D = new Vector3(tmp, -temp, 0);
        }*/

    }
}

