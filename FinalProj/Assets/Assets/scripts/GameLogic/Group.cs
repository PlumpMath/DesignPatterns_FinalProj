using Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using AI;

namespace CharacterScripts
{
    public class Group : MonoBehaviour
    {
        [SerializeField]
        private List<CharacterData> _groupMemberCharacterData;
        [SerializeField]
        private List<GameObject> _GroupMemberGameObjects;
        
        /*Unity wants these to be public*/
        //public List<GameObject> _MemberTypes;
        //public GameObject _LeadMember;
        //public GameObject _DefaultGroupMembers;
        //public GUIText _GroupMembersText;
        public Vector3 avgOfGroup;

        public List<CharacterData> GroupMembersCharacterData
        {
            get { return _groupMemberCharacterData; }
        }

        public List<GameObject> GroupMembersGameObjects
        {
            get { return _GroupMemberGameObjects; }
        }

        public GameObject Leader
        {
            get 
            {
                //Debug.Log(_GroupMemberGameObjects[0].ToString());
                return _GroupMemberGameObjects[0];
            }

        }
        public Group()
        {
            Debug.Log("Group: Group Created");
            _GroupMemberGameObjects = new List<GameObject>();
            _groupMemberCharacterData = new List<CharacterData>();
            avgOfGroup = new Vector3();
        }

        void OnLevelWasLoaded()
        {
            //clean up these on load since they get repopulated in LoadPlayerGroup
            _groupMemberCharacterData = new List<CharacterData>();
            avgOfGroup = new Vector3();

            LoadPlayerGroup();
        }

        private void LoadPlayerGroup()
        {
            int i = 0;
            for(i=0;i<GlobalGameInfo.PlayerGroupData._GroupMemberGameObjects.Count;i++)
            {
                UnityEngine.Object tmp = null;
                tmp = Instantiate(GlobalGameInfo.PlayerGroupData._GroupMemberGameObjects[i], new Vector3(0f, 0f, 0f), new Quaternion());
                GameObject newGO = null;
                newGO = (GameObject)tmp;

                GlobalGameInfo.PlayerGroupData._GroupMemberGameObjects[i] = newGO;

                //set the target for any AI characters to the member in front of them in the group
                AICharacterControl AI = null;
                AI = newGO.GetComponent<AICharacterControl>();
                if (AI != null && GlobalGameInfo.PlayerGroupData._GroupMemberGameObjects.Count > 0)
                {
                    AI.target = GlobalGameInfo.PlayerGroupData._GroupMemberGameObjects[i - 1];
                }

                //store the CharacterData script Components
                Component t = null;
                t = newGO.GetComponent("CharacterData");
                CharacterData temp = null;
                temp = (CharacterData)t;
                GlobalGameInfo.PlayerGroupData._groupMemberCharacterData.Add(temp);
            }
        }

        public void AddCharacter(GameObject newCharacter)
        {
            if (_GroupMemberGameObjects.Count < GlobalConsts.MAX_GROUP_SIZE)
            {
                _GroupMemberGameObjects.Add(newCharacter);
                CharacterData t = newCharacter.GetComponent<CharacterData>();
                _groupMemberCharacterData.Add(t);
            }
        }

        

        /*public bool RemoveCharacter(GameObject removeCharacter)
        {
            
            if(_groupMembers.Contains(removeCharacter))
            {
                _groupMembers.Remove(removeCharacter);
                return true;
            }
            return false;
        }*/

        void Update()
        {
            UpdateAvgPositionOfGroup();
        }

        public void UpdateAvgPositionOfGroup()
        {
            int i = 0;
            avgOfGroup = new Vector3(0f, 0f, 0f);
            for (i = 0; i < _groupMemberCharacterData.Count; i++)
            {
                /*t = _GroupMembers[i].GetComponent("CharacterData");
                dat = (CharacterData)t;*/
                avgOfGroup += _GroupMemberGameObjects[i].transform.position;
            }
            avgOfGroup /= _groupMemberCharacterData.Count;
            this.gameObject.transform.position = avgOfGroup;
        }
    }
}
