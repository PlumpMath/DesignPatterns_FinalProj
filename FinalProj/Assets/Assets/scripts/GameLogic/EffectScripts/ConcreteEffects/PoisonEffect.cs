using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class PoisonEffect : TimedEffect
    {
        private float _damage;

	    public PoisonEffect(CharacterData target):base("Poisoned",target,1)
        { 
            _damage = .2f; 
        }

        public PoisonEffect()
        {}

        public override void ApplyEffect()
        {
            Target.CurHP -= _damage;
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new PoisonEffect(target);
        }
    }
}

