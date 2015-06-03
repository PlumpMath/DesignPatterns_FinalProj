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