using UnityEngine;
using System.Collections;
using FSM;
using AI;
using GUIScripts;
using Globals;
using Utils;

namespace CharacterScripts
{
    public class NormalState : FSMState
    {
        public NormalState()
        {
            _stateID = StateID.NormalStateID;
        }

        public override void Reason(GameObject player, GameObject group)
        {
            //enemy inside Battle Radius
            ThirdPersonUserControl control = player.GetComponent<ThirdPersonUserControl>();
            
            if (Vector3.Distance(player.transform.position, group.transform.position) < control.BRadius)
            {
                GlobalGameInfo.enemyGroup = group.GetComponent<Group>();
                control.SetTransition(Transition.TransitionToBattleState);
                control.transform.LookAt(group.transform.position);
                
            }

        }

        public override void Act(GameObject player, GameObject npc)
        {
            
            
        }
    }
}