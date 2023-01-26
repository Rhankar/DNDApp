using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp.Model
{
    public interface IEquippable
    {
        void EquipItem(DNDCharacter owner);
    }
}
