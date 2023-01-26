using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDStats : INotifyPropertyChanged
    {
        public DNDStatStrength Strength { get; set; }
        public DNDStatDexterity Dexterity { get; set; }
        public DNDStatConstitution Constitution { get; set; }
        public DNDStatIntelligence Intelligence { get; set; }
        public DNDStatWisdom Wisdom { get; set; }
        public DNDStatCharisma Charisma { get; set; }

        public DNDStats()
        {
            Strength = new DNDStatStrength(StatType.STRENGTH);
            Strength.BaseValue = 2;
            Dexterity = new DNDStatDexterity(StatType.DEXTERITY);
            Constitution = new DNDStatConstitution(StatType.CONSTITUTION);
            Intelligence = new DNDStatIntelligence(StatType.INTELLIGENCE);
            Wisdom = new DNDStatWisdom(StatType.WISDOM);
            Charisma = new DNDStatCharisma(StatType.CHARISMA);
        }

        public DNDStats(DNDStats copy)
        {
            Strength = new DNDStatStrength(copy.Strength);
            Dexterity = new DNDStatDexterity(copy.Dexterity);
            Constitution = new DNDStatConstitution(copy.Constitution);
            Intelligence = new DNDStatIntelligence(copy.Intelligence);
            Wisdom = new DNDStatWisdom(copy.Wisdom);
            Charisma = new DNDStatCharisma(copy.Charisma);
        }

        public void RecalculateStatSkillsModifiers()
        {
            Strength.SetSkillsModifier();
            Dexterity.SetSkillsModifier();
            Constitution.SetSkillsModifier();
            Intelligence.SetSkillsModifier();
            Wisdom.SetSkillsModifier();
            Charisma.SetSkillsModifier();
        }

        public void SetProficiencyBonus(int proficiency)
        {
            Strength.SetProficiencyModifier(proficiency);
            Dexterity.SetProficiencyModifier(proficiency);
            Constitution.SetProficiencyModifier(proficiency);
            Intelligence.SetProficiencyModifier(proficiency);
            Wisdom.SetProficiencyModifier(proficiency);
            Charisma.SetProficiencyModifier(proficiency);
        }

        public void AddStatBonus(StatType statType, int quantity)
        {
            DNDStat statToChange = GetStat(statType);

            statToChange.StatBonuses += quantity;
            statToChange.SetSkillsModifier();
        }

        public void RemoveStatBonus(StatType statType, int quantity)
        {
            DNDStat statToChange = GetStat(statType);

            statToChange.StatBonuses -= quantity;
            statToChange.SetSkillsModifier();
        }

        public void ApplySkillProficiency(SkillTypes skillToImprove)
        {
            DNDSkill skill = GetSkillFromEnum(skillToImprove);

            if (!ReferenceEquals(skill, null))
            {
                //skill.Proficiency = SkillProficiency.Proficient;
                skill.AddProficiency(SkillProficiency.Proficient);
            }
        }

        public void RemoveSkillProficiency(SkillTypes skillType)
        {
            DNDSkill skill = GetSkillFromEnum(skillType);

            if (!ReferenceEquals(skill, null))
            {
                //skill.Proficiency = SkillProficiency.Proficient;
                skill.RemoveProficiency(SkillProficiency.Proficient);
            }
        }

        public void AddSavingThrowProficiency(StatType stat)
        {
            DNDStat s = GetStat(stat);

            s.SavingThrow.AddProficiency(SkillProficiency.Proficient);
        }

        public void RemoveSavingThrowProficiency(StatType stat)
        {
            DNDStat s = GetStat(stat);

            s.SavingThrow.RemoveProficiency(SkillProficiency.Proficient);
        }

        private DNDSkill GetSkillFromEnum(SkillTypes skillType)
        {
            DNDSkill skill = null;

            switch (skillType)
            {
                case SkillTypes.Athletics:
                    skill = Strength.GetStatFromSkill(skillType);
                    break;
                case SkillTypes.Acrobatics:
                case SkillTypes.SleightOfHand:
                case SkillTypes.Stealth:
                    skill = Dexterity.GetStatFromSkill(skillType);
                    break;
                case SkillTypes.Arcana:
                case SkillTypes.History:
                case SkillTypes.Investigation:
                case SkillTypes.Nature:
                case SkillTypes.Religion:
                    skill = Intelligence.GetStatFromSkill(skillType);
                    break;
                case SkillTypes.AnimalHandling:
                case SkillTypes.Insight:
                case SkillTypes.Medicine:
                case SkillTypes.Perception:
                case SkillTypes.Survival:
                    skill = Wisdom.GetStatFromSkill(skillType);
                    break;
                case SkillTypes.Deception:
                case SkillTypes.Intimidation:
                case SkillTypes.Performance:
                case SkillTypes.Persuasion:
                    skill = Charisma.GetStatFromSkill(skillType);
                    break;
                case SkillTypes.SavingThrow:
                    break;
                default:
                    break;
            }

            return skill;
        }

        private DNDStat GetStat(StatType statType)
        {
            switch (statType)
            {
                case StatType.STRENGTH:
                    return Strength;
                case StatType.DEXTERITY:
                    return Dexterity;
                case StatType.CONSTITUTION:
                    return Constitution;
                case StatType.INTELLIGENCE:
                    return Intelligence;
                case StatType.WISDOM:
                    return Wisdom;
                case StatType.CHARISMA:
                    return Charisma;
                default:
                    return Strength;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
