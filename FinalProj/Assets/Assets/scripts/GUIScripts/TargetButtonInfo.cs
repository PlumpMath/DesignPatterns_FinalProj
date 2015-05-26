using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CharacterWeaponFramework;
using Globals;

namespace GUIScripts
{
    public class TargetButtonInfo : MonoBehaviour
    {
        
        private string _DisplayString;
        private CharacterData _Data;
        [SerializeField]
        private int _targetNum;
        private Button _but;
        private Group _grp;
        private string _eff;

        public TargetButtonInfo()
        {
            _targetNum = -1;
            _grp = GlobalGameInfo.PlayerGroupData;
            _eff = "NullEffect";
        } 

        public string Effect
        {
            get { return _eff; }
            set
            {
                _eff = value;
            }
        }

        public Button Button
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
                    Debug.LogWarning("TargetButtonInfo: Button is already set!");
                }
            }
        }

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
                    Debug.LogWarning("TargetButtonInfo: DisplayString already set!");
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
                    Debug.LogWarning("TargetButtonInfo: Data already set!");
                }
            }
        }

        public int TargetNum
        {
            get { return _targetNum; }
            set
            {
                if(_targetNum != -1)
                {
                    Debug.LogWarning("TargetButtonInfo: TargetNum is already set!");
                }
                else
                {
                    _targetNum = value;
                }
            }
        }
    }
}

