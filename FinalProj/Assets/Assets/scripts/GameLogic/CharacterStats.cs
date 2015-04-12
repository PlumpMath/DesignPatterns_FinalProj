using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CharacterWeaponFramework
{
    public struct CharacterStats
    {
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

        public CharacterStats(double maxHP, double maxMP,double maxStamina, double xp,double chanceToHit)
        {
            _maxHP = maxHP;
            _curHP = maxHP;
            _maxMP = maxMP;
            _curMP = maxMP;
            _maxStamina = maxStamina;
            _curStamina = maxStamina;
            _expriencePoints = xp;
            _chanceToHit = chanceToHit;
        }

        public CharacterStats stats
        {
            get { return this; }
        }

        public double MaxHP
        {
            get { return _maxHP; }
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

     };
}
