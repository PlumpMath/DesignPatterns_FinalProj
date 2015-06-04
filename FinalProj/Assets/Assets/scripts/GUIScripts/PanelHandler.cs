using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace GUIScripts
{
    public class PanelHandler : MonoBehaviour
    {
        public static void StaticTogglePanel(GameObject panel)
        {
            if(panel.activeInHierarchy == false)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }

        //because unity won't allow static methods to be called by button clicks a non static version is needed
        public void TogglePanel(GameObject panel)
        {
            StaticTogglePanel(panel);
        }
        
    }
}

