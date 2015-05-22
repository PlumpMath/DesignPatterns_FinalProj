using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CharacterWeaponFramework;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Globals;

namespace GUIScripts
{
    public class TargetsPanelHandlerGUI : MonoBehaviour
    {
        delegate void AddEffectToButton(BaseEventData baseEvent);

        [SerializeField]
        private GameObject _TargetButton;
        private List<GameObject> _Buttons;
        private Group _grp;

        private const int ButtonHeight = 30;
        private const int ButtonWidth = 160;
        private const int MaxPanelHeight = ButtonHeight * 10;

        void Awake()
        {

            _Buttons = new List<GameObject>();

            
        }

        void Start()
        {
            _grp = GlobalGameInfo.PlayerGroupData;
            int numButtons = ConstructButtons();
        }


        private int ConstructButtons()
        {
            int i = 0;
            //Debug.Log("_grp.GroupMembersCharacterData.Count:" + _grp.GroupMembersCharacterData.Count);
            for (i = 0; i < _grp.GroupMembersCharacterData.Count;i++ )
            {
                GameObject targetButton = Instantiate(_TargetButton);
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
            GameObject but = eventData.selectedObject;
            Debug.Log(but.ToString() +", "+ but.GetType());
            TargetButtonInfo info = but.GetComponent<TargetButtonInfo>();

            //Debug.Log("info.TargetNum:" + info.TargetNum);

            GlobalGameInfo.EffFact.CreateEffect(info.Effect, GlobalGameInfo.PlayerGroupData.GroupMembersCharacterData[info.TargetNum]);
        }
    }
}

