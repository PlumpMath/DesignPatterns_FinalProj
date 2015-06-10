using UnityEngine;
using System.Collections;
using System;
using CharacterScripts;

namespace EffectScripts
{
    public abstract class InstantEffect : IEffect
    {
        private string _InternalEffectName;
        private string _DisplayEffectName;

        protected InstantEffect(string InternalEffectName, string DisplayEffectName)
        {
            _InternalEffectName = InternalEffectName;
            _DisplayEffectName = DisplayEffectName;
        }

        protected InstantEffect()
        {}

        public string EffectNameDisplayString
        {
            get { return _DisplayEffectName; }
        }


        public string EffectNameInternalString
        {
            get { return _InternalEffectName; }
        }

        public abstract IEffect CreateEffect(CharacterData caster, params CharacterData[] targets);
    }
}

