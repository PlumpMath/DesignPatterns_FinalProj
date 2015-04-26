using UnityEngine;
using System.Collections;

namespace CharacterWeaponFramework
{
    public class HealEffect : TimedEffect
    {
        private float _heal;
        
        public HealEffect():base("Healing",2)
        {
            _heal = .2f;
        }

        public override void ApplyEffect()
        {
            Target.CurHP += _heal;
        }
    }
}

