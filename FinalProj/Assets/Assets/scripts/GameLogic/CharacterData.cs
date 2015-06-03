using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System;
using EffectScripts;

namespace CharacterScripts
{
    
    public class CharacterData : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private Vector3 _position;
        [SerializeField]
        private bool _enemy;

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

        void Update()
        {
            _position = this.gameObject.transform.position;
            
            int i = 0;
            for(i=0;i<_AppliedEffects.Count;i++)
            {
                _AppliedEffects[i].ApplyEffect();
            }
            if(_curHP <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            if(_alive == true)
            {
                _alive = false;
                Vector3 tmp = this.transform.rotation.eulerAngles;
                tmp.z = 90;
                this.transform.Rotate(tmp);
            }
            
        }

        public bool attack(CharacterData target)
        {
            return _weapon.attack(this, target);
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                if(value == null)
                {
                    Debug.LogError("CharacterData: Attempted to set character name to null.");
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

        public bool Enemy
        {
            get { return _enemy; }
            set { _enemy = value; }
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
