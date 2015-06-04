using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class TestTimedEffect : TimedEffect
    {
        
        private TestTimedEffect(CharacterData target,string InternalEffectName, string DisplayEffectName,float lifetime):base(target,InternalEffectName,DisplayEffectName,lifetime)
        { }

        public TestTimedEffect(string InternalEffectName,string DisplayEffectName,float lifetime):base(InternalEffectName,DisplayEffectName,lifetime)
        { }

        public override void ApplyEffect()
        {
            Debug.Log("TestTimedEffect on " + Target.Name);
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new TestTimedEffect(target,this.EffectNameInternalString,this.EffectNameDisplayString,this.Lifetime);
        }
    }
}

