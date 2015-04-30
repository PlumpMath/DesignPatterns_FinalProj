using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CharacterWeaponFramework;
using UnityEngine.UI;

namespace GUIScripts
{
    public class EffectHandlerGUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _EffectButton;
        private EffectFactory _EffFact;
        private List<Button> _Buttons;

        private const int ButtonHeight = 30;

        // Use this for initialization
        void Start()
        {
            _EffFact = new EffectFactory();
            _Buttons = new List<Button>();

            //construct the buttons that can be used to apply effects
            int i = 0;
            for(i=0;i<_EffFact.FactSize;i++)
            {
                GameObject butObj = Instantiate<GameObject>(_EffectButton);
                butObj.transform.SetParent(this.gameObject.transform,false);
                butObj.transform.localPosition -= new Vector3(0, ButtonHeight * i, 0);
                EffectButtonInfo info = butObj.GetComponent<EffectButtonInfo>();
                Button but = butObj.GetComponent<Button>();
                info.DisplayString = _EffFact.GetDisplayString(i);
                info.InternalNameString = _EffFact.GetInternalName(i);
                but.onClick.AddListener(
                    () =>
                    {
                        _EffFact.CreateEffect(info.InternalNameString, GameStateInfo.PlayerGroupData.GroupMembersCharacterData[0]);
                    });

                info.Btn = but;

                _Buttons.Add(but);
            }
        }

        void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);
        }

    }
}

