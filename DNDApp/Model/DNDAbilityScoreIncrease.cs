using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDAbilityScoreIncrease
    {
        private DNDCharacter _owner;

        private StatType _statType;
        public StatType StatType { 
            get 
            {
                return _statType;
            } 
            set 
            {
                if(value == _statType) return;

                RemoveStatBonus();
                Debug.WriteLine("HELLO");
                _statType = value;
                ApplyStatBonus();
            }
        }
        public int Quantity { get; set; }

        public DNDAbilityScoreIncrease(StatType t, int q, DNDCharacter owner)
        {
            _owner = owner;

            _statType = t;
            Quantity = q;
        }

        public void ApplyStatBonus()
        {
            _owner.AddStatBonus(_statType, Quantity);
        }

        public void RemoveStatBonus()
        {
            _owner.RemoveStatBonus(_statType, Quantity);
        }
    }
}
