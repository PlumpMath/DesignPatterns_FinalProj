using UnityEngine;
using System.Collections;
using CharacterScripts;

namespace EffectScripts
{
    public class TestInstantEffect : InstantEffect
    {
        private float _dmg;

        private TestInstantEffect(CharacterData target,string InternalEffectName, string DisplayEffectName,float strength):base(InternalEffectName,DisplayEffectName)
        {
            target.CurHP -= strength;
        }

        public TestInstantEffect(string InternalEffectName, string DisplayEffectName,float strength):base(InternalEffectName,DisplayEffectName)
        { 
            _dmg = strength; 
        }

        public override IEffect CreateEffect(CharacterData target)
        {
            return new TestInstantEffect(target,this.EffectNameInternalString,this.EffectNameDisplayString,this._dmg);
        }
    }
}

