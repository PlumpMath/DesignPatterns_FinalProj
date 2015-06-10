using UnityEngine;
using System.Collections;
using CharacterScripts;
using System.Collections.Generic;
using EffectScripts;
using Globals;

namespace GUIScripts
{
    public class turns : MonoBehaviour
    {
        private List<BattleAction> _BattleActions;

        void Start()
        {
            _BattleActions = new List<BattleAction>();
        }

        public void AddAction(string eff, CharacterData target)
        {
            _BattleActions.Add(new BattleAction(eff, target));
        }

        public void PreformActions()
        {
            foreach(BattleAction act in _BattleActions)
            {
                act.ExecuteAction();
            }
            _BattleActions.Clear();
        }


        private class BattleAction
        {
            private string _eff;
            private CharacterData _target;

            public BattleAction(string eff, CharacterData target)
            {
                _eff = eff;
                _target = target;
            }

            public void ExecuteAction()
            {
                GlobalGameInfo.EffFact.CreateEffect(_eff, _target);
            }
        }
    }

}

