using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CharacterWeaponFramework
{
    [RequireComponent(typeof(LeveledList))]
    public class Spawners : MonoBehaviour
    {
        [SerializeField]
        private List<string> _SpawnTags;
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
            PopulateGroupFromSpawnTags(_SpawnedGroup);
        }

        private void AddExistingGroupMembersToLists(Group g)
        {
            for(int i =0;i<g.GroupMembersGameObjects.Count;i++)
            {
                _ThingsSpawned.Add(g.GroupMembersGameObjects[i]);
            }
        }

        private void PopulateGroupFromSpawnTags(Group g)
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
                    Debug.LogError("Spawner has spawned something not a GameObject!" + e.StackTrace);
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
                    Debug.LogWarning("Attempted to set ChanceToSpawn to a value greater than 1. Setting value to 1.");
                    _ChanceToSpawn = 1;
                }
                else if (value < 0)
                {
                    Debug.LogWarning("Attempted to set ChanceToSpawn to a value less than 0. Setting value to 0.");
                    _ChanceToSpawn = 0;
                }
                else
                {
                    Debug.Log("Changed ChanceToSpawn to " + value);
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
                    Debug.LogWarning("Attempted to set SpawnRadius to a value less than 0. Setting value to 0.");
                    _SpawnRadius = 0;
                }
                else
                {
                    Debug.Log("Changed SpawnRadius to " + value);
                    _SpawnRadius = value;
                }
            }
        }
    }
}


