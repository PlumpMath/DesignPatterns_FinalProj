using UnityEngine;
using System.Collections;
using CharacterScripts;
using EffectScripts;
using GUIScripts;
using Utils;

namespace Globals
{
    public class GlobalGameInfo : MonoBehaviour
    {
        [SerializeField]
        private static Group _PlayerGroupData;
        public GameObject _player;
        public static EffectFactory EffFact = new EffectFactory();
        private static GameObject[] _BattleUI;


        public static Group PlayerGroupData
        {
            get { return _PlayerGroupData; }
        }

        public static GameObject[] BattleUI
        {
            get { return _BattleUI; }
        }

        void OnLevelWasLoaded()
        {
            _BattleUI = GameObject.FindGameObjectsWithTag("BattleUI");

            //BattleUIUtils.ToggleBattleUI();
        }

        // Use this for initialization
        void Start()
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
}

