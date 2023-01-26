using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStatIntelligence : DNDStat
    {
        private DNDSkill _arcana;
        public DNDSkill Arcana
        {
            get
            {
                return _arcana;
            }
            set
            {
                _arcana = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _history;
        public DNDSkill History
        {
            get
            {
                return _history;
            }
            set
            {
                _history = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _investigation;
        public DNDSkill Investigation
        {
            get
            {
                return _investigation;
            }
            set
            {
                _investigation = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _nature;
        public DNDSkill Nature
        {
            get
            {
                return _nature;
            }
            set
            {
                _nature = value;
                SetSkillsModifier();
            }
        }
        private DNDSkill _religion;
        public DNDSkill Religion
        {
            get
            {
                return _religion;
            }
            set
            {
                _religion = value;
                SetSkillsModifier();
            }
        }

        public DNDStatIntelligence(StatType stype) : base(stype)
        {
            Arcana = new DNDSkill(SkillTypes.Arcana, SkillProficiency.NotProficient);
            History = new DNDSkill(SkillTypes.History, SkillProficiency.NotProficient);
            Investigation = new DNDSkill(SkillTypes.Investigation, SkillProficiency.NotProficient);
            Nature = new DNDSkill(SkillTypes.Nature, SkillProficiency.NotProficient);
            Religion = new DNDSkill(SkillTypes.Religion, SkillProficiency.NotProficient);

            SetSkillsModifier();
        }

        public DNDStatIntelligence(DNDStatIntelligence copy) : base(copy)
        {
            Arcana = new DNDSkill(copy.Arcana);
            History = new DNDSkill(copy.History);
            Investigation = new DNDSkill(copy.Investigation);
            Nature = new DNDSkill(copy.Nature);
            Religion = new DNDSkill(copy.Religion);

            SetSkillsModifier();
        }

        public override void SetSkillsModifier()
        {
            base.SetSkillsModifier();
            if (!ReferenceEquals(Arcana, null))
            {
                Arcana.Modifier = Modifier;
            }

            if (!ReferenceEquals(History, null))
            {
                History.Modifier = Modifier;
            }

            if (!ReferenceEquals(Investigation, null))
            {
                Investigation.Modifier = Modifier;
            }

            if (!ReferenceEquals(Nature, null))
            {
                Nature.Modifier = Modifier;
            }

            if (!ReferenceEquals(Religion, null))
            {
                Religion.Modifier = Modifier;
            }
        }

        public override void SetProficiencyModifier(int proficiency)
        {
            base.SetProficiencyModifier(proficiency);

            Arcana.ProficiencyBonus = ProficiencyModifier;
            History.ProficiencyBonus = ProficiencyModifier;
            Investigation.ProficiencyBonus = ProficiencyModifier;
            Nature.ProficiencyBonus = ProficiencyModifier;
            Religion.ProficiencyBonus = ProficiencyModifier;
        }

        public override DNDSkill GetStatFromSkill(SkillTypes t)
        {
            switch (t)
            {
                case SkillTypes.Arcana: return Arcana;
                case SkillTypes.Investigation: return Investigation;
                case SkillTypes.Nature: return Nature;
                case SkillTypes.Religion: return Religion;
                case SkillTypes.History: return History;
            }

            return null;
        }
    }
}
