using System;
using UnityEngine;
using CharacterScripts;
using FSM;

namespace AI
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        [SerializeField]
        private float _PersonalSpace;
        [SerializeField]
        private float _MaxDistanceToLeader;
        public GameObject target; // target to aim for
        private Transform _transTarget;
        private FSMSystem fsm;

        public float PersonalSpace
        {
            get { return _PersonalSpace; }
        }

        public float MaxDistanceToLeader
        {
            get { return _MaxDistanceToLeader; }
        }

        // Use this for initialization
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
            _transTarget = target.transform;
            //make sure that _MaxDistanceToLeader is greater than _PersonalSpace since that wouldn't make sense
            if(_MaxDistanceToLeader < _PersonalSpace)
            {
                Debug.LogWarning("AICharacterControl: MaxDistanceToLeader is smaller than PersonalSpace.");
                _MaxDistanceToLeader = _PersonalSpace + 1;
            }
            MakeFSM();
        }

        private void MakeFSM()
        {
            MoveToGroupLeaderState follow = new MoveToGroupLeaderState();
            follow.AddTransition(Transition.TransitionToMovingToGroupLeaderState,StateID.MoveingToGroupLeaderStateID);
            follow.AddTransition(Transition.TransitionToStandingStillState, StateID.StandingStillStateID);
            StandStillState stand = new StandStillState();
            stand.AddTransition(Transition.TransitionToStandingStillState, StateID.StandingStillStateID);
            stand.AddTransition(Transition.TransitionToMovingToGroupLeaderState, StateID.MoveingToGroupLeaderStateID);

            fsm = new FSMSystem();
            fsm.AddState(follow);
            fsm.AddState(stand);

        }

        public void SetTransition(Transition t)
        {
            fsm.PerformTransition(t);
        }

        // Update is called once per frame
        private void Update()
        {
            if (target != null )
            {
                fsm.CurrentState.Reason(target, this.gameObject);
                fsm.CurrentState.Act(target, this.gameObject);
            }
            else
            {
                Debug.LogError("AICharacterControl: target not set.");
            }

        }
    }
}
