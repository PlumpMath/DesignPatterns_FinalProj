using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class PoisonEffect : TimedEffect
    {
        private float _damage;
        private float _manaCost;

	    private PoisonEffect(CharacterData caster, CharacterData target, string InternalEffectName ,string DisplayEffectName, float lifetime, float strength, float manaCost):base(target,InternalEffectName,DisplayEffectName,lifetime)
        {
            _damage = strength;
            caster.CurMP -= manaCost;
        }

        public PoisonEffect(string InternalEffectName, string DisplayEffectName, float lifetime, float strength, float manaCost):base(InternalEffectName, DisplayEffectName,lifetime)
        {
            _damage = strength;
            _manaCost = manaCost;
        }

        public override void ApplyEffect()
        {
            Target.CurHP -= _damage;
        }


        public override IEffect CreateEffect(CharacterData caster, params CharacterData[] targets)
        {
            if(caster.CurMP >= _manaCost)
            {
                return new PoisonEffect(caster, targets[0], this.EffectNameInternalString, EffectNameDisplayString, this.Lifetime, this._damage, this._manaCost);
            }
            else
            {
                Debug.Log("Failed cast, Not enough mana");
                return new NullEffect("NullEffect", "Null Effect");
            }
            
        }
    }
}

