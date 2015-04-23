using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI_New
{

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public ItemDatabase database;

    public void Start()
    {
        inventory.Add(database.items[0]);
        Debug.Log("first item: " + inventory[0].itemName);
    }

    void OnGUI()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10, 10 * i, 200, 50), inventory[i].itemName);
        }
    }
}
}