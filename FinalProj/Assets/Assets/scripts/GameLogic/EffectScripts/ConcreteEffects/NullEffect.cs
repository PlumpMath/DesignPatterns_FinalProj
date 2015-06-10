using UnityEngine;
using System.Collections;
using System;
using CharacterScripts;

namespace EffectScripts
{
    public class NullEffect : InstantEffect
    {
        public NullEffect(string InternalEffectName, string DisplayEffectName)
            : base(InternalEffectName, DisplayEffectName)
        { }

        public override IEffect CreateEffect(CharacterData caster, params CharacterData[] targets)
        {
            return new NullEffect(this.EffectNameInternalString, this.EffectNameDisplayString);
        }
    }
}

