using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        VeryRare,
        Legendary,
        Artifact,
    }

    public enum ItemType
    {
        Armour,
    }

    public class DNDItem
    {
        public string Name { get; set; }
        public ItemRarity Rarity { get; set; }
        public float Weight { get; set; }
        //attunement

    }
}
