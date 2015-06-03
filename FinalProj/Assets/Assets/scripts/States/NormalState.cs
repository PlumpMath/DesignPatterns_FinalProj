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

        public override void Reason(GameObject player, GameObject npc)
        {
            //enemy inside Battle Radius
            ThirdPersonUserControl control = player.GetComponent<ThirdPersonUserControl>();
            if (Vector3.Distance(player.transform.position, npc.transform.position) < control.BRadius)
            {
                control.SetTransition(Transition.TransitionToBattleState);
                control.transform.LookAt(npc.transform.position);
            }

            //if the amount of dead things in the group is equal to the size of the group, transition to normal state
            int dead = 0;
            for (int y = 0; y < Globals.GlobalGameInfo.EnemyGroup.GroupMembersGameObjects.Count; y++)
            {
                CharacterData currentEnemyData = Globals.GlobalGameInfo.EnemyGroup.GroupMembersGameObjects[y].GetComponent<CharacterData>();
                if (currentEnemyData.Alive == false) dead++;
            }
            if (dead >= Globals.GlobalGameInfo.EnemyGroup.GroupMembersGameObjects.Count)control.SetTransition(Transition.TransitionToNormalState);
        }

        public override void DoBeforeEntering()
        {
            BattleUIUtils.ToggleEffectSubPanels();
            BattleUIUtils.ToggleBattleUI();
        }

        public override void Act(GameObject player, GameObject npc)
        {
            //turn off button input as a work around 
            
        }
    }
}