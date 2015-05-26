using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//From http://wiki.unity3d.com/index.php?title=Finite_State_Machine
namespace FSM
{
    public enum Transition
    {
        NullTransition = 0,
        TransitionToMovingToGroupLeaderState = 1,
        TransitionToStandingStillState = 2,
        TransitionToBattleState = 3,
        TransitionToNormalState = 4,
        TransitionToTeleportMarkerState = 5,
        TransitionToDoNothingState = 6,

    }

    public enum StateID
    {
        NullStateID = 0,
        MoveingToGroupLeaderStateID = 1,
        StandingStillStateID = 2,
        BattleStateID = 3,
        NormalStateID = 4,
        TeleportStateID = 5,
        DoNothingStateID = 6,

    }

    public abstract class FSMState
    {
        protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
        protected StateID _stateID;
        public StateID ID
        {
            get { return _stateID; }
        }

        public void AddTransition(Transition trans, StateID id)
        {
            if(trans == Transition.NullTransition)
            {
                Debug.LogError("FSM: NullTransition not allowed");
                return;
            }

            if(id == StateID.NullStateID)
            {
                Debug.LogError("FSM: NullStateID not allowed");
                return;
            }

            if(map.ContainsKey(trans))
            {
                Debug.LogError("FSM: Already in dictonary");
                return;
            }

            map.Add(trans, id);
        }

        public void DeleteTransition(Transition trans)
        {
            if(trans == Transition.NullTransition)
            {
                Debug.LogError("FSM: NullTransition deletion not allowed");
                return;
            }

            if(map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }

            Debug.LogError("FSM: Transition not in transition dictionary");
        }

        public StateID GetOutputState(Transition trans)
        {
            if(map.ContainsKey(trans))
            {
                return map[trans];
            }
            return StateID.NullStateID;
        }
        
        public virtual void DoBeforeEntering(){}

        public virtual void DoBeforeLeaving() {}

        public abstract void Reason(GameObject player, GameObject npc);

        public abstract void Act(GameObject player, GameObject npc);
    }

    public class FSMSystem: IEnumerable
    {
        private List<FSMState> _states;

        private StateID _currentStateID;
        public StateID CurrentStateID
        {
            get { return _currentStateID; }
        }
        private FSMState _currentState;
        public FSMState CurrentState
        {
            get { return _currentState; }
        }

        public FSMSystem()
        {
            _states = new List<FSMState>();
        }

        public void AddState(FSMState s)
        {
            if(s == null)
            {
                Debug.LogError("FSM: Null reference not allowed");
                return;
            }

            if(_states.Count == 0)
            {
                _states.Add(s);
                _currentState = s;
                _currentStateID = s.ID;
                return;
            }

            foreach(FSMState state in _states)
            {
                if(state.ID == s.ID)
                {
                    Debug.LogError("FSM: State already added");
                    return;
                }
                
            }

            _states.Add(s);
        }

        public void DeleteState(StateID id)
        {
            if(id == StateID.NullStateID)
            {
                Debug.LogError("FSM: Can't remove null state");
                return;
            }

            foreach(FSMState state in _states)
            {
                if(state.ID == id)
                {
                    _states.Remove(state);
                    return;
                }
            }

            Debug.LogError("FSM: State can't be deleted because it doesn't exsist");
        }

        public void PerformTransition(Transition trans)
        {
            if(trans == Transition.NullTransition)
            {
                Debug.LogError("FSM: Null transition can't be used");
                return;
            }

            StateID id = _currentState.GetOutputState(trans);
            if(id == StateID.NullStateID)
            {
                Debug.LogError("FSM: No target to transition to");
                return;
            }

            _currentStateID = id;

            foreach(FSMState state in _states)
            {
                if(state.ID == _currentStateID)
                {
                    _currentState.DoBeforeLeaving();
                    _currentState = state;
                    _currentState.DoBeforeEntering();
                    break;
                }
            }

        }

        public IEnumerator GetEnumerator()
        {
            return _states.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
    }
}

