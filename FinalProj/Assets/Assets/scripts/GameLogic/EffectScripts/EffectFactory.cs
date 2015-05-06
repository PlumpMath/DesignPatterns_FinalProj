using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CharacterWeaponFramework
{
    public class EffectFactory
    {
        private List<Effects> _EffectList;

        public EffectFactory()
        {
            _EffectList = new List<Effects>();

            //Add effects to the list of effects in the game here
            _EffectList.Add(new Effects(new NullEffect()        , "NullEffect"          ,"Null Effect", false));
            _EffectList.Add(new Effects(new PoisonEffect()      , "PoisonEffect"        ,"Poison Effect", true));
            _EffectList.Add(new Effects(new HealEffect()        , "HealEffect"          ,"Heal Effect", true));
            _EffectList.Add(new Effects(new TestInstantEffect() , "TestInstantEffect"   , "Test Instant Effect", true));


        }


        public IEffect CreateEffect(string eff,CharacterData target)
        {
            int t = IndexOf(eff);
            if(t!=-1)
            {
                return _EffectList[t].Effect.CreateEffect(target);
            }

            return new NullEffect();
        }

        public int FactSize
        {
            get { return _EffectList.Count; }
        }

        public string GetDisplayString(int i)
        {
            return _EffectList[i].DisplayName;
        }

        public string GetInternalName(int i)
        {
            return _EffectList[i].Name;
        }

        private int IndexOf(string name)
        {
            int i = 0;
            for(i = 0;i<_EffectList.Count;i++ )
            {
                if(_EffectList[i].Name==name)
                {
                    return i;
                }
            }

            return -1;
        }

        private struct Effects
        {
            private IEffect _effect;
            private string _internalName;
            private string _displayName;
            private bool _display;

            public Effects(IEffect eff, string internalName, string displayName, bool display)
            {
                _effect = eff;
                _internalName = internalName;
                _displayName = displayName;
                _display = display;
            }

            public IEffect Effect
            {
                get { return _effect; }
            }

            public string Name
            {
                get { return _internalName; }
            }

            public string DisplayName
            {
                get { return _displayName; }
            }

            public bool Display
            {
                get { return _display; }
            }

        }
    }


}

