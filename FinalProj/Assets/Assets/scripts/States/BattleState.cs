using UnityEngine;
using System.Collections;
using FSM;
using AI;

namespace CharacterScripts
{
    public class BattleState : FSMState
    {
        public BattleState()
        {
            _stateID = StateID.BattleStateID;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            
        }

        public override void Act(GameObject player, GameObject npc)
        {
            AICharacterControl enemy = npc.GetComponent<AICharacterControl>();
            ThirdPersonCharacter character = npc.GetComponent<ThirdPersonCharacter>();
            enemy.target = npc;
            enemy.agent.SetDestination(npc.transform.position);
            enemy.SetTransition(Transition.TransitionToStandingStillState);
            character.Move(enemy.agent.desiredVelocity, false, false);
            enemy.transform.LookAt(player.transform.position);
        }
    }
}
