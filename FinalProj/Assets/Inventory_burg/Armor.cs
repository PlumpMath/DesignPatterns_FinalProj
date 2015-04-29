using UnityEngine;

public class Armor : Item
{
    private EquipmentSlot _slot;
    private int _armorLevel;//level of this piece of armor

    public Armor()
    {
        _armorLevel = 0;
        _slot = EquipmentSlot.Head;
    }

    public Armor(int al, EquipmentSlot slot)
    {
        _armorLevel = al;
        _slot = slot; 
    }

    public int ArmorLevel
    {
        get { return _armorLevel; }
        set { _armorLevel = value; }
    }

     public EquipmentSlot Slot
    {
        get { return _slot; }
        set { _slot = value; }
    }
}

