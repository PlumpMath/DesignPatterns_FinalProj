using UnityEngine;
using System.Collections;
using System;
using CharacterScripts;

namespace EffectScripts
{
    public class NullEffect : InstantEffect
    {
        public NullEffect() : base("Null Effect")
        {}

        public override IEffect CreateEffect(CharacterData target)
        {
            return new NullEffect();
        }

    }
}

