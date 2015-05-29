using UnityEngine;
using System.Collections;
using CharacterWeaponFramework;

namespace EffectScripts
{
    public class TestInstantEffect : InstantEffect
    {
        private float _dmg;

        public TestInstantEffect(CharacterData target):base("Test Instant Effect")
        {
            _dmg = 20f;
            target.CurHP -= _dmg;
        }

        public TestInstantEffect()
        {}

        public override IEffect CreateEffect(CharacterData target)
        {
            return new TestInstantEffect(target);
        }
    }
}

