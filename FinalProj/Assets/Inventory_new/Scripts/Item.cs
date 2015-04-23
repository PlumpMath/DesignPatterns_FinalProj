using UnityEngine;
using System.Collections;

namespace UI_New
{
[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public int itemPower;
    public int itemSpeed;
    public ItemType itemType;

    public enum ItemType
    { 
        Weapon,
        Consumable,
        Quest
    }

    public Item(string name, int id, string desc, int power, int speed, ItemType type) //constructor
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemPower = power;
        itemSpeed = speed;
        itemType = type;
    }
    
}

}
