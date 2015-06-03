using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class HealEffect : TimedEffect
    {
        private float _heal;
        
        private HealEffect(CharacterData target, string InternalEffectName ,string DisplayEffectName, float lifetime, float strength):base(target,InternalEffectName,DisplayEffectName,lifetime)
        {
            _heal = strength;
        }

        public HealEffect(string InternalEffectName, string DisplayEffectName, float lifetime, float strength):base(InternalEffectName, DisplayEffectName,lifetime)
        {
            _heal = strength;
        }

        public override void ApplyEffect()
        {
            Target.CurHP += _heal;
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new HealEffect(target, this.EffectNameInternalString,this.EffectNameDisplayString, this.Lifetime, this._heal);
        }
    }
}

