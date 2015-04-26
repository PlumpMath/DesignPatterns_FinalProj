using UnityEngine;
using System.Collections;

namespace CharacterWeaponFramework
{
    public class TestTimedEffect : TimedEffect
    {
        
        public TestTimedEffect():base("TestTimedEffect",5)
        { }

        public override void ApplyEffect()
        {
            Debug.Log("TestTimedEffect on " + Target.Name);
        }
    }
}

