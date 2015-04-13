using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System;

namespace CharacterWeaponFramework
{
    public class CharacterData : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private CharacterStats _stats;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private IDisposable leaveGroup;
        [SerializeField]
        private Vector3 _position;

        public CharacterData()
        {}

        void Start()
        {}

        void Update()
        {
            _position = this.gameObject.transform.position;
        }

        public bool attack(CharacterData target)
        {
            float temp = UnityEngine.Random.value;
            //the character uses up stamina and mana even if the attack misses
            _stats.CurMP = _stats.CurMP - _weapon.Stats.ManaCost;
            _stats.CurStamina = _stats.CurStamina - _weapon.Stats.StaminaCost;
            //if the characters chance to hit and the weapons chance to hit and both less than or equal to a randomly determined value
            //the attack lands
            if(_stats.ChanceToHit <= temp && _weapon.Stats.ChanceToHit <= temp)
            {
                //generate a random number between min and max damage of this characters weapon;
                double dmg = UnityEngine.Random.value * (target.Weapon.Stats.MaxDamge - target.Weapon.Stats.MinDamage) + target.Weapon.Stats.MinDamage;
                CharacterStats t = target.Stats;
                t.CurHP = target.Stats.CurHP - dmg;
                target.Stats = t;

                return true;
            }
            return false;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CharacterStats Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
            }
        }

        public Weapon Weapon
        {
            set 
            {
                _weapon = value;
            }
            get { return _weapon; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }
    }
}
