using UnityEngine;
using System.Collections;
using FSM;
using AI;

namespace CharacterScripts
{
    public class MoveToGroupLeaderState : FSMState
    {
        public MoveToGroupLeaderState()
        {
            _stateID = StateID.MoveingToGroupLeaderStateID;        
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            AICharacterControl ai = npc.GetComponent<AICharacterControl>();
            if (Vector3.Distance(player.transform.position, npc.transform.position) < ai.PersonalSpace)
            {
                ai.SetTransition(Transition.TransitionToStandingStillState);
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            AICharacterControl ai = npc.GetComponent<AICharacterControl>();
            ThirdPersonCharacter character = npc.GetComponent<ThirdPersonCharacter>();
            ai.agent.SetDestination(player.transform.position);
            character.Move(ai.agent.desiredVelocity, false, false);
        }
    }
}

