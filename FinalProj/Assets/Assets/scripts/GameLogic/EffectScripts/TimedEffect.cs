using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace CharacterWeaponFramework
{
    public abstract class TimedEffect : MonoBehaviour, IEffect
    {
        private IDisposable unsubsriber;
        private string _EffectName;
        private float _Lifetime;
        private CharacterData _target;
        
        [SerializeField]
        private Button but = null;

        protected TimedEffect(string name, float lifetime)
        {
            _EffectName = name;
            _Lifetime = lifetime;
        }

        void Start()
        {
            Component t = gameObject.GetComponent<TargetInfo>();
            TargetInfo info = (TargetInfo)t;
            but.onClick.AddListener(
                () =>
                {
                    CreateEffect(GameStateInfo.PlayerGroupData.GroupMembersCharacterData[info.targetNumber]);
                });
        }

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

        public void CreateEffect(CharacterData target)
        {
            if(target != null)
            {
                _target = target;
                unsubsriber = target.Subscribe(this);

                StartCoroutine(removeEffect());
            }
        }

        public abstract void ApplyEffect();

        private IEnumerator removeEffect()
        {
            yield return new WaitForSeconds(_Lifetime);
            Debug.Log("Effect removed");
            unsubsriber.Dispose();
        }
     

    }
}

