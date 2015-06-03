using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using EffectScripts;

namespace CharacterScripts
{
    public abstract class Weapon 
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

        }

        public double MaxDamge
        {
            get { return MaxDamge; }
        }

        public double ManaCost
        {
            get { return _ManaCost; }
        }

        public double StaminaCost
        {
            get { return _StaminaCost; }
        }

        public double ChanceToHit
        {
            get { return _ChanceToHit; }
        }
        #endregion

        protected Weapon()
        {
            _AttackEffect = new NullEffect("NullEffect","Null Effect");
        }

        public bool attack(CharacterData Holder, CharacterData target)
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
                double dmg = UnityEngine.Random.value * (target.Weapon.MaxDamge - target.Weapon.MinDamage) + target.Weapon.MinDamage;
                target.CurHP = target.CurHP - dmg;
                _AttackEffect.CreateEffect(target);
                return true;
            }
            return false;
        }
    }
}
