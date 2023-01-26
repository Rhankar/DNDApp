using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStatConstitution : DNDStat
    {
        public DNDStatConstitution(StatType stype) : base(stype)
        {
            SetSkillsModifier();
        }

        public DNDStatConstitution(DNDStatConstitution copy) : base(copy)
        {
            SetSkillsModifier();
        }

        public override void SetSkillsModifier()
        {
            base.SetSkillsModifier();
        }

        public override void SetProficiencyModifier(int proficiency)
        {
            base.SetProficiencyModifier(proficiency);
        }

        public override DNDSkill GetStatFromSkill(SkillTypes t)
        {
            return null;
        }
    }
}
