using UnityEngine;
using System;
using System.Collections;
using FSM;
using Globals;

namespace AI
{
    public class TeleportMarkerState : FSMState
    {
        public TeleportMarkerState()
        {
            _stateID = StateID.TeleportStateID;
        }

        public override void Reason(GameObject marker, GameObject npc)
        {
            MarkerMoniter mon = marker.GetComponent<MarkerMoniter>();
            if(Vector3.Distance(marker.transform.position,npc.transform.position)<=mon.MoniterRadius)
            {
                mon.SetTransition(Transition.TransitionToTeleportMarkerState);
            }
        }

        //Teleport marker to a random location then transition to the DoNothingState
        public override void Act(GameObject marker, GameObject npc)
        {
            MarkerMoniter mon = marker.GetComponent<MarkerMoniter>();
            System.Random rand = new System.Random();
            Vector3 randVect = UnityEngine.Random.insideUnitSphere * rand.Next(GlobalConsts.WORLD_SIZE);
            randVect.y = 0;
            marker.transform.position = randVect;
            mon.SetTransition(Transition.TransitionToDoNothingState);
        }
    }
}

