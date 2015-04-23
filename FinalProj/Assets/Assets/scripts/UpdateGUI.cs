using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CharacterWeaponFramework;
using System.Collections.Generic;
using System.Text;

public class UpdateGUI : MonoBehaviour 
{
    [SerializeField]
    private Group _playerGroup;
    
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        Canvas can = this.GetComponentInChildren<Canvas>();
        Text text = (Text)can.GetComponentInChildren<Text>();
        List<CharacterData> charData = _playerGroup.GroupMembersCharacterData;
        Vector3 avgOfGroup = _playerGroup.avgOfGroup;

        StringBuilder s = new StringBuilder();
        int i = 0;
        for (i = 0; i < charData.Count; i++)
        {
            /*t = _GroupMembers[i].GetComponent("CharacterData");
            dat = (CharacterData)t;*/
            s.Append(charData[i].Name + ", ");

        }
        s.Append("\n");

        //avgOfGroup = new Vector3(0f, 0f, 0f);
        for (i = 0; i < charData.Count; i++)
        {
            /*t = _GroupMembers[i].GetComponent("CharacterData");
            dat = (CharacterData)t;*/
            s.Append(charData[i].Position.ToString() + ", ");
        }
        //avgOfGroup /= charData.Count;

        s.Append("\nAvgPosOfGroup ");
        s.Append(avgOfGroup.ToString());
        text.text = s.ToString();
	}
}
