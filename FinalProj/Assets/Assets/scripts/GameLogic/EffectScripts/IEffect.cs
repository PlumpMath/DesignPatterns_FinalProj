using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CharacterWeaponFramework 
{
    public interface IEffect
    {
        IEffect CreateEffect(CharacterData target);

        string EffectName{get;}


    }
}
