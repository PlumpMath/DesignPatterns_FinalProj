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
        private Weapon _weapon;
        [SerializeField]
        private Vector3 _position;

        #region CharacaterStats
        [SerializeField]
        private double _maxHP;
        [SerializeField]
        private double _curHP;
        [SerializeField]
        private double _maxMP;
        [SerializeField]
        private double _curMP;
        [SerializeField]
        private double _maxStamina;
        [SerializeField]
        private double _curStamina;
        [SerializeField]
        private double _expriencePoints;
        [SerializeField]
        private double _chanceToHit;
        [SerializeField]
        private bool _alive;

        public double MaxHP
        {
            get { return _maxHP; }
            set 
            { 
                if(value >= 0)
                {
                    _maxHP = value; 
                }
            }
        }

        public double CurHP
        {
            set 
            { 
                //if the value for hp is less than 0 set hp to 0 as negative hp doesn't make sense
                if(value < 0)
                {
                    _curHP = 0;
                }
                //if the value for hp is greater than the max hp set the cur hp to max to prevent characters having hp above their maximum
                else if(value > _maxHP)
                {
                    _curHP = _maxHP;
                }
                else
                {
                    _curHP = value;
                }
            }

            get { return _curHP; }
        }

        public double MaxMP
        {
            get { return _maxMP; }
            set 
            { 
                if(value >= 0)
                {
                    _maxMP = value; 
                }
                
            }
        }

        public double CurMP
        {
            set
            {
                //same reasioning as with HP don't allow negative or hp above maximum
                if (value < 0)
                {
                    _curMP = 0;
                }
                else if( value > _maxMP)
                {
                    _curMP = _maxMP;
                }
                else
                {
                    _curMP = value;
                }
            }

            get { return _curMP; }
        }

        public double MaxStamina
        {
            get { return _maxStamina; }
            set 
            { 
                if(value >= 0)
                {
                    _maxStamina = value; 
                }
            }
        }

        public double CurStamina
        {
            set
            {
                //same reasioning as with HP don't allow negative or hp above maximum
                if (value < 0)
                {
                    _curStamina = 0;
                }
                else if( value > _maxStamina)
                {
                    _curStamina = _maxStamina;
                }
                else
                {
                    _curStamina = value;
                }
            }

            get { return _curStamina; }
        }

        public double ChanceToHit
        {
            get { return _chanceToHit; }
        }

        public bool Alive
        {
            get { return _alive; }
        }
        #endregion


        //using a restricted form of observer pattern where only TimedEffects can subscribe
        private List<TimedEffect> _AppliedEffects;


        public CharacterData()
        {
            _AppliedEffects = new List<TimedEffect>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            _position = this.gameObject.transform.position;
            //Debug.Log("_AppliedEffects:" + _AppliedEffects.ToString() + "\nCount:" + _AppliedEffects.Count.ToString());
            int i = 0;
            for(i=0;i<_AppliedEffects.Count;i++)
            {
                _AppliedEffects[i].ApplyEffect();
            }
            if(_curHP <= 0)
            {
                _alive = false;
            }
        }

        public bool attack(CharacterData target)
        {
            float temp = UnityEngine.Random.value;
            //the character uses up stamina and mana even if the attack misses
            _curMP = _curMP - _weapon.Stats.ManaCost;
            _curStamina = _curStamina - _weapon.Stats.StaminaCost;
            //if the characters chance to hit and the weapons chance to hit and both less than or equal to a randomly determined value
            //the attack lands
            if(_chanceToHit <= temp && _weapon.Stats.ChanceToHit <= temp)
            {
                //generate a random number between min and max damage of this characters weapon;
                double dmg = UnityEngine.Random.value * (target.Weapon.Stats.MaxDamge - target.Weapon.Stats.MinDamage) + target.Weapon.Stats.MinDamage;
                target.CurHP = target.CurHP - dmg;

                return true;
            }
            return false;
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                if(value == null)
                {
                    Debug.LogWarning("Attempted to set character name to null!");
                }
                else
                {
                    _name = value; 
                }
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



        public IDisposable Subscribe(TimedEffect observer)
        {
            _AppliedEffects.Add(observer);
            return new Unsubsriber(_AppliedEffects, observer);
        }

        private class Unsubsriber : IDisposable
        {
            private List<TimedEffect> _AppliedEffects;
            private TimedEffect _Effect;

            public Unsubsriber(List<TimedEffect> observers, TimedEffect observer)
            {
                _AppliedEffects = observers;
                _Effect = observer;
            }

            public void Dispose()
            {
                if(_Effect != null && _AppliedEffects.Contains(_Effect))
                {
                    _AppliedEffects.Remove(_Effect);
                }
            }
        }

    }
}
