using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CharacterScripts
{
    public class LeveledList : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _ListOfMobs;


        public GameObject this[int key]
        {
            get { return _ListOfMobs[key]; }
        }

        public int Length
        {
            get { return _ListOfMobs.Count; }
        }
    }
}

