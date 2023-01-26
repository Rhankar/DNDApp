using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public enum StatType
    {
        STRENGTH,
        DEXTERITY,
        CONSTITUTION,
        INTELLIGENCE,
        WISDOM,
        CHARISMA,
    }

    

    public abstract class DNDStat : INotifyPropertyChanged
    {
        public const int MAX_STAT_VALUE = 20;

        public StatType SType { get; set; }

        private DNDSkill _savingThrow;
        public DNDSkill SavingThrow
        {
            get
            {
                return _savingThrow;
            }
            set
            {
                _savingThrow = value;
                SetSkillsModifier();
            }
        }

        private int _baseValue;
        public int BaseValue { get
            {
                return _baseValue;
            }
            set
            {
                if(_baseValue == value) return;

                _baseValue = value;

                //Calculate Skills Modifier
                SetSkillsModifier();
                OnPropertyChanged(nameof(BaseValue));
                OnPropertyChanged(nameof(TotalValue));
                OnPropertyChanged(nameof(Modifier));
            }
        }

        private int _statBonuses;
        public int StatBonuses
        {
            get 
            {
                return _statBonuses;
            }
            set
            {
                if (_statBonuses == value) return;

                _statBonuses = value;
                OnPropertyChanged(nameof(TotalValue));
                OnPropertyChanged(nameof(Modifier));
            }
        }

        public int TotalValue
        {
            get 
            {
                int total = Math.Clamp(BaseValue + StatBonuses, 0, MAX_STAT_VALUE);
                //add uncapped bonuses

                return total;
            }
        }

        public int Modifier
        {
            get
            {
                return (TotalValue / 2) - 5;
            }
        }

        public int ProficiencyModifier { get; set; }
        //STAT changes
        //MAX STAT changes
        //OVERRIDE changes

        //public List<DNDSkill> SkillList { get; set; }

        //OnStatChange

        public DNDStat(StatType statType)
        {
            SType = statType;

            SavingThrow = new DNDSkill(SkillTypes.SavingThrow, SkillProficiency.NotProficient);
            //SkillList = new List<DNDSkill>();
            //PopulateSkills();
        }

        public DNDStat(DNDStat copy)
        {
            SType = copy.SType;
            BaseValue = copy.BaseValue;
            SavingThrow = new DNDSkill(copy.SavingThrow);
        }

        public virtual void SetSkillsModifier()
        {
            if (!ReferenceEquals(SavingThrow, null))
            {
                SavingThrow.Modifier = Modifier;
            }
        }
        public virtual void SetProficiencyModifier(int proficiency)
        {
            ProficiencyModifier = proficiency;
            SavingThrow.ProficiencyBonus = ProficiencyModifier;
        }

        public abstract DNDSkill GetStatFromSkill(SkillTypes t);

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
