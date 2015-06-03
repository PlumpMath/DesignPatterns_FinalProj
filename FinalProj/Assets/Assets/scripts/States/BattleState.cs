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
            enemy.target = npc;
            enemy.SetTransition(Transition.TransitionToStandingStillState);
            ThirdPersonCharacter enmy = npc.GetComponent<ThirdPersonCharacter>();
            //enemy.transform.LookAt(player.transform.position);
        }
    }
}
