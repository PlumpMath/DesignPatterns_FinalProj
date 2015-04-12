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
        private List<Character> _groupMembers;
        public GameObject _LeadMember;
        public GameObject _DefaultGroupMember;
        public GUIText _GroupMembersText;
        public Vector3 _Position;

        public Group()
        {}

        void Start()
        {
            _Position = new Vector3();
            _groupMembers = new List<Character>();
            //create the GameObject
            Instantiate(_LeadMember, new Vector3(0f, 0f, 0f), new Quaternion());
            //store the Character script Components
            Component t = _LeadMember.GetComponent("ThirdPersonCharacter");
            ThirdPersonCharacter temp = (ThirdPersonCharacter)t;
            temp.Name = "Player";
            this.AddCharacter(temp);

            //create the GameObject
            Instantiate(_DefaultGroupMember, _groupMembers[0].transform.position, _groupMembers[0].transform.rotation);
            //store the Character script Components
            t = _DefaultGroupMember.GetComponent("ThirdPersonCharacter");
            temp = (ThirdPersonCharacter)t;
            temp.Name = "Ai";
            this.AddCharacter(temp);
        }

        public bool AddCharacter(Character newCharacter)
        {
            if(_groupMembers.Count < GlobalConsts.MAX_GROUP_SIZE)
            {
                _groupMembers.Add(newCharacter);
                return true;
            }
            return false;
        }

        public bool RemoveCharacter(Character removeCharacter)
        {
            if(_groupMembers.Contains(removeCharacter))
            {
                _groupMembers.Remove(removeCharacter);
                return true;
            }
            return false;
        }

        void FixedUpdate()
        {
            StringBuilder s = new StringBuilder();
            s.Append(_groupMembers[0].Name + ", ");
            
            int i = 1;
            for(i=1;i<_groupMembers.Count;i++)
            {
                s.Append(_groupMembers[i].Name + ", ");

            }
            s.Append("\n");

            Vector3 avgOfGroup = new Vector3(0f,0f,0f);
            for(i=0;i<_groupMembers.Count;i++)
            {
                avgOfGroup += _groupMembers[i].Position;
                s.Append(_groupMembers[i].transform.position.ToString() + ", ");
            }
            avgOfGroup /= _groupMembers.Count;
            _Position = avgOfGroup;
            
            s.Append("\nAvgPosOfGroup ");
            s.Append(_Position);
            _GroupMembersText.text = s.ToString();
        }
    }
}
