using DNDApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DNDApp
{
    public enum Alignments
    {
        [Alignment("Lawful Good")] LAWFUL_GOOD,
        [Alignment("Lawful Neutral")] LAWFUL_NEUTRAL,
        [Alignment("Lawful Evil")] LAWFUL_EVIL,
        [Alignment("Neutral Good")] NEUTRAL_GOOD,
        [Alignment("True Neutral")] TRUE_NEUTRAL,
        [Alignment("Neutral Evil")] NEUTRAL_EVIL,
        [Alignment("Chaotic Good")] CHAOTIC_GOOD,
        [Alignment("Chaotic Neutral")] CHAOTIC_NEUTRAL,
        [Alignment("Chaotic Evil")] CHAOTIC_EVIL,
    }

    public class AlignmentAttribute : Attribute
    {
        public string Name { get; }

        public AlignmentAttribute(string name)
        {
            Name = name;
        }
    }

    public static class AlignmentsExtension
    {
        public static string GetName(this Alignments alignment)
        {
            return GetAttribute(alignment).Name;
        }

        public static AlignmentAttribute GetAttribute(Alignments alignment)
        {
            return (AlignmentAttribute)Attribute.GetCustomAttribute(ForValue(alignment), typeof(AlignmentAttribute));
        }

        public static MemberInfo ForValue(Alignments alignment)
        {
            return typeof(Alignments).GetField(Enum.GetName(typeof(Alignments), alignment));
        }
    }

    public class DNDCharacter : INotifyPropertyChanged
    {
        public const int MAX_LEVEL = 20;

        public DNDCharacterBiography Biography { get; set; }
        //public string Race { get; set; }
        //BACKGROUND
        public Alignments Alignment { get; set; }
        public string Pronouns { get; set; }
        public string PlayerName { get; set; }

        private ObservableCollection<DNDClass> _dndClasses;
        public ObservableCollection<DNDClass> DNDClasses
        {
            get
            {
                if (ReferenceEquals(null, _dndClasses))
                {
                    _dndClasses = new ObservableCollection<DNDClass>();
                }

                return _dndClasses;
            }
            set
            {
                _dndClasses = value;
                OnPropertyChanged(nameof(DNDClasses));
            }
        }

        public int CurrentLevel
        {
            get
            {
                int lvl = 0;

                foreach (DNDClass c in DNDClasses)
                {
                    lvl += c.Level;
                }

                return lvl;
            }
        }

        public int ProficiencyBonus
        {
            get
            {
                return (CurrentLevel - 1) / 4 + 2; //(LEVELS - 1)/4 +2
            }
        }
        public DNDStats Stats { get; set; }
        public int MaxHP
        {
            get
            {
                int bonusConHP = Stats.Constitution.Modifier >= 0 ? (Stats.Constitution.Modifier * CurrentLevel) : 0;

                if (DNDClasses.Count <= 0)
                {
                    return bonusConHP;
                }
                return DNDClasses[0].GetTotalRolledHP() + bonusConHP;
            }
        }

        private int _currentHP;
        public int CurrentHP
        {
            get
            {
                return _currentHP;
            }
            set
            {
                if (_currentHP == value) return;

                _currentHP = Math.Clamp(value, 0, MaxHP);
            }
        }

        private int armorAC = 0;
        private ArmorType equippedArmorType = ArmorType.None;
        public int AC
        {
            get
            {
                int total = 0;

                switch (equippedArmorType)
                {
                    case ArmorType.Light:
                        total = armorAC + Stats.Dexterity.Modifier;
                        break;
                    case ArmorType.Medium:
                        total = armorAC + Math.Clamp(Stats.Dexterity.Modifier, 0, DNDItemArmor.MAX_DEX_MEDIUM_ARMOR);
                        break;
                    case ArmorType.Heavy:
                        total = armorAC;
                        break;
                    default:
                        total = DNDItemArmor.NO_ARMOR_BASE_AC + Stats.Dexterity.Modifier;
                        break;
                }

                return total;
            }
        }

        private ObservableCollection<DNDItem> _inventory;
        public ObservableCollection<DNDItem> Inventory
        {
            get
            {
                if (ReferenceEquals(null, _inventory))
                {
                    _inventory = new ObservableCollection<DNDItem>();
                }

                return _inventory;
            }
            set
            {
                _inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        private ObservableCollection<DNDItem> _equippedInventory;
        public ObservableCollection<DNDItem> EquippedInventory
        {
            get
            {
                if (ReferenceEquals(null, _equippedInventory))
                {
                    _equippedInventory = new ObservableCollection<DNDItem>();
                }

                return _equippedInventory;
            }
            set
            {
                _equippedInventory = value;
                OnPropertyChanged(nameof(EquippedInventory));
            }
        }

        private ObservableCollection<DNDAction> _battleActions;
        public ObservableCollection<DNDAction> BattleActions
        {
            get
            {
                if (ReferenceEquals(null, _battleActions))
                {
                    _battleActions = new ObservableCollection<DNDAction>();
                }

                return _battleActions;
            }
            set
            {
                _battleActions = value;
                OnPropertyChanged(nameof(BattleActions));
            }
        }

        private DNDRace _race;
        public DNDRace Race
        {
            get
            {
                return _race;
            }
            set
            {
                if (_race == value) return;

                _race = value;
                OnPropertyChanged(nameof(Race));
            }
        }

        public DNDCharacter()
        {
            Biography = new DNDCharacterBiography();
            //Biography.Name = "Dee Fault";
            //Biography.Nickname = "D";
            Alignment = Alignments.TRUE_NEUTRAL;
            Pronouns = "...";
            PlayerName = "...";
            //DNDClasses = new DNDClass();
            //DNDClasses.OnLevelChange += ClassChangedLevel;
            Stats = new DNDStats();

            CurrentHP = MaxHP - 1;
            ChangeProficiency();
        }

        public DNDCharacter(DNDCharacter characterToCopy)
        {
            Biography = new DNDCharacterBiography(characterToCopy.Biography);
            Alignment = Alignments.TRUE_NEUTRAL;
            Pronouns = characterToCopy.Pronouns;
            PlayerName = characterToCopy.PlayerName;

            //DNDClasses = new DNDClass(characterToCopy.DNDClasses);
            //BuildDummyClasses();

            DNDClasses = new ObservableCollection<DNDClass>();
            foreach (DNDClass c in characterToCopy.DNDClasses)
            {
                DNDClasses.Add(new DNDClass(c, this));
            }

            Stats = new DNDStats(characterToCopy.Stats);

            BuildDummyRace();

            //remove me
            DNDAction action = new DNDAction();
            action.ActionType = ActionType.Action;
            action.Name = "Action A";
            action.Description = "Description A";
            BattleActions.Add(action);
            action = new DNDAction();
            action.ActionType = ActionType.Action;
            action.Name = "Action B";
            action.Description = "Description B";
            BattleActions.Add(action);


            CurrentHP = characterToCopy.CurrentHP;
            ChangeProficiency();

            BuildDummyInventory();
        }

        #region CLASS METHODS
        public bool AddClass(DNDClassInfo classInfo)
        {
            if (ContainsClass(classInfo))
            {
                return false;
            }

            DNDClass c = new DNDClass(classInfo, this);
            c.OnLevelChange += ClassChangedLevel;
            DNDClasses.Add(c);
            c.AddSavingThrowProficiencies();
            return true;
        }

        private bool ContainsClass(DNDClassInfo classInfo)
        {
            foreach (DNDClass c in DNDClasses)
            {
                if (c.ClassInfo.Name == classInfo.Name)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveClass(DNDClass c)
        {
            //REMOVE SKILL PROFICIENCIES
            c.RemoveAllSkillProficiencies();
            c.RemoveAllSavingThrowProficiencies();

            DNDClasses.Remove(c);
            OnPropertyChanged(nameof(CurrentLevel));
            ChangeProficiency();
        }

        public void AddClassLevel(DNDClass c)
        {
            if (DNDClass.RemainingLevels > 0)
            {
                c.IncreaseLevel(1);
                //OnPropertyChanged(nameof(CurrentLevel));
            }
        }

        public void RemoveClassLevel(DNDClass c)
        {
            c.DecreaseLevel(1);
            //OnPropertyChanged(nameof(CurrentLevel));
        }
        #endregion

        #region INVENTORY METHODS
        public void BuildDummyInventory()
        {
            DNDItemArmor armorTest = new DNDItemArmor();
            armorTest.Name = "Leather Armor";
            armorTest.BaseAC = 11;
            armorTest.Type = ArmorType.Light;
            Inventory.Add(armorTest);

            DNDItem item = new DNDItem();
            item.Name = "ITEM A";
            Inventory.Add(item);
            item = new DNDItem();
            item.Name = "ITEM B";
            Inventory.Add(item);
            item = new DNDItem();
            item.Name = "ITEM C";
            Inventory.Add(item);
            item = new DNDItem();
            item.Name = "ITEM D";
            Inventory.Add(item);

            EquipItem(armorTest);
        }

        public void EquipItem(DNDItem item)
        {
            if (item is DNDItemArmor armor)
            {
                armor.EquipItem(this);
                EquippedInventory.Add(armor);
            }
        }

        public void ApplyArmorStats(int baseAC, ArmorType type)
        {
            //unnequip other armor
            armorAC = baseAC;
            equippedArmorType = type;

            OnPropertyChanged(nameof(AC));
        }
        #endregion

        #region RACE METHODS
        public void BuildDummyRace()
        {
            DNDRace race = new DNDRace(this);
            race.Name = "Dragonborn";

            race.Speeds = new DNDSpeeds();
            race.Speeds.WalkSpeed = 30;

            DNDAbilityScoreIncrease asi = new DNDAbilityScoreIncrease(StatType.STRENGTH, 2, this);
            race.ScoreIncreases.Add(asi);
            asi = new DNDAbilityScoreIncrease(StatType.DEXTERITY, 1, this);
            race.ScoreIncreases.Add(asi);

            Race = race;

            Race.ApplyRaceAbilityScoreIncreases();
            Race.ApplyRaceFeatures();
        }
        #endregion

        //private void ApplyRaceFeatures()
        //{
        //    Race.ApplyRaceFeatures();
        //}
        #region STATS METHODS
        public void AddStatBonus(StatType statType, int quantity)
        {
            Stats.AddStatBonus(statType, quantity);
            OnPropertyChanged(nameof(AC));
            OnPropertyChanged(nameof(MaxHP));
        }

        public void RemoveStatBonus(StatType statType, int quantity)
        {
            Stats.RemoveStatBonus(statType, quantity);
            OnPropertyChanged(nameof(AC));
            OnPropertyChanged(nameof(MaxHP));
        }

        public void RecalculateModifiers()
        {
            Stats.RecalculateStatSkillsModifiers();
        }

        public void ApplySkillProficiency(SkillTypes skillToImprove)
        {
            Stats.ApplySkillProficiency(skillToImprove);
        }

        public void RemoveSkillProficiency(SkillTypes skillToImprove)
        {
            Stats.RemoveSkillProficiency(skillToImprove);
        }

        public void ApplySavingThrowProficiency(StatType stat)
        {
            Stats.AddSavingThrowProficiency(stat);
        }

        public void RemoveSavingThrowProficiency(StatType stat)
        {
            Stats.RemoveSavingThrowProficiency(stat);
        }
        #endregion

        #region ACTION METHODS
        public void AddAction(DNDAction action)
        {
            BattleActions.Add(action);
        }

        public void RemoveAction(DNDAction action)
        {
            BattleActions.Remove(action);
        }
        #endregion

        /// <summary>
        /// Called when proficiency is changed
        /// </summary>
        public void ChangeProficiency()
        {
            Stats.SetProficiencyBonus(ProficiencyBonus);
            OnPropertyChanged(nameof(AC));
        }

        private void ClassChangedLevel()
        {
            DNDClass.RemainingLevels = MAX_LEVEL - CurrentLevel;
            ChangeProficiency();
            OnPropertyChanged(nameof(CurrentLevel));
        }

        public void UpdateCharacter(DNDCharacter c)
        {
            Stats = new DNDStats(c.Stats);
            Biography = new DNDCharacterBiography(c.Biography);
            OnPropertyChanged(nameof(AC));
        }

        public override string ToString()
        {
            return Biography.Name;
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
