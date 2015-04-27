using UnityEngine;
using System.Collections;

namespace CharacterWeaponFramework
{
    public class TestTimedEffect : TimedEffect
    {
        
        public TestTimedEffect(CharacterData target):base("TestTimedEffect", target,5)
        { }

        public TestTimedEffect()
        { }

        public override void ApplyEffect()
        {
            Debug.Log("TestTimedEffect on " + Target.Name);
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new TestTimedEffect(target);
        }
    }
}

