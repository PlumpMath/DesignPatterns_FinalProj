using UnityEngine;
using System.Collections;

namespace GUIScripts
{
    public class JessStartGame : MonoBehaviour
    {

        public void StartLevel()
        {
            Application.LoadLevel("JessFinalProjTestingWorld");
        }
    }
}