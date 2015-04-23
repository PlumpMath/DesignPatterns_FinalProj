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
        private List<ThirdPersonCharacter> _groupMembersThirdPersonCharacter;
        [SerializeField]
        private List<CharacterData> _groupMemberCharacterData;
        [SerializeField]
        private List<GameObject> _GroupMemberGameObjects;
        
        /*Unity wants these to be public*/
        //public List<GameObject> _MemberTypes;
        public GameObject _LeadMember;
        public GameObject _DefaultGroupMembers;
        //public GUIText _GroupMembersText;
        public Vector3 avgOfGroup;

        public List<ThirdPersonCharacter> GroupMembersThirdPersonCharacter
        {
            get { return _groupMembersThirdPersonCharacter; }
        }

        public List<CharacterData> GroupMembersCharacterData
        {
            get { return _groupMemberCharacterData; }
        }


        public Group()
        {}

        void Start()
        {
            avgOfGroup = new Vector3();
            _groupMembersThirdPersonCharacter = new List<ThirdPersonCharacter>();
            _GroupMemberGameObjects = new List<GameObject>();
            //create the GameObject
            UnityEngine.Object t = Instantiate(_LeadMember, new Vector3(0f, 0f, 0f), new Quaternion());
            this.AddCharacter((GameObject)t);

            //create the GameObject
            /*Instantiate(_DefaultGroupMember, _groupMembers[0].transform.position, _groupMembers[0].transform.rotation);
            this.AddCharacter(temp);*/
        }

        public void AddCharacter(GameObject newCharacter)
        {
            if (_groupMembersThirdPersonCharacter.Count < GlobalConsts.MAX_GROUP_SIZE)
            {
                _GroupMemberGameObjects.Add(newCharacter);
                
                //store the CharacterData script Components
                Component t = newCharacter.GetComponent("CharacterData");
                CharacterData temp = (CharacterData)t;
                _groupMemberCharacterData.Add(temp);

                //store the ThirdPersonCharacter script Components
                t = newCharacter.GetComponent("ThirdPersonCharacter");
                ThirdPersonCharacter temp2 = (ThirdPersonCharacter)t;
                _groupMembersThirdPersonCharacter.Add(temp2);
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
            int i = 0;
            avgOfGroup = new Vector3(0f, 0f, 0f);
            for (i = 0; i < _groupMemberCharacterData.Count; i++)
            {
                /*t = _GroupMembers[i].GetComponent("CharacterData");
                dat = (CharacterData)t;*/
                avgOfGroup += _groupMemberCharacterData[i].Position;
            }
            avgOfGroup /= _groupMemberCharacterData.Count;
        }
    }
}
