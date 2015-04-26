using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CharacterWeaponFramework;

public class PanelHandler : MonoBehaviour 
{
    public void TogglePanel(GameObject panel)
    {
        if(panel.activeInHierarchy==false)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    void OnLevelWasLoaded()
    {
        TargetInfo[] targs = GetComponentsInChildren<TargetInfo>();
        int temp = GameStateInfo.PlayerGroupData.GroupMembersCharacterData.Count;
        //disable all buttons that don't have a corresponding gameobject
        if(targs.Length > temp)
        {
            int i = 0;
            for(i = temp;i<targs.Length;i++)
            {
                targs[i].gameObject.SetActive(false);
            }
        }

    }
}
