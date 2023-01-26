using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public abstract class DNDFeatureEffect
    {
        public abstract void ApplyEffect(DNDCharacter owner);
        public abstract void RemoveEffect(DNDCharacter owner);
    }

    public class AddProficiencyEffect : DNDFeatureEffect
    {
        public SkillTypes skillToImprove;

        public override void ApplyEffect(DNDCharacter owner)
        {
            owner.ApplySkillProficiency(skillToImprove);
        }

        public override void RemoveEffect(DNDCharacter owner)
        {
            owner.RemoveSkillProficiency(skillToImprove);
        }
    }
}
