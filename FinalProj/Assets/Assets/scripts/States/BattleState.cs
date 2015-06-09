using UnityEngine;
using System.Collections;
using FSM;
using AI;
using GUIScripts;
using Globals;
using Utils;

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
            ThirdPersonUserControl control = player.GetComponent<ThirdPersonUserControl>();
            //if the amount of dead things in the group is equal to the size of the group, transition to normal state
            int dead = 0;
            for (int y = 0; y < Globals.GlobalGameInfo.enemyGroup.GroupMembersGameObjects.Count; y++)
            {
                CharacterData currentEnemyData = Globals.GlobalGameInfo.enemyGroup.GroupMembersGameObjects[y].GetComponent<CharacterData>();
                if (currentEnemyData.Alive == false) dead++;
            }
            if (dead >= Globals.GlobalGameInfo.enemyGroup.GroupMembersGameObjects.Count) control.SetTransition(Transition.TransitionToNormalState);
        }

        public override void DoBeforeEntering()
        {
            BattleUIUtils.InstantiateBattleUI();
            //ensure that the sub panels are not enabled at the start of battle
            BattleUIUtils.SetEffectSubPanelsToOff();
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
