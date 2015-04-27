using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Timers;
using Globals;

namespace CharacterWeaponFramework
{
    public abstract class TimedEffect : IEffect
    {
        private IDisposable unsubsriber;
        private string _EffectName;
        private float _Lifetime;
        private CharacterData _target;
        private Timer Tim;
        
        [SerializeField]
        private Button but = null;

        protected TimedEffect(string name, CharacterData target, float lifetime)
        {
            _EffectName = name;
            _Lifetime = lifetime;
            Tim = new Timer(lifetime*GlobalConsts.NUM_MILLISECOND_IN_SECOND);
            Tim.Elapsed += removeEffect;
            Tim.Enabled = true;
            _target = target;
            this.Subscribe();
        }

        protected TimedEffect()
        {}

        public string EffectName
        {
            get { return _EffectName; }
        }

        public float Lifetime
        {
            get { return _Lifetime; }
        }

        public CharacterData Target
        {
            get { return _target; }
        }

        public abstract IEffect CreateEffect(CharacterData target);

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
            Debug.Log("Effect removed");
            unsubsriber.Dispose();
        }
     

    }
}

