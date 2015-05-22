using UnityEngine;
using System.Collections;
using FSM;

namespace CharacterWeaponFramework
{
    public class BattleState : FSMState
    {
        public BattleState()
        {
            _stateID = StateID.BattleStateID;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            AICharacterControl ai = npc.GetComponent<AICharacterControl>();
            if (Vector3.Distance(player.transform.position, npc.transform.position) < ai.PersonalSpace)
            {
                ai.SetTransition(Transition.TransitionToBattleState);
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            ThirdPersonUserControl plyr = player.GetComponent<ThirdPersonUserControl>();
            ThirdPersonCharacter character = player.GetComponent<ThirdPersonCharacter>();
            character.Move(Vector3.zero, false, false);
        }
    }
}
