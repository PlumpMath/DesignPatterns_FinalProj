using UnityEngine;
using System.Collections;
namespace CharacterWeaponFramework
{
    public class PoisonEffect : TimedEffect
    {
        private float _damage;
	    public PoisonEffect():base("Poisoned",1)
        { _damage = .2f; }

        public override void ApplyEffect()
        {
            Target.CurHP -= _damage;
        }
    }
}

