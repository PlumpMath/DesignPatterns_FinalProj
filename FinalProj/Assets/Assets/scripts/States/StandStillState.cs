using UnityEngine;
using System.Collections;
using FSM;

namespace CharacterWeaponFramework
{
    public class StandStillState : FSMState
    {

        public StandStillState()
        {
            _stateID = StateID.StandingStillStateID;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            AICharacterControl ai = npc.GetComponent<AICharacterControl>();
            if(Vector3.Distance(player.transform.position,npc.transform.position) < ai.PersonalSpace)
            {
                ai.SetTransition(Transition.TransitionToStandingStillState);
            }

        }

        public override void Act(GameObject player, GameObject npc)
        {
            ThirdPersonCharacter character = npc.GetComponent<ThirdPersonCharacter>();
            character.Move(Vector3.zero, false, false);
        }
    }
}

