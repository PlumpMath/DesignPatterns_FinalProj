using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using FSM;

/*
 * Moniters the GameObject this is attached to for a specific GameObject (_MoniterFor) to enter the given radius (_MoniterRadius)
 * when this occurs the attached GameObject is teleported to a new location
 * primarly used for setting points for AI groups to move to
 */
namespace AI
{
    public class MarkerMoniter : MonoBehaviour
    {
        [SerializeField]
        private float _MoniterRadius;
        private GameObject _MoniterFor;
        private FSMSystem fsm;

        public void SetTransition(Transition trans)
        {
            fsm.PerformTransition(trans);
        }

        public GameObject MoniterFor
        {
            get
            {
                if (_MoniterFor != null)
                {
                    return _MoniterFor;
                }
                else
                {
                    Debug.LogWarning("MarkerMoniter: requested MoniterFor but MoniterFor is null.");
                    return null;
                }
            }
            set { _MoniterFor = value; }
        }

        public float MoniterRadius
        {
            get { return _MoniterRadius; }
        }

        // Use this for initialization
        void Start()
        {
            MakeFSM();
        }

        private void MakeFSM()
        {
            TeleportMarkerState teleport = new TeleportMarkerState();
            teleport.AddTransition(Transition.TransitionToTeleportMarkerState, StateID.TeleportStateID);
            teleport.AddTransition(Transition.TransitionToDoNothingState, StateID.DoNothingStateID);
            DoNothingState nothing = new DoNothingState();
            nothing.AddTransition(Transition.TransitionToDoNothingState, StateID.DoNothingStateID);
            nothing.AddTransition(Transition.TransitionToTeleportMarkerState, StateID.TeleportStateID);

            fsm = new FSMSystem();
            fsm.AddState(teleport);
            fsm.AddState(nothing);
        }

        // Update is called once per frame
        void Update()
        {
            if(_MoniterFor != null)
            {
                //see which state is valid and then act on the valid state
                foreach(FSMState state in fsm)
                {
                    state.Reason(this.gameObject, _MoniterFor);
                }
                fsm.CurrentState.Act(this.gameObject, _MoniterFor);
            }
            else
            {
                //ugly but don't have the time to do it proper
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}

