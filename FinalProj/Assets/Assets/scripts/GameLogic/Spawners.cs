using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AI;
using Globals;

namespace CharacterScripts
{
    [RequireComponent(typeof(LeveledList))]
    public class Spawners : MonoBehaviour
    {
        [SerializeField]
        private float _ChanceToSpawn;
        [SerializeField]
        private float _SpawnRadius;
        [SerializeField]
        private int _SpawnCap;
        private LeveledList _LeveledList;
        private List<GameObject> _ThingsSpawned;
        [SerializeField]
        private GameObject _GroupPreFab;
        private Group _SpawnedGroup;
        [SerializeField]
        private GameObject _PositionMarker;


        // Use this for initialization
        void Start()
        {
            _ThingsSpawned = new List<GameObject>();
            _LeveledList = GetComponentInParent<LeveledList>();
        }


        //instantiates a group based on the prefab set in the editor
        private void CreateGroupFromPrefab()
        {
            GameObject t = Instantiate(_GroupPreFab);
            _SpawnedGroup = t.GetComponent<Group>();
            //The only way to have the count be greater than 0 is if the group is instantiated from a prefab
            //that has members already in it and to keep consistant game data these need to be added to some lists
            if(_SpawnedGroup.GroupMembersGameObjects.Count > 0)
            {
                AddExistingGroupMembersToLists(_SpawnedGroup);
            }
            //fill out the remaining spawn cap with things from the Spawn Tags list
            PopulateGroupFromLeveledList(_SpawnedGroup);
            SetMovementTargetsOfGroup(_SpawnedGroup);

            ForceGroupPositionUpdate(t);
        }

        private void ForceGroupPositionUpdate(GameObject t)
        {
            _SpawnedGroup.UpdateAvgPositionOfGroup();
            t.transform.position = _SpawnedGroup.avgOfGroup;
        }

        private void SetMovementTargetsOfGroup(Group g)
        {
            SetGroupLeaderTarget(g);
            int i;
            GameObject cur;
            AICharacterControl control;
            for(i = 1;i<g.GroupMembersGameObjects.Count;i++)
            {
                cur = g.GroupMembersGameObjects[i];
                control = cur.GetComponent<AICharacterControl>();
                control.target = g.GroupMembersGameObjects[i - 1];
                
            }
        }

        private void SetGroupLeaderTarget(Group g)
        {
            GameObject leader = g.GroupMembersGameObjects[0];
            AICharacterControl control = leader.GetComponent<AICharacterControl>();
            GameObject target = CreatePositionMarkerForLeader();
            MarkerMoniter mon = target.GetComponent<MarkerMoniter>();
            mon.MoniterFor = leader;
            control.target = target;
        }

        private GameObject CreatePositionMarkerForLeader()
        {
            GameObject marker = Instantiate(_PositionMarker);
            System.Random rand = new System.Random();
            Vector3 randVect = UnityEngine.Random.insideUnitSphere * rand.Next(GlobalConsts.WORLD_SIZE);
            randVect.y = 0;
            marker.transform.position = randVect;
            return marker;
        }

        private void AddExistingGroupMembersToLists(Group g)
        {
            for(int i =0;i<g.GroupMembersGameObjects.Count;i++)
            {
                _ThingsSpawned.Add(g.GroupMembersGameObjects[i]);
            }
        }

        private void PopulateGroupFromLeveledList(Group g)
        {
            while(_ThingsSpawned.Count < _SpawnCap)
            {
                System.Random rand = new System.Random();
                int IndexOfThingToSpawn = rand.Next(0, _LeveledList.Length);
                

                Vector3 tempVec = UnityEngine.Random.insideUnitSphere * _SpawnRadius + transform.position;
                tempVec.y = transform.position.y;
                Quaternion tempQuaternion = new Quaternion();
                GameObject GameObjectToSpawn = _LeveledList[IndexOfThingToSpawn];
                UnityEngine.Object tmp = Instantiate(GameObjectToSpawn,tempVec,tempQuaternion);
                try
                {
                    GameObject ThingSpawned = (GameObject)tmp;
                    _SpawnedGroup.AddCharacter(ThingSpawned);
                    _ThingsSpawned.Add(ThingSpawned);
                }
                catch(Exception e)
                {
                    Debug.LogError("Spawners: Spawner has spawned something not a GameObject!" + e.StackTrace);
                } 
            }
        }

        // Update is called once per frame
        void Update()
        {
            System.Random rand = new System.Random();

            if(rand.NextDouble() < _ChanceToSpawn && _ThingsSpawned.Count < _SpawnCap)
            {
                CreateGroupFromPrefab();
            }

            if (_SpawnedGroup == null)
            {
                _ThingsSpawned = new List<GameObject>();
                
            }

        }

        public float ChanceToSpawn
        {
            get { return _ChanceToSpawn; }
            set
            {
                //check input for values that make sense
                //if nonsense values given log a message
                if (value > 1)
                {
                    Debug.LogWarning("Spawners: Attempted to set ChanceToSpawn to a value greater than 1. Setting value to 1.");
                    _ChanceToSpawn = 1;
                }
                else if (value < 0)
                {
                    Debug.LogWarning("Spawners: Attempted to set ChanceToSpawn to a value less than 0. Setting value to 0.");
                    _ChanceToSpawn = 0;
                }
                else
                {
                    Debug.Log("Spawners: Changed ChanceToSpawn to " + value);
                    _ChanceToSpawn = value;
                }
            }
        }

        public float SpawnRadius
        {
            get { return _SpawnRadius; }
            set
            {
                //check input for values that make sense
                if (value < 0)
                {
                    Debug.LogWarning("Spawners: Attempted to set SpawnRadius to a value less than 0. Setting value to 0.");
                    _SpawnRadius = 0;
                }
                else
                {
                    Debug.Log("Spawners: Changed SpawnRadius to " + value);
                    _SpawnRadius = value;
                }
            }
        }
    }
}


