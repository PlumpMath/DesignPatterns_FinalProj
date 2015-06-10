using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CharacterScripts;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Globals;

namespace GUIScripts
{
    public class TargetsPanelHandlerGUI : TemplatePanelHandlerGUI
    {
        delegate void AddEffectToButton(BaseEventData baseEvent);

        private List<GameObject> _Buttons;
        private Group _grp;
        private static int numMovesAllowed;
        private static int numMoves;


        void Awake()
        {
            _Buttons = new List<GameObject>();
            numMovesAllowed = GlobalGameInfo.PlayerGroupData.GroupMembersCharacterData.Count;
        }

        protected override void Hook1()
        {
            _grp = GlobalGameInfo.enemyGroup;
        }

        public static void ResetMoveCounter()
        {
            numMoves = 0;
        }

        protected override int ConstructButtons()
        {
            int i = 0;
            //Debug.Log("_grp.GroupMembersCharacterData.Count:" + _grp.GroupMembersCharacterData.Count);
            for (i = 0; i < _grp.GroupMembersGameObjects.Count;i++ )
            {
                GameObject targetButton = Instantiate(_Button);
                targetButton.transform.SetParent(this.gameObject.transform, false);
                _Buttons.Add(targetButton);
                Button but = targetButton.GetComponent<Button>();
                

                RectTransform butTrans = targetButton.GetComponent<RectTransform>();
                butTrans.anchoredPosition3D = new Vector3(ButtonWidth / 2.0f, -(i * ButtonHeight) - (ButtonHeight / 2.0f), 0);

                TargetButtonInfo info = targetButton.GetComponent<TargetButtonInfo>();
                info.DisplayString = _grp.GroupMembersCharacterData[i].Name;
                info.Data = _grp.GroupMembersCharacterData[i];
                info.TargetNum = i;
                info.Button = but;


                AddEvents(targetButton);
            }


            return i;
        }

        private void AddEvents(GameObject targetButton)
        {
            EventTrigger eventTrigger = targetButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerClick;

            entry.callback = new EventTrigger.TriggerEvent();

            UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(ButtonCallback);
            entry.callback.AddListener(callback);

            eventTrigger.delegates.Add(entry);
        }

        public void AddEffectToButtons(string eff)
        {
            int i;
            for (i = 0; i < _Buttons.Count; i++)
            {
                TargetButtonInfo info = _Buttons[i].GetComponent<TargetButtonInfo>();

                info.Effect = eff;
            }
        }

        public void ButtonCallback(BaseEventData eventData)
        {
            if(numMoves < numMovesAllowed)
            {
                GameObject but = eventData.selectedObject;
                //Debug.Log(but.ToString() +", "+ but.GetType());
                TargetButtonInfo info = but.GetComponent<TargetButtonInfo>();

                GameObject turnsBtn = GameObject.FindGameObjectWithTag("TurnButton");
                //Debug.Log("info.TargetNum:" + info.TargetNum);
                turns t = turnsBtn.GetComponent<turns>();
                System.Random rand = new System.Random();
                int tmp = GlobalGameInfo.PlayerGroupData.GroupMembersCharacterData.Count - 1;
                int r = rand.Next(tmp);

                t.AddAction(info.Effect, GlobalGameInfo.PlayerGroupData.GroupMembersCharacterData[r], _grp.GroupMembersCharacterData[info.TargetNum]);
                numMoves++;
            }
            else
            {
                Debug.Log("Max moves allowed performed");
            }
        }
    }
}

