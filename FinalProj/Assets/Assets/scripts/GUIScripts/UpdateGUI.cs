using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CharacterWeaponFramework;
using System.Collections.Generic;
using System.Text;
using Globals;

namespace GUIScripts
{
    public class UpdateGUI : MonoBehaviour
    {
        [SerializeField]
        private Group _playerGroup;


        void OnLevelWasLoaded()
        {
            _playerGroup = GlobalGameInfo.PlayerGroupData;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Canvas can = this.GetComponentInChildren<Canvas>();
            Text text = (Text)can.GetComponentInChildren<Text>();
            List<CharacterData> charData = _playerGroup.GroupMembersCharacterData;
            Vector3 avgOfGroup = _playerGroup.avgOfGroup;

            StringBuilder s = new StringBuilder();
            int i = 0;
            for (i = 0; i < charData.Count; i++)
            {
                s.Append(charData[i].Name + ",");
                s.Append(" HP:" + charData[i].CurHP.ToString("F0") + "/" + charData[i].MaxHP);
                s.Append(" MP:" + charData[i].CurMP.ToString("F0") + "/" + charData[i].MaxMP);
                s.Append(" Stamina:" + charData[i].CurStamina.ToString("F0") + "/" + charData[i].MaxStamina);
                s.Append(" Alive:" + charData[i].Alive);
                s.Append("\n");

            }

            text.text = s.ToString();

        }
    }
}

