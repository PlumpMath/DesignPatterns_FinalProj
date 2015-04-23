using UnityEngine;
using System.Collections;
using CharacterWeaponFramework;

public class CameraController : MonoBehaviour {

    //public GameObject player;
    public Group _playerGroup;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _playerGroup.avgOfGroup + offset;
    }
}
