using UnityEngine;
using System.Collections;

namespace GUIScripts
{
    public class StartGame : MonoBehaviour
    {

        public void StartLevel()
        {
            Application.LoadLevel("FinalProjTestingWorld");
        }
    }
}

