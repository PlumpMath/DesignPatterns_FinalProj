//Written by Sam Arutyunyan for Design Patterns Project Spring 2015
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        //public GameObject rh, oh, hm;
        public static GameObject mount_rh_weapon;
        public static GameObject mount_offHand;
        public static GameObject mount_helmet;

        static List<Item> _inventory = new List<Item>();

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
                    GameObject mesh = Instantiate(Resources.Load("item_Mesh/" + _equipment[(int)EquipmentSlot.MainHand].Name) as GameObject);
                    mesh.transform.position = mount_rh_weapon.transform.position;

                    mesh.transform.parent = mount_rh_weapon.transform;
                    mesh.transform.localPosition = Vector3.zero;
                    mesh.transform.localRotation = Quaternion.identity;
                    //mesh.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }

        void Awake()
        {

        }
        void Start()
        {
            mount_rh_weapon = GameObject.Find("mount_rh");
            mount_offHand = GameObject.Find("mount_lh"); ;
            mount_helmet = GameObject.Find("mount_head"); ;
        }
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                GUI_Manager.instance.ToggleInventoryWindow();
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                GUI_Manager.instance.ToggleCharacterWindow();
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
}