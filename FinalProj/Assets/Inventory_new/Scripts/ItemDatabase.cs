using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI_New
{

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("necklace", 0, "simple necklace", 2, 0, Item.ItemType.Quest));
        items.Add(new Item("helm", 1, "simple helmet", 2, 0, Item.ItemType.Quest)); 
    }
}

}