using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStatCharisma : DNDStat
    {
        private DNDSkill _deception;
        public DNDSkill Deception
        {
            get
            {
                return _deception;
            }
            set
            {
                _deception = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _intimidation;
        public DNDSkill Intimidation
        {
            get
            {
                return _intimidation;
            }
            set
            {
                _intimidation = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _performance;
        public DNDSkill Performance
        {
            get
            {
                return _performance;
            }
            set
            {
                _performance = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _persuasion;
        public DNDSkill Persuasion
        {
            get
            {
                return _persuasion;
            }
            set
            {
                _persuasion = value;
                SetSkillsModifier();
            }
        }

        public DNDStatCharisma(StatType stype) : base(stype)
        {
            Deception = new DNDSkill(SkillTypes.Deception, SkillProficiency.NotProficient);
            Intimidation = new DNDSkill(SkillTypes.Intimidation, SkillProficiency.NotProficient);
            Performance = new DNDSkill(SkillTypes.Performance, SkillProficiency.NotProficient);
            Persuasion = new DNDSkill(SkillTypes.Persuasion, SkillProficiency.NotProficient);

            SetSkillsModifier();
        }

        public DNDStatCharisma(DNDStatCharisma copy) : base(copy)
        {
            Deception = new DNDSkill(copy.Deception);
            Intimidation = new DNDSkill(copy.Intimidation);
            Performance = new DNDSkill(copy.Performance);
            Persuasion = new DNDSkill(copy.Persuasion);

            SetSkillsModifier();
        }

        public override void SetSkillsModifier()
        {
            base.SetSkillsModifier();
            if (!ReferenceEquals(Deception, null))
            {
                Deception.Modifier = Modifier;
            }

            if (!ReferenceEquals(Intimidation, null))
            {
                Intimidation.Modifier = Modifier;
            }

            if (!ReferenceEquals(Performance, null))
            {
                Performance.Modifier = Modifier;
            }

            if (!ReferenceEquals(Persuasion, null))
            {
                Persuasion.Modifier = Modifier;
            }
        }

        public override void SetProficiencyModifier(int proficiency)
        {
            base.SetProficiencyModifier(proficiency);
            Deception.ProficiencyBonus = ProficiencyModifier;
            Intimidation.ProficiencyBonus = ProficiencyModifier;
            Performance.ProficiencyBonus = ProficiencyModifier;
            Persuasion.ProficiencyBonus = ProficiencyModifier;
        }

        public override DNDSkill GetStatFromSkill(SkillTypes t)
        {
            switch (t)
            {
                case SkillTypes.Deception:
                    return Deception;
                case SkillTypes.Intimidation:
                    return Intimidation;
                case SkillTypes.Performance:
                    return Performance;
                case SkillTypes.Persuasion:
                    return Persuasion;
            }

            return null;
        }
    }
}
