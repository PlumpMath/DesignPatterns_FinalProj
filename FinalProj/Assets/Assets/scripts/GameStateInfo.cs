using UnityEngine;
using System.Collections;
using CharacterWeaponFramework;

public class GameStateInfo : MonoBehaviour 
{
    [SerializeField]
    private static Group _PlayerGroupData;
    public GameObject _player;

    public static Group PlayerGroupData
    {
        get { return _PlayerGroupData; }
    }

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
