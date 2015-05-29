using UnityEngine;
using System.Collections;

/*
 * This state is for when an object needs to do nothing
 * Due to it's nature it can only be set directly and can't be transitioned to in the usual way
 */
namespace FSM
{
    public class DoNothingState : FSMState
    {
        public DoNothingState()
        {
            _stateID = StateID.DoNothingStateID;
        }

        public override void Reason(GameObject obj, GameObject npc)
        {
            //leave empty
        }

        public override void Act(GameObject obj, GameObject npc)
        {
            //leave empty
        }
    }
}

