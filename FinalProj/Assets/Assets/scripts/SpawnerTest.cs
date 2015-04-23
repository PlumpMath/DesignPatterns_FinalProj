using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Utils;
using CharacterWeaponFramework;

public class SpawnerTest : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> _ThingsToSpawn;
    [SerializeField]
    private float _ChanceToSpawn;
    [SerializeField]
    private float _SpawnRadius;
    [SerializeField]
    private int _SpawnCap;
    private List<GameObject> _ThingsSpawned;
    
	// Use this for initialization
	void Start () 
    {
        _ThingsSpawned = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        System.Random rand = new System.Random();
        
        if(rand.NextDouble() < _ChanceToSpawn && _ThingsSpawned.Count < _SpawnCap)
        {
            int temp = rand.Next(0, _ThingsToSpawn.Count);

            Vector3 tempVec = UnityEngine.Random.insideUnitSphere * _SpawnRadius + transform.position;

            tempVec.y = transform.position.y;

            Quaternion tempQuaternion = new Quaternion();
            UnityEngine.Object ThingSpawned = Instantiate(_ThingsToSpawn[temp], tempVec, tempQuaternion);
            
            try
            {
                GameObject t = (GameObject)ThingSpawned;
                Canvas can = t.GetComponent<Canvas>();
                GameObject g = GameObject.FindGameObjectWithTag("MainCamera");
                can.worldCamera = g.GetComponent<Camera>();

                //UnityEngine.UI.Button but = t.GetComponent<UnityEngine.UI.Button>();
                //but.onClick.AddListener(() => _group.AddCharacter(t));

                _ThingsSpawned.Add(t);
            }
            catch(Exception e)
            {
                Debug.LogError("Spawner has spawned something not a GameObject!" + e.StackTrace);
            }
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
            if(value < 0)
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
