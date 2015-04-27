using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CharacterWeaponFramework;

namespace GUIScripts
{
    public class PanelHandler : MonoBehaviour
    {
        public void TogglePanel(GameObject panel)
        {
            if (panel.activeInHierarchy == false)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }

        
    }
}
