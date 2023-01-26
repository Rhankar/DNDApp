using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStatStrength : DNDStat
    {
        private DNDSkill _athletics;
        public DNDSkill Athletics { 
            get
            {
                return _athletics;
            }
            set
            {
                _athletics = value;
                SetSkillsModifier();
            }
        }

        public DNDStatStrength(StatType stype) : base(stype)
        {
            Athletics = new DNDSkill(SkillTypes.Athletics, SkillProficiency.NotProficient);

            SetSkillsModifier();
        }

        public DNDStatStrength(DNDStatStrength copy) : base(copy)
        {
            Athletics = new DNDSkill(copy.Athletics);

            SetSkillsModifier();
        }

        public override void SetSkillsModifier()
        {
            base.SetSkillsModifier();
            if(!ReferenceEquals(Athletics, null))
            {
                Athletics.Modifier = Modifier;
            }
        }

        public override void SetProficiencyModifier(int proficiency)
        {
            base.SetProficiencyModifier(proficiency);
            Athletics.ProficiencyBonus = ProficiencyModifier;
        }

        public override DNDSkill GetStatFromSkill(SkillTypes t)
        {
            switch (t)
            {
                case SkillTypes.Athletics: 
                    return Athletics;
            }

            return null;
        }
    }
}
