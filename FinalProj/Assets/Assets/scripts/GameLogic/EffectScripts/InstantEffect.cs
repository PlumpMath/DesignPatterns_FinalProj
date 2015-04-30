﻿using UnityEngine;
using System.Collections;
using System;

namespace CharacterWeaponFramework
{
    public abstract class InstantEffect : IEffect
    {
        private string _EffectName;

        protected InstantEffect(string name)
        {
            _EffectName = name;
        }

        protected InstantEffect()
        {}

        public string EffectName
        {
            get { return _EffectName; }
        }

        public abstract IEffect CreateEffect(CharacterData target);



    }
}
