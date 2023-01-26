using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDClassInfo
    {
        //FEATURES
        public string Name { get; set; }
        public HitDice HitDice { get; set; }
        public StatType[] SavingThrowProficiencies { get; set; }
        public SkillTypes[] SkillProficienciesChoices { get; set; }
        public int ChooseAmount { get; set; }

        public List<DNDClassFeature> ClassFeatures { get; set; }

        public List<DNDSubClassInfo> SubClasses { get; set; }
    }

    public class HitDice
    {
        public int Number { get; set; }
        public int Faces { get; set; }

        [JsonIgnore]
        public int AverageDiceRoll
        {
            get
            {
                float number = ((float)(1 + MaxDiceRoll) / 2) * Number;

                return (int) MathF.Ceiling(number);
            }
        }

        [JsonIgnore]
        public int MaxDiceRoll
        {
            get
            {
                return Number * Faces;
            }
        }

        public override string ToString()
        {
            return String.Concat(Number,"d",Faces);
        }
    }
}
