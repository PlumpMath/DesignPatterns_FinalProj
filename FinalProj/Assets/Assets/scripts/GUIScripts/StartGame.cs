using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour 
{
    [SerializeField]
    private Canvas _CharacterSelectScreen;
    [SerializeField]
    private Canvas _GameHUD;

    void Start()
    {
        _GameHUD.enabled = false;
    }

	public void HideCharacterScreen()
    {
        _CharacterSelectScreen.enabled = false;
        _GameHUD.enabled = true;
    }
}
