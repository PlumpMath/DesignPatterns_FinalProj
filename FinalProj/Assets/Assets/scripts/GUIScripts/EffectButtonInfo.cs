using UnityEngine;
using System.Collections;
using CharacterWeaponFramework;
using System;
using UnityEngine.UI;

namespace GUIScripts
{
    public class EffectButtonInfo : MonoBehaviour
    {
        private string _DisplayString;
        private string _InternalNameString;
        private Button _but;

        void Start()
        {

        }

        public string DisplayString
        {
            get { return _DisplayString; }
            set
            {
                if(_DisplayString == null)
                {
                    _DisplayString = value;
                    Text t = GetComponentInChildren<Text>();
                    t.text = value;
                }
                else
                {
                    Debug.LogWarning("DisplayString already set!");
                }
            }
        }

        public string InternalNameString
        {
            get { return _InternalNameString; }
            set
            {
                if(_InternalNameString == null)
                {
                    _InternalNameString = value;
                }
                else
                {
                    Debug.LogWarning("InternalNameString already set!");
                }
            }
        }

        public Button Btn
        {
            get { return _but; }
            set
            {
                if(_but == null)
                {
                    _but = value;
                }
                else
                {
                    Debug.LogWarning("Btn already set!");
                }
            }
        }
    }
}

