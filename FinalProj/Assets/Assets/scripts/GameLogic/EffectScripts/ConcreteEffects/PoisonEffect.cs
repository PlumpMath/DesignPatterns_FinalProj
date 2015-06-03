using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class PoisonEffect : TimedEffect
    {
        private float _damage;

	    private PoisonEffect(CharacterData target, string InternalEffectName ,string DisplayEffectName, float lifetime, float strength):base(target,InternalEffectName,DisplayEffectName,lifetime)
        {
            _damage = strength;
        }

        public PoisonEffect(string InternalEffectName, string DisplayEffectName, float lifetime, float strength):base(InternalEffectName, DisplayEffectName,lifetime)
        {
            _damage = strength;
        }

        public override void ApplyEffect()
        {
            Target.CurHP -= _damage;
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new PoisonEffect(target,this.EffectNameInternalString,EffectNameDisplayString,this.Lifetime,this._damage);
        }
    }
}

