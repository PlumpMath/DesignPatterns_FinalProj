using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CharacterWeaponFramework
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private WeaponStats _stats;


        public WeaponStats Stats
        {
            get { return _stats; }
        }
    }
}
