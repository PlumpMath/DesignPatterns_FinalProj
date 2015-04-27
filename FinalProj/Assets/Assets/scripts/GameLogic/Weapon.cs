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

        public abstract void attack(CharacterData target);
    }
}
