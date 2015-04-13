using Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters;

namespace CharacterWeaponFramework
{
    public class Group : MonoBehaviour
    {
        [SerializeField]
        private List<ThirdPersonCharacter> _groupMembers;
        [SerializeField]
        private List<CharacterData> _groupMemberData;
        [SerializeField]
        private List<GameObject> _GroupMemberGameObjects;
        public GameObject _LeadMember;
        public GameObject _DefaultGroupMember;
        public GUIText _GroupMembersText;
        public Vector3 _Position;
        public Vector3 avgOfGroup;

        public Group()
        {}

        void Start()
        {
            _Position = new Vector3();
            _groupMembers = new List<ThirdPersonCharacter>();
            _GroupMemberGameObjects = new List<GameObject>();
            //create the GameObject
            UnityEngine.Object t = Instantiate(_LeadMember, new Vector3(0f, 0f, 0f), new Quaternion());
            this.AddCharacter((GameObject)t);

            //create the GameObject
            /*Instantiate(_DefaultGroupMember, _groupMembers[0].transform.position, _groupMembers[0].transform.rotation);
            this.AddCharacter(temp);*/
        }

        public bool AddCharacter(GameObject newCharacter)
        {
            if(_groupMembers.Count == 0)
            {
                _GroupMemberGameObjects.Add(newCharacter);
                
                //store the CharacterData script Components
                Component t = newCharacter.GetComponent("CharacterData");
                CharacterData temp = (CharacterData)t;
                _groupMemberData.Add(temp);

                //store the ThirdPersonCharacter script Components
                t = newCharacter.GetComponent("ThirdPersonCharacter");
                ThirdPersonCharacter temp2 = (ThirdPersonCharacter)t;
                _groupMembers.Add(temp2);
                return true;
            }
            else if ( _groupMembers.Count < GlobalConsts.MAX_GROUP_SIZE)
            {

                return true;
            }
            return false;
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

        void FixedUpdate()
        {
            StringBuilder s = new StringBuilder();
            //s.Append(_groupMemberData[0].Name + ", ");
           /* Component t;
            CharacterData dat;*/
            int i = 0;
            for(i=0;i<_groupMembers.Count;i++)
            {
                /*t = _GroupMembers[i].GetComponent("CharacterData");
                dat = (CharacterData)t;*/
                s.Append(_groupMemberData[i].Name + ", ");

            }
            s.Append("\n");

            avgOfGroup = new Vector3(0f,0f,0f);
            for(i=0;i<_groupMemberData.Count;i++)
            {
                /*t = _GroupMembers[i].GetComponent("CharacterData");
                dat = (CharacterData)t;*/
                avgOfGroup += _groupMemberData[i].Position;
                s.Append(_groupMemberData[i].Position.ToString() + ", ");
            }
            avgOfGroup /= _groupMemberData.Count;
            _Position = avgOfGroup;
            
            s.Append("\nAvgPosOfGroup ");
            s.Append(_Position);
            _GroupMembersText.text = s.ToString();
        }
    }
}
