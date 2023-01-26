using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public enum SkillTypes
    {
        Athletics = 0,

        Acrobatics = 1,
        SleightOfHand = 2,
        Stealth = 3,

        Arcana = 4,
        History = 5,
        Investigation = 6,
        Nature = 7,
        Religion = 8,

        AnimalHandling = 9,
        Insight = 10,
        Medicine = 11,
        Perception = 12,
        Survival = 13,

        Deception = 14,
        Intimidation = 15,
        Performance = 16,
        Persuasion = 17,

        SavingThrow = 18,
    }

    public enum SkillProficiency
    {
        NotProficient = 0,
        Proficient = 1,
        Expert = 2,
    }

    public class DNDSkill : INotifyPropertyChanged
    {
        public SkillTypes SkillType { get; set; }

        private List<SkillProficiency> _skillProficiencies;
        public List<SkillProficiency> SkillProficiencies { 
            get 
            {
                if (ReferenceEquals(_skillProficiencies, null))
                {
                    _skillProficiencies = new List<SkillProficiency>();
                }

                return _skillProficiencies;
            } 
            set
            {
                _skillProficiencies = value;
            }
        }
        private SkillProficiency _proficiency;
        public SkillProficiency Proficiency { 
            get
            {
                SkillProficiency p = SkillProficiency.NotProficient;

                foreach (SkillProficiency skillProficiency in _skillProficiencies)
                {
                    if (skillProficiency > p)
                    {
                        p = skillProficiency;
                    }
                }

                return p;
            }
            //set
            //{
            //    if(_proficiency == value) return;

            //    _proficiency = value;
            //    OnPropertyChanged(nameof(Proficiency));
            //}
        }

        private int _modifier;
        public int Modifier { 
            get
            {
                switch (Proficiency)
                {
                    case SkillProficiency.NotProficient:
                        return _modifier;
                    case SkillProficiency.Proficient:
                        return _modifier + ProficiencyBonus;
                    case SkillProficiency.Expert:
                        return _modifier + ProficiencyBonus * 2;
                    default:
                        return _modifier;
                }
            }
            set
            {
                if (_modifier == value) return;
                
                _modifier = value;
                OnPropertyChanged(nameof(Modifier));
            }
        }
        //modifier changes
        private int _proficiencyBonus;
        public int ProficiencyBonus { 
            get
            {
                return _proficiencyBonus;
            }
            set
            {
                if (_proficiencyBonus == value) return;

                _proficiencyBonus = value;
                OnPropertyChanged(nameof(Modifier));
            } 
        }

        public DNDSkill(SkillTypes skillType, SkillProficiency skillProficiency)
        {
            SkillProficiencies = new List<SkillProficiency>();

            if (skillType == SkillTypes.SavingThrow && skillProficiency == SkillProficiency.Expert)
            {
                throw new Exception("Saving Throws can't be Expert");
            }

            SkillType = skillType;
            if (skillProficiency != SkillProficiency.NotProficient)
            {
                SkillProficiencies.Add(skillProficiency);
            }
            //Proficiency = skillProficiency;
        }

        public DNDSkill(DNDSkill copy)
        {
            SkillType = copy.SkillType;
            //Proficiency = copy.Proficiency;
            SkillProficiencies = new List<SkillProficiency>();
            SkillProficiencies.AddRange(copy.SkillProficiencies);
        }

        public void AddProficiency(SkillProficiency proficiency)
        {
            SkillProficiencies.Add(proficiency);
            OnPropertyChanged(nameof(Proficiency));
            OnPropertyChanged(nameof(Modifier));
        }

        public void RemoveProficiency(SkillProficiency proficiency)
        {
            SkillProficiencies.Remove(proficiency);
            OnPropertyChanged(nameof(Proficiency));
            OnPropertyChanged(nameof(Modifier));
        }

        public bool IsSavingThrow()
        {
            return SkillType == SkillTypes.SavingThrow;
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
