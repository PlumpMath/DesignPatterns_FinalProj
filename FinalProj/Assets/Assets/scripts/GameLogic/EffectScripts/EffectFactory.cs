using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CharacterScripts;

namespace EffectScripts
{
    public class EffectFactory
    {
        private List<Effects> _EffectList;
        private List<Effects> _ValidEffectList;

        public EffectFactory()
        {
            _EffectList = new List<Effects>();

            //Add effects to the list of effects in the game here
            _EffectList.Add(new Effects(new NullEffect("NullEffect","Null Effect")                       , false));
            _EffectList.Add(new Effects(new PoisonEffect("PoisonEffect","Poison Effect",1,.2f)           , true));
            _EffectList.Add(new Effects(new HealEffect("HealEffect","Heal Effect",2,.2f)                 , true));
            _EffectList.Add(new Effects(new TestInstantEffect("TestInstantEffect","TestInstantEffect",20), false));


            CreateValidEffectList();
        }

        private void CreateValidEffectList()
        {
            _ValidEffectList = new List<Effects>();
            int i;
            for (i = 0; i < _EffectList.Count; i++)
            {
                if (_EffectList[i].Display)
                {
                    _ValidEffectList.Add(_EffectList[i]);
                }
            }
        }


        public IEffect CreateEffect(string eff,CharacterData target)
        {
            int t = IndexOf(eff);
            if(t!=-1)
            {
                return _EffectList[t].Effect.CreateEffect(target);
            }

            return new NullEffect("NullEffect","Null Effect");
        }

        public int FactSize
        {
            get { return _ValidEffectList.Count; }
        }

        public string GetDisplayString(int i)
        {
            return _ValidEffectList[i].Effect.EffectNameDisplayString;
        }

        public string GetInternalName(int i)
        {
            return _ValidEffectList[i].Effect.EffectNameInternalString;
        }

        private int IndexOf(string name)
        {
            int i = 0;
            for(i = 0;i<_EffectList.Count;i++ )
            {
                if(_EffectList[i].Effect.EffectNameInternalString==name)
                {
                    return i;
                }
            }

            return -1;
        }

        private struct Effects
        {
            private IEffect _effect;
            private bool _display;

            public Effects(IEffect eff, bool display)
            {
                _effect = eff;
                _display = display;
            }

            public IEffect Effect
            {
                get { return _effect; }
            }

            public bool Display
            {
                get { return _display; }
            }

        }
    }


}

