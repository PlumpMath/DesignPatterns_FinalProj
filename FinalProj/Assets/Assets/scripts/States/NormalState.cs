using UnityEngine;
using System.Collections;
using FSM;
using AI;

namespace CharacterWeaponFramework
{
    public class NormalState : FSMState
    {
        public NormalState()
        {
            _stateID = StateID.NormalStateID;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            //enemy inside Battle Radius
            ThirdPersonUserControl control = player.GetComponent<ThirdPersonUserControl>();
            if (Vector3.Distance(player.transform.position, npc.transform.position) < control.BRadius)
            {
                control.SetTransition(Transition.TransitionToBattleState);
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            //turn off button input as a work around 
        }
    }
}