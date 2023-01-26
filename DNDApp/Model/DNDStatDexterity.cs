using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStatDexterity : DNDStat
    {
        private DNDSkill _acrobatics;
        public DNDSkill Acrobatics
        {
            get
            {
                return _acrobatics;
            }
            set
            {
                _acrobatics = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _sleightOfHand;
        public DNDSkill SleightOfHand
        {
            get
            {
                return _sleightOfHand;
            }
            set
            {
                _sleightOfHand = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _stealth;
        public DNDSkill Stealth
        {
            get
            {
                return _stealth;
            }
            set
            {
                _stealth = value;
                SetSkillsModifier();
            }
        }

        public DNDStatDexterity(StatType stype) : base(stype)
        {
            Acrobatics = new DNDSkill(SkillTypes.Acrobatics, SkillProficiency.NotProficient);
            SleightOfHand = new DNDSkill(SkillTypes.SleightOfHand, SkillProficiency.Expert);
            Stealth = new DNDSkill(SkillTypes.Stealth, SkillProficiency.Proficient);

            SetSkillsModifier();
        }

        public DNDStatDexterity(DNDStatDexterity copy) : base(copy)
        {
            Acrobatics = new DNDSkill(copy.Acrobatics);
            SleightOfHand = new DNDSkill(copy.SleightOfHand);
            Stealth = new DNDSkill(copy.Stealth);

            SetSkillsModifier();
        }

        public override void SetSkillsModifier()
        {
            base.SetSkillsModifier();
            if (!ReferenceEquals(Acrobatics, null))
            {
                Acrobatics.Modifier = Modifier;
            }

            if (!ReferenceEquals(SleightOfHand, null))
            {
                SleightOfHand.Modifier = Modifier;
            }

            if (!ReferenceEquals(Stealth, null))
            {
                Stealth.Modifier = Modifier;
            }
        }

        public override void SetProficiencyModifier(int proficiency)
        {
            base.SetProficiencyModifier(proficiency);
            Acrobatics.ProficiencyBonus = ProficiencyModifier;
            SleightOfHand.ProficiencyBonus = ProficiencyModifier;
            Stealth.ProficiencyBonus = ProficiencyModifier;
        }

        public override DNDSkill GetStatFromSkill(SkillTypes t)
        {
            switch (t)
            {
                case SkillTypes.Acrobatics: return Acrobatics;
                case SkillTypes.SleightOfHand: return SleightOfHand;
                case SkillTypes.Stealth: return Stealth;
            }

            return null;
        }
    }
}
