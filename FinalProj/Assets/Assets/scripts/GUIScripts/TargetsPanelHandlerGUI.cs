using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CharacterWeaponFramework;
using UnityEngine.UI;

namespace GUIScripts
{
    public class TargetsPanelHandlerGUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _TargetButton;
        private EffectFactory _EffFact;
        private List<GameObject> _Buttons;
        private Group _grp;

        private const int ButtonHeight = 30;
        private const int ButtonWidth = 160;
        private const int MaxPanelHeight = ButtonHeight * 10;

        void Awake()
        {
            _EffFact = new EffectFactory();
            _Buttons = new List<GameObject>();

            
        }

        void OnLevelWasLoaded()
        {
            _grp = GameStateInfo.PlayerGroupData;
            int numButtons = ConstructButtons();
        }


        private int ConstructButtons()
        {
            int i = 0;

            for (i = 0; i < _grp.GroupMembersCharacterData.Count;i++ )
            {
                GameObject targetButton = Instantiate(_TargetButton);
                targetButton.transform.SetParent(this.gameObject.transform, false);
                _Buttons.Add(targetButton);
                
                RectTransform butTrans = targetButton.GetComponent<RectTransform>();
                butTrans.anchoredPosition3D = new Vector3(ButtonWidth / 2.0f, -(i * ButtonHeight) - (ButtonHeight / 2.0f), 0);

                TargetButtonInfo info = targetButton.GetComponent<TargetButtonInfo>();
                info.DisplayString = _grp.GroupMembersCharacterData[i].Name;
                info.Data = _grp.GroupMembersCharacterData[i];

            }

            return i;
        }

        public void AddEffectToButtons(string eff)
        {
            int i;
            for (i = 0; i < _Buttons.Count; i++)
            {
                Button but = _Buttons[i].GetComponent<Button>();
                but.onClick.AddListener(
                    () =>
                    {
                        _EffFact.CreateEffect(eff, _grp.GroupMembersCharacterData[i]);
                    });

            }
        }
    }
}

