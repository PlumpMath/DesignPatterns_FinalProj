using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Timers;
using Globals;
using CharacterScripts;

namespace EffectScripts
{
    public abstract class TimedEffect : IEffect
    {
        private IDisposable unsubsriber;
        private string _InternalEffectName;
        private string _DisplayEffectName;
        private float _Lifetime;
        private CharacterData _target;
        private Timer Tim;

        protected TimedEffect(CharacterData target,string InternalEffectName,string DisplayEffectName, float lifetime)
        {
            _InternalEffectName = InternalEffectName;
            _DisplayEffectName = DisplayEffectName;
            _Lifetime = lifetime;
            Tim = new Timer(lifetime*GlobalConsts.NUM_MILLISECOND_IN_SECOND);
            Tim.Elapsed += removeEffect;
            Tim.Enabled = true;
            _target = target;
            this.Subscribe();
        }

        protected TimedEffect(string InternalEffectName, string DisplayEffectName, float lifetime)
        {
            _InternalEffectName = InternalEffectName;
            _DisplayEffectName = DisplayEffectName;
            _Lifetime = lifetime;
        }

        public string EffectNameDisplayString
        {
            get { return _DisplayEffectName; }
        }

        public float Lifetime
        {
            get { return _Lifetime; }
        }

        public CharacterData Target
        {
            get { return _target; }
        }

        public abstract IEffect CreateEffect(CharacterData caster, params CharacterData[] targets);

        private void Subscribe()
        {
            if (_target != null)
            {
                unsubsriber = _target.Subscribe(this);
            }
        }

        public abstract void ApplyEffect();

        private void removeEffect(System.Object source, ElapsedEventArgs e)
        {
            Tim.Enabled = false;
            Debug.Log("TimedEffect: Effect removed");
            unsubsriber.Dispose();
        }


        public string EffectNameInternalString
        {
            get { return _InternalEffectName; }
        }
    }
}

