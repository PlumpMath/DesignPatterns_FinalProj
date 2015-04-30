using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Manager : MonoBehaviour
{    
    List<Item> _lootItems;

    public GUISkin mySkin;
    public float buttonWidth = 40;
    public float buttonHeight = 40;
    public float closeButtonWidth = 20;
    public float closeButtonHeight = 20;

    float _offset = 10;

    bool _displayLootWindow = false;
    float lootWindowHeight = 90;
    const int LOOT_WINDOW_ID = 0;
    Rect _lootWindowRect = new Rect(0, 0, 0, 0);
    Vector2 _lootWindowSlider = Vector2.zero;
    public Chest currentChest;
    string _toolTip = "";

    /////Inventory section ///////////////    
    bool _displayInventoryWindow = false;    
    const int INVENTORY_WINDOW_ID = 1;
    Rect _inventoryWindowRect = new Rect(10, 10, 170, 265);
    int _inventoryRows = 6;
    int _inventoryCols = 4;
    float _doubleClickTimer = 0;
    const float DOUBLE_CLICK_TIMER_THRESHOLD = .5f;//how high _doubleClickTimer must get before we can click again
    Item _selectedItem;

    /////Character section ///////////////    
    bool _displayCharacterWindow = false;
    const int CHARACTER_WINDOW_ID = 2;
    Rect _characterWindowRect = new Rect(10, 10, 170, 265);
    int _characterPanel = 0;
    string[] _characterPanelNames = new string[] { "Equipment", "Attributes", "Skills" };

	// Use this for initialization
	void Start () 
    {
       // _lootItems = new List<Item>();        
	}

    void OnGUI()
    {
        GUI.skin = mySkin;
        if (_displayCharacterWindow)
        {
            _characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterWindowRect, CharacterWindow, "Character");
        }

        if (_displayInventoryWindow)
        {
            _inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
        }

        if (_displayLootWindow)
        {
            _lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset + lootWindowHeight),
                Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Loot");
        }

        DisplayToolTip();
    }
    void LootWindow(int id)
    {
        GUI.skin = mySkin;              

        if (currentChest != null)
        {            
            _lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * .5f, 15, _lootWindowRect.width - 10, 70), _lootWindowSlider,  
                                                    new Rect(0, 0, currentChest.loot.Count * buttonWidth + _offset, buttonHeight + _offset));
        
            for (int i = 0; i < currentChest.loot.Count; i++)
            {

                if (GUI.Button(new Rect(5 + (buttonWidth * i), 0, buttonWidth, buttonHeight),
                    new GUIContent(currentChest.loot[i].Icon, currentChest.loot[i].ToolTip()), "Inventory Slot Common"))
                {                    
                    Player.Inventory.Add(currentChest.loot[i]);
                    currentChest.loot.RemoveAt(i);
                }            
            }
            GUI.EndScrollView();

            SetToolTip();

            if (GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "x") || currentChest.loot.Count == 0)
            {
                ClearWindow();
            }
        }            
    }    

    public void DisplayLoot()
    {
        _displayLootWindow = true;
    }
   
    public void ClearWindow()
    {
       // _lootItems.Clear();
        currentChest.OnMouseUp();//simulates mouse up to close chest
        currentChest = null;
        _displayLootWindow = false;
    }
    public void InventoryWindow(int id)
    {
        int i = 0;
        for (int y = 0; y < _inventoryRows; y++)
        {
            for (int x = 0; x < _inventoryCols; x++)
            {
                if (i < Player.Inventory.Count)
                {
                    if (GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent(Player.Inventory[i].Icon, Player.Inventory[i].ToolTip()), "Inventory Slot Common"))//(x + y * _inventoryCols).ToString()
                    {
                        if (_doubleClickTimer != 0 && _selectedItem != null)
                        {
                            if (Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHOLD)
                            {
                                //Make Weapon
                                if (typeof(Weapon) == Player.Inventory[i].GetType())
                                {
                                    if (Player.EquipedWeapon == null)
                                    {
                                        Player.EquipedWeapon = Player.Inventory[i];
                                        Player.Inventory.RemoveAt(i);
                                    }
                                    else
                                    {
                                        Item temp = Player.EquipedWeapon;
                                        Player.EquipedWeapon = Player.Inventory[i];
                                        Player.Inventory[i] = temp;
                                    }
                                }//Make Armor                                   
                                else if (typeof(Armor) == Player.Inventory[i].GetType())
                                {
                                    Armor arm = (Armor)Player.Inventory[i];
                                    switch (arm.Slot)//it is inherintly Item type, we convert it to Armor to get access to .slot
                                    {
                                        case EquipmentSlot.Head:
                                            if (Player.EquipedHeadGear == null)
                                            {
                                                Player.EquipedHeadGear = Player.Inventory[i];
                                                Player.Inventory.RemoveAt(i);
                                            }
                                            else
                                            {
                                                Item temp = Player.EquipedHeadGear;
                                                Player.EquipedHeadGear = Player.Inventory[i];
                                                Player.Inventory[i] = temp;
                                            }

                                            break;

                                        case EquipmentSlot.Torso:
                                            break;

                                        case EquipmentSlot.Hands:
                                            break;

                                        case EquipmentSlot.Legs:
                                            break;

                                        case EquipmentSlot.Feet:
                                            break;

                                        case EquipmentSlot.OffHand:
                                            if (Player.EquipedShield == null)
                                            {
                                                Player.EquipedShield = Player.Inventory[i];
                                                Player.Inventory.RemoveAt(i);
                                            }
                                            else
                                            {
                                                Item temp = Player.EquipedShield;
                                                Player.EquipedShield = Player.Inventory[i];
                                                Player.Inventory[i] = temp;
                                            }

                                            break;

                                    }

                                }
                                
                                _doubleClickTimer = 0;
                                _selectedItem = null;
                            }
                            else
                            {
                                _doubleClickTimer = Time.time;
                            }
                        }
                        else 
                        {
                            _doubleClickTimer = Time.time;
                            _selectedItem = Player.Inventory[i];
                        }

                    }
                }
                else
                {
                    //if not showing inventory item, show empty box
                    GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), (x + y * _inventoryCols).ToString(), "Inventory Slot Common");
                }
                i++;
                
            }
        }
        SetToolTip();
        GUI.DragWindow();
    }

    public void ToggleInventoryWindow()
    {
        _displayInventoryWindow = !_displayInventoryWindow;
    }

    public void CharacterWindow(int id)
    {
        _characterPanel = GUI.Toolbar(new Rect(5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _characterPanelNames);

        switch (_characterPanel)
        { 
            case 0:
                DisplayEquipment();
                break;
            case 1:
                DisplayAttributes();
                break;
            case 2:
                DisplaySkills();
                break;
            default:
                break;
        }

        GUI.DragWindow();
    }

    public void ToggleCharacterWindow()
    {
        _displayCharacterWindow = !_displayCharacterWindow;
    }

    void DisplayEquipment()
    {
       // GUI.skin = mySkin;
        if (Player.EquipedWeapon == null)
        {
            GUI.Label(new Rect(5, 100, 40, 40), "", "Inventory Slot Common");
        }
        else 
        {
            if (GUI.Button(new Rect(5, 100, 40, 40), new GUIContent(Player.EquipedWeapon.Icon, Player.EquipedWeapon.ToolTip())))
            {
                Player.Inventory.Add(Player.EquipedWeapon);
                Player.EquipedWeapon = null;
            }
        }
        SetToolTip();
    }
    void DisplayAttributes()
    {
        Debug.Log("displaying attributes");
    }
    void DisplaySkills()
    {
        Debug.Log("displaying skills");
    }

    void SetToolTip()
    {
        if (Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
        {
            if (_toolTip != "")
            {
                _toolTip = "";                     
            }
            if (GUI.tooltip != "")
            {
                _toolTip = GUI.tooltip;
            }
        }
    }

    void DisplayToolTip()
    {
        if(_toolTip != "")
            GUI.Box(new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip);
    }
}
