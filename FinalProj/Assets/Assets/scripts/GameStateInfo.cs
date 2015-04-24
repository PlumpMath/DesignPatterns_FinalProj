using UnityEngine;
using System.Collections;
using CharacterWeaponFramework;

public class GameStateInfo : MonoBehaviour 
{
    public static Group _PlayerGroupData;
    public GameObject _player;


	// Use this for initialization
	void Start () 
    {
        _PlayerGroupData = gameObject.AddComponent<Group>();
        _PlayerGroupData.AddCharacter(_player);
	}
	
    void Awake()
    {
        Object.DontDestroyOnLoad(this);
    }


    public void addCharacterToPlayerGroup(GameObject newChar)
    {
        _PlayerGroupData.AddCharacter(newChar);
    }
}
