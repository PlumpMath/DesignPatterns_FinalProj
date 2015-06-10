using UnityEngine;
using System.Collections;
using CharacterScripts;
using System;
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

        public void AddAction(string eff,CharacterData caster, params CharacterData[] targets)
        {
            _BattleActions.Add(new BattleAction(eff,caster, targets));
        }

        public void PreformActions()
        {
            AITurn();
            foreach(BattleAction act in _BattleActions)
            {
                act.ExecuteAction();
            }
            _BattleActions.Clear();
            TargetsPanelHandlerGUI.ResetMoveCounter();
        }

        private void AITurn()
        {
            for(int i =0;i<GlobalGameInfo.enemyGroup.GroupMembersCharacterData.Count;i++)
            {
                CharacterData caster = GlobalGameInfo.enemyGroup.GroupMembersCharacterData[i];
                if(caster.Alive)
                {
                    string eff = AIPickEffect();
                    CharacterData target = AIPickTarget();
                
                    AddAction(eff, caster, target);
                }
                
            }
        }

        private string AIPickEffect()
        {
            System.Random rand = new System.Random();
            int t = rand.Next(GlobalGameInfo.EffFact.FactSize-1);
            return GlobalGameInfo.EffFact.GetInternalName(t);
        }

        private CharacterData AIPickTarget()
        {
            System.Random rand = new System.Random();
            int tmp = GlobalGameInfo.PlayerGroupData.GroupMembersCharacterData.Count-1;
            int t = rand.Next(tmp);
            return GlobalGameInfo.PlayerGroupData.GroupMembersCharacterData[t];
        }

        private class BattleAction
        {
            private string _eff;
            private CharacterData[] _targets;
            private CharacterData _caster;

            public BattleAction(string eff,CharacterData caster, CharacterData[] targets)
            {
                _eff = eff;
                _caster = caster;
                _targets = targets;
            }

            public void ExecuteAction()
            {
                GlobalGameInfo.EffFact.CreateEffect(_eff,_caster, _targets);
            }
        }
    }

}

