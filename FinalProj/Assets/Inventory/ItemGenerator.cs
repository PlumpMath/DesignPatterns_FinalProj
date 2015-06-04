//Written by Sam Arutyunyan for Design Patterns Project Spring 2015
using UnityEngine;
namespace inventory
{
    public class ItemGenerator
    {
        public const int BASE_MELEE_RANGE = 1;
        public const int BASE_RANGED_RANGE = 5;
        const string MELEE_WEAPON_PATH = "Item_Icons/Weapon/";

        public static Item CreateItem()
        {
            int rand = Random.Range(0, (int)ItemType.COUNT);
            Item item = CreateItem((ItemType)rand);

            return item;
        }

        public static Item CreateItem(ItemType t)//Creates an Item of the specified type
        {
            Item item = new Item();

            switch (t)
            {
                case ItemType.MeleeWeapon:
                    item = CreateMeleeWeapon();
                    break;
                // case ItemType.RangedWeapon:
                //   item = CreateRangedWeapon();
                //  break;
                case ItemType.Armor:
                    item = CreateArmor();
                    break;
            }

            item.Value = Random.Range(1, 101);
            item.Rarity = RarityTypes.Common;
            item.MaxDurability = Random.Range(50, 61);
            item.CurDurability = item.MaxDurability;
            return item;
        }

        public static Weapon CreateWeapon()
        {
            //decide for melee or ranged weapon
            Weapon weapon = CreateMeleeWeapon();

            return weapon;

        }
        public static Weapon CreateMeleeWeapon()
        {
            Weapon meleeWeapon = new Weapon();

            string[] weaponNames = new string[3];
            weaponNames[0] = "Sword";
            weaponNames[1] = "Mace";
            weaponNames[2] = "Axe";

            meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Length)];

            meleeWeapon.MaxDamage = Random.Range(5, 11);//5-10

            meleeWeapon.DamageVariance = Random.Range(.2f, .75f); //from 20 to 75 % variance

            meleeWeapon.TypeOfDamage = DamageType.Slash;

            meleeWeapon.MaxRange = BASE_MELEE_RANGE;

            meleeWeapon.Icon = Resources.Load(MELEE_WEAPON_PATH + meleeWeapon.Name) as Texture2D;

            return meleeWeapon;
        }
        private static Armor CreateArmor()
        {
            //decide type of armor to create. 
            int temp = Random.Range(0, 2);//between eitehr 0 or 1

            Armor armor = new Armor();

            switch (temp)
            {
                case 0:
                    armor = CreateShield();
                    break;
                case 1:
                    armor = CreateHelm();
                    break;
            }



            return armor;
        }

        private static Armor CreateShield()
        {
            Armor armor = new Armor();

            //array of all melee weapons we can make (looking at our directory of textures? >_>
            string[] shieldNames = new string[]
        {
            "Small Shield", 
            "Large Shield"
        };

            //fill in all values for built in type
            armor.Name = shieldNames[Random.Range(0, shieldNames.Length)];

            //assign properties for shield:
            armor.ArmorLevel = Random.Range(10, 17);



            //assign icon for the weapon
            armor.Icon = Resources.Load(MELEE_WEAPON_PATH + armor.Name) as Texture2D;

            //assign eq slot where this can be assigned. 
            armor.Slot = EquipmentSlot.OffHand;

            return armor;
        }


        private static Armor CreateHelm()
        {
            Armor armor = new Armor();

            //array of all melee weapons we can make (looking at our directory of textures? >_>
            string[] hatNames = new string[]
        {
            "Large Helm", 
            "Small Helm"
        };

            //fill in all values for built in type
            armor.Name = hatNames[Random.Range(0, hatNames.Length)];

            //assign properties for hat:
            armor.ArmorLevel = Random.Range(10, 17);



            //assign icon for the weapon
            armor.Icon = Resources.Load(MELEE_WEAPON_PATH + armor.Name) as Texture2D;

            //assign eq slot where this can be assigned. 
            armor.Slot = EquipmentSlot.Head;

            return armor;
        }
    }
}