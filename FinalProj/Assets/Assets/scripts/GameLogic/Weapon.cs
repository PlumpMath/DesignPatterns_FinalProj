using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CharacterWeaponFramework
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private WeaponStats _stats;
        [SerializeField]
        private IEffect _AttackEffect;

        public WeaponStats Stats
        {
            get { return _stats; }
        }

        public abstract void attack(CharacterData target);
    }
}
