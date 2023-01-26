using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStatWisdom : DNDStat
    {
        private DNDSkill _animalHandling;
        public DNDSkill AnimalHandling
        {
            get
            {
                return _animalHandling;
            }
            set
            {
                _animalHandling = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _insight;
        public DNDSkill Insight
        {
            get
            {
                return _insight;
            }
            set
            {
                _insight = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _medicine;
        public DNDSkill Medicine
        {
            get
            {
                return _medicine;
            }
            set
            {
                _medicine = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _perception;
        public DNDSkill Perception
        {
            get
            {
                return _perception;
            }
            set
            {
                _perception = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _survival;
        public DNDSkill Survival
        {
            get
            {
                return _survival;
            }
            set
            {
                _survival = value;
                SetSkillsModifier();
            }
        }

        public DNDStatWisdom(StatType stype) : base(stype)
        {
            AnimalHandling = new DNDSkill(SkillTypes.AnimalHandling, SkillProficiency.NotProficient);
            Insight = new DNDSkill(SkillTypes.Insight, SkillProficiency.NotProficient);
            Medicine = new DNDSkill(SkillTypes.Medicine, SkillProficiency.NotProficient);
            Perception = new DNDSkill(SkillTypes.Perception, SkillProficiency.NotProficient);
            Survival = new DNDSkill(SkillTypes.Survival, SkillProficiency.NotProficient);

            SetSkillsModifier();
        }

        public DNDStatWisdom(DNDStatWisdom copy) : base(copy)
        {
            AnimalHandling = new DNDSkill(copy.AnimalHandling);
            Insight = new DNDSkill(copy.Insight);
            Medicine = new DNDSkill(copy.Medicine);
            Perception = new DNDSkill(copy.Perception);
            Survival = new DNDSkill(copy.Survival);

            SetSkillsModifier();
        }

        public override void SetSkillsModifier()
        {
            base.SetSkillsModifier();
            if (!ReferenceEquals(AnimalHandling, null))
            {
                AnimalHandling.Modifier = Modifier;
            }

            if (!ReferenceEquals(Insight, null))
            {
                Insight.Modifier = Modifier;
            }

            if (!ReferenceEquals(Medicine, null))
            {
                Medicine.Modifier = Modifier;
            }

            if (!ReferenceEquals(Perception, null))
            {
                Perception.Modifier = Modifier;
            }

            if (!ReferenceEquals(Survival, null))
            {
                Survival.Modifier = Modifier;
            }
        }

        public override void SetProficiencyModifier(int proficiency)
        {
            base.SetProficiencyModifier(proficiency);
            AnimalHandling.ProficiencyBonus = ProficiencyModifier;
            Insight.ProficiencyBonus = ProficiencyModifier;
            Medicine.ProficiencyBonus = ProficiencyModifier;
            Perception.ProficiencyBonus = ProficiencyModifier;
            Survival.ProficiencyBonus = ProficiencyModifier;
        }

        public override DNDSkill GetStatFromSkill(SkillTypes t)
        {
            switch (t)
            {
                case SkillTypes.AnimalHandling: return AnimalHandling;
                case SkillTypes.Insight: return Insight;
                case SkillTypes.Medicine: return Medicine;
                case SkillTypes.Survival: return Survival;
                case SkillTypes.Perception: return Perception;
            }

            return null;
        }
    }
}
