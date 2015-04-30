using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public GameObject rh, oh, hm;
    public static GameObject mount_rh_weapon;
    public static GameObject mount_offHand;
    public static GameObject mount_helmet;

    static List<Item> _inventory = new List<Item>();
    static GUI_Manager _gui;
    public static GameObject[] _weaponMesh;//assign in inspector. 
    public Transform weaponMount;
    protected static Item[] _equipment = new Item[(int)EquipmentSlot.COUNT];

    public static List<Item> Inventory
    {
        get { return _inventory; }        
    }
    static Item _equipedWeapon;
    public static Item EquipedWeapon
    {
        get { return _equipment[(int)EquipmentSlot.MainHand]; }
        set
        {
            _equipment[(int)EquipmentSlot.MainHand] = value;



            if (mount_rh_weapon.transform.childCount > 0)
                Destroy(mount_rh_weapon.transform.GetChild(0).gameObject);

            if (_equipment[(int)EquipmentSlot.MainHand] != null)
            {
                GameObject mesh = Instantiate(Resources.Load("item_Mesh/" + _equipment[(int)EquipmentSlot.MainHand].Name),
                                        mount_rh_weapon.transform.position, mount_rh_weapon.transform.rotation) as GameObject;
                mesh.transform.parent = mount_rh_weapon.transform;
            }
        }
    }

    void Awake()
    {
        //Transform weaponMount = transform.Find("root_jt/pelvis_jt/Main Hand");
        int count = weaponMount.childCount;        
        _weaponMesh = new GameObject[count];
        for (int i = 0; i < count; i++)           
        {
            _weaponMesh[i] = weaponMount.GetChild(i).gameObject;
        }
        HideWeaponMeshes();
    }
    void Start()
    {
        _gui = FindObjectOfType<GUI_Manager>();

        mount_rh_weapon = rh;
        mount_offHand = oh;
        mount_helmet = hm;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            _gui.ToggleInventoryWindow();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            _gui.ToggleCharacterWindow();
        }
    }

    static void HideWeaponMeshes()
    {
        for (int i = 0; i < _weaponMesh.Length; i++)
        {
            _weaponMesh[i].SetActive(false);
            Debug.Log(_weaponMesh[i].name);
        }
    }

    public static Item EquipedShield
    {
        get { return _equipment[(int)EquipmentSlot.OffHand]; }
        set
        {
            _equipment[(int)EquipmentSlot.OffHand] = value;

            if (mount_offHand.transform.childCount > 0)
                Destroy(mount_offHand.transform.GetChild(0).gameObject);

            if (_equipment[(int)EquipmentSlot.OffHand] != null)
            {
                GameObject mesh = Instantiate(Resources.Load("Item_Mesh/" + _equipment[(int)EquipmentSlot.OffHand].Name),
                                        mount_offHand.transform.position, mount_offHand.transform.rotation) as GameObject;
                mesh.transform.parent = mount_offHand.transform;
            }
        }
    }

    public static Item EquipedHeadGear
    {
        get { return _equipment[(int)EquipmentSlot.Head]; }
        set
        {
            _equipment[(int)EquipmentSlot.Head] = value;

            if (mount_helmet.transform.childCount > 0)
                Destroy(mount_helmet.transform.GetChild(0).gameObject);

            if (_equipment[(int)EquipmentSlot.Head] != null)
            {
                GameObject mesh = Instantiate(Resources.Load("Item_Mesh/" + _equipment[(int)EquipmentSlot.Head].Name),
                                        mount_helmet.transform.position, mount_helmet.transform.rotation) as GameObject;
                mesh.transform.parent = mount_helmet.transform;

            }
        }
    }
}
