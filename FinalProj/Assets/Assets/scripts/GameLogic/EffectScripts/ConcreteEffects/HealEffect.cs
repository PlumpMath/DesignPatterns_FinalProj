using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class HealEffect : TimedEffect
    {
        private float _heal;
        
        public HealEffect(CharacterData target):base("Healing",target,2)
        {
            _heal = .2f;
        }

        public HealEffect()
        {}

        public override void ApplyEffect()
        {
            Target.CurHP += _heal;
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new HealEffect(target);
        }
    }
}

