﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI_New
{

public class Inventory : MonoBehaviour
{
    public int slotsX = 5, slotsY = 4;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public bool showInventory = false;
    public ItemDatabase database;

    public void Start()
    {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            inventory.Add(new Item());
            slots.Add(new Item());
        }

        inventory[0] = database.items[0];
        inventory[1] = database.items[1];
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if(showInventory)        
        {
            DrawInventory();
        }
       // for (int i = 0; i < inventory.Count; i++)       
            //GUI.Label(new Rect(10, 10 * i, 200, 50), inventory[i].itemName);
        
    }

    void DrawInventory()
    {
        int k = 0;
        for (int x = 0; x < slotsX; x++)
        {
            for (int y = 0; y < slotsY; y++)
            {                
                Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
                GUI.Box(slotRect, (k).ToString(), skin.GetStyle("slot"));
                slots[k] = inventory[k];
                if(slots[k].itemName != null)
                {                    
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);
                }
                k++;
            }
        
        }
    }
}
} 