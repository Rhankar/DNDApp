using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    public class AddActionEffect : DNDFeatureEffect
    {
        public DNDAction actionToAdd;

        public override void ApplyEffect(DNDCharacter owner)
        {
            //Add action
            owner.AddAction(actionToAdd);
        }

        public override void RemoveEffect(DNDCharacter owner)
        {
            //Remove Action
            owner.RemoveAction(actionToAdd);
        }
    }
}
