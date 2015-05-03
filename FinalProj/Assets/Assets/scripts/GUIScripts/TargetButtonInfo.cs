using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CharacterWeaponFramework;


namespace GUIScripts
{
    public class TargetButtonInfo : MonoBehaviour
    {
        private string _DisplayString;
        private CharacterData _Data;

        public string DisplayString
        {
            get { return _DisplayString; }
            set
            {
                if (_DisplayString == null)
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

        public CharacterData Data
        {
            get { return _Data; }
            set
            {
                if(_Data == null)
                {
                    _Data = value;
                }
                else
                {
                    Debug.LogWarning("Data already set!");
                }
            }
        }

        void Start()
        {

        }

    }
}

