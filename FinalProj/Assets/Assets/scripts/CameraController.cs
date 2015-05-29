using UnityEngine;
using System.Collections;
using CharacterScripts;
using Globals;

public class CameraController : MonoBehaviour {

    //public GameObject player;
    public Group _playerGroup;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        
    }

    void OnLevelWasLoaded()
    {
        offset = transform.position;
        _playerGroup = GlobalGameInfo.PlayerGroupData;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _playerGroup.avgOfGroup + offset;
    }
}
