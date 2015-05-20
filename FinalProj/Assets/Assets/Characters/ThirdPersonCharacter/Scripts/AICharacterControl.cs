using System;
using UnityEngine;
using CharacterWeaponFramework;

namespace CharacterWeaponFramework
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
        private bool _moveToTarget;

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
                _MaxDistanceToLeader = _PersonalSpace + 1;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (_transTarget != null )
            {
                
                float dis = Vector3.Distance(this.transform.position, _transTarget.position);
                if (dis < _PersonalSpace)
                {
                    _moveToTarget = false;
                }
                
                
                if(dis > _MaxDistanceToLeader)
                {
                    _moveToTarget = true;
                }

                if (_moveToTarget)
                {
                    agent.SetDestination(_transTarget.position);
                    // use the values to move the character
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    StopMoving();
                }
                
            }
            else
            {
                StopMoving();
            }
        }

        private void StopMoving()
        {
            // We still need to call the character's move function, but we send zeroed input as the move param.
            character.Move(Vector3.zero, false, false);
        }

        public void SetTarget(Transform target)
        {
            this._transTarget = target;
        }


    }
}
