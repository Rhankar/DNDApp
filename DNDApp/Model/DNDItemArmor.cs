using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp.Model
{
    public enum ArmorType
    {
        Light,
        Medium,
        Heavy,
        None,
    }

    public class DNDItemArmor : DNDItem, IEquippable
    {
        public const int MAX_DEX_MEDIUM_ARMOR = 2;
        public const int NO_ARMOR_BASE_AC = 10;

        private DNDCharacter _owner;

        public ArmorType Type { get; set; }
        public int BaseAC { get; set; }

        public void EquipItem(DNDCharacter owner)
        {
            //shrug
            _owner = owner;
            owner.ApplyArmorStats(BaseAC, Type);
        }
    }
}
