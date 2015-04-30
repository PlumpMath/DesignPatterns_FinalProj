using UnityEngine;
using System.Collections;
using System;

namespace CharacterWeaponFramework
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

