using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using EffectScripts;

namespace CharacterScripts
{
    public class Weapon 
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private IEffect _AttackEffect;

        #region WeaponStats
        [SerializeField]
        private double _MinDamage;
        [SerializeField]
        private double _MaxDamage;
        [SerializeField]
        private double _ManaCost;
        [SerializeField]
        private double _StaminaCost;
        [SerializeField]
        private double _ChanceToHit;

        public double MinDamage
        {
            get { return _MinDamage; }
            set
            {
                if(value < 0)
                {
                    _MinDamage = 0;
                }
                else
                {
                    _MinDamage = value;
                }
            }

        }

        public double MaxDamage
        {
            get { return _MaxDamage; }
            set
            {
                if(value < _MinDamage)
                {
                    _MaxDamage = _MinDamage;
                }
                else
                {
                    _MaxDamage = value;
                }

            }
        }

        public double ManaCost
        {
            get { return _ManaCost; }
            set
            {
                if(value < 0)
                {
                    _ManaCost = 0;
                }
                else
                {
                    _ManaCost = value;
                }
            }
        }

        public double StaminaCost
        {
            get { return _StaminaCost; }
            set
            {
                if(value<0)
                {
                    _StaminaCost = 0;
                }
                else
                {
                    _StaminaCost = value;
                }
            }
        }

        public double ChanceToHit
        {
            get { return _ChanceToHit; }
            set
            {
                if(value < 0)
                {
                    _ChanceToHit = 0;
                }
                else if(value>1)
                {
                    _ChanceToHit = 1;
                }
                else
                {
                    _ChanceToHit = value;
                }
            }
        }
        #endregion

        protected Weapon(string name, double minDmg, double maxDmg, double manaCost, double staminaCost, double chanceToHit)
        {
            _name = name;
            setWeaponStats(minDmg,maxDmg,manaCost,staminaCost,chanceToHit, new NullEffect("NullEffect", "Null Effect"));
        }

        protected Weapon(string name,double minDmg, double maxDmg, double manaCost, double staminaCost, double chanceToHit,IEffect eff)
        {
            _name = name;
            setWeaponStats(minDmg, maxDmg, manaCost, staminaCost, chanceToHit, eff);
        }

        private void setWeaponStats(double minDmg, double maxDmg, double manaCost, double staminaCost, double chanceToHit, IEffect eff)
        {
            //DO NOT CHANGE ORDER MATTERS 
            MaxDamage = maxDmg;
            MinDamage = minDmg;
            ManaCost = manaCost;
            StaminaCost = staminaCost;
            ChanceToHit = chanceToHit;
            _AttackEffect = eff;
        }

        public virtual bool attack(CharacterData Holder, CharacterData target)
        {
            float temp = UnityEngine.Random.value;
            //the character uses up stamina and mana even if the attack misses
            Holder.CurMP = Holder.CurMP - _ManaCost;
            Holder.CurStamina = Holder.CurStamina - _StaminaCost;
            //if the characters chance to hit and the weapons chance to hit and both less than or equal to a randomly determined value
            //the attack lands
            if (Holder.ChanceToHit <= temp && _ChanceToHit <= temp)
            {
                //generate a random number between min and max damage of this characters weapon;
                double dmg = UnityEngine.Random.value * (target.Weapon.MaxDamage - target.Weapon.MinDamage) + target.Weapon.MinDamage;
                target.CurHP = target.CurHP - dmg;
                _AttackEffect.CreateEffect(target);
                return true;
            }
            return false;
        }
    }
}
