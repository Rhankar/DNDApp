using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Animation;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using static System.Windows.Forms.AxHost;

namespace DNDApp
{
    public class DNDClass : INotifyPropertyChanged
    {
        public static int RemainingLevels = 20;

        private DNDCharacter _owner;

        private int _level;
        public int Level { 
            get
            {
                return _level;
            }
            set
            {
                if(_level == value) return;

                _level = value;
                OnPropertyChanged(nameof(Level));

                if (!ReferenceEquals(classFeaturesCollection, null))
                {
                    ClassFeaturesSourceCollection.Refresh();
                }
            }
        }

        private List<int> _rolledHP;
        public List<int> RolledHP { 
            get
            {
                if(ReferenceEquals(null, _rolledHP))
                {
                    _rolledHP = new List<int>();
                }
                
                return _rolledHP;
            }
            set
            {
                _rolledHP = value; 
            } 
        }

        private ObservableCollection<SkillTypes> _choosenSkillProficiencies;
        public ObservableCollection<SkillTypes> ChoosenSkillProficiencies
        { 
            get 
            {
                if(ReferenceEquals(_choosenSkillProficiencies, null))
                {
                    _choosenSkillProficiencies = new ObservableCollection<SkillTypes>();
                }

                return _choosenSkillProficiencies;
            }
            set
            {
                _choosenSkillProficiencies = value;
            }
        }

        private SkillTypes _selectedItem;
        [JsonIgnore]
        public SkillTypes SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public int RemainingChoices
        {
            get
            {
                return ClassInfo.ChooseAmount - ChoosenSkillProficiencies.Count;
            }
        }

        public DNDClassInfo ClassInfo { get; set; }
        public DNDSubClassInfo SelectedSubclass { get; set; }

        private ObservableCollection<DNDClassFeature> _classFeatures;
        [JsonIgnore]
        public ObservableCollection<DNDClassFeature> ClassFeatures 
        {
            get => _classFeatures;
            set
            {
                _classFeatures = value;
            }
        }

        [JsonIgnore]
        public ICollectionView ClassFeaturesSourceCollection
        {
            get
            {
                return this.classFeaturesCollection.View;
            }
        }

        private CollectionViewSource classFeaturesCollection;

        public delegate void LevelChanged();
        public event LevelChanged OnLevelChange;

        public DNDClass(DNDClassInfo classInfo, DNDCharacter owner)
        {
            this.ClassInfo = classInfo;
            Level = 1;
            _owner = owner;

            AddClassFeatures();
            //ChoosenSkillProficiencies.Add(ClassInfo.SkillProficienciesChoices[0]);
            //IncreaseLevel(1);
            //RolledHP = new List<int>();
            //RolledHP.Add(10);
        }

        private void AddClassFeatures()
        {
            ClassFeatures = new ObservableCollection<DNDClassFeature>();

            foreach (DNDClassFeature f in ClassInfo.ClassFeatures)
            {
                ClassFeatures.Add(f);
            }

            classFeaturesCollection = new CollectionViewSource();
            classFeaturesCollection.Source = ClassFeatures;
            classFeaturesCollection.Filter += new FilterEventHandler(IsFeatureBelowOrEqualLevel);
        }

        public void IsFeatureBelowOrEqualLevel(object sender, FilterEventArgs e)
        {
            DNDClassFeature feature = e.Item as DNDClassFeature;

            if (feature != null)
            {
                if (feature.Level <= Level)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DNDClass(DNDClass copy, DNDCharacter owner)
        {
            Level = copy.Level;
            //IncreaseLevel(copy.Level);
            RolledHP = new List<int>();
            RolledHP.AddRange(copy.RolledHP);

            _owner = owner;
            //RolledHP = new List<int>();
            //RolledHP.Add(10);
        }

        public int GetTotalRolledHP()
        {
            int total = 0;

            foreach (int number in RolledHP)
            {
                total += number;
            }

            return total;
        }

        public void IncreaseLevel(int quantity)
        {
            Level += quantity;
            OnLevelChange?.Invoke();
        }

        public void DecreaseLevel(int quantity)
        {
            if (Level <= 1)
            {
                return;
            }

            Level -= quantity;
            OnLevelChange?.Invoke();
        }

        public void AddSkillProficiency()
        {
            if (CanAddSkillChoice())
            {
                ChoosenSkillProficiencies.Add(SelectedItem);
                _owner.ApplySkillProficiency(SelectedItem);

                OnPropertyChanged(nameof(RemainingChoices));
            }
        }

        private bool IsSkillAdded()
        {
            return ChoosenSkillProficiencies.Contains(SelectedItem);
        }

        private bool CanAddSkillChoice()
        {
            return (RemainingChoices > 0) && !IsSkillAdded();
        }

        public void RemoveAllSkillProficiencies()
        {
            foreach (SkillTypes t in ChoosenSkillProficiencies)
            {
                _owner.RemoveSkillProficiency(t);
            }
        }

        public void RevomeSkillProficiency(SkillTypes skill)
        {
            ChoosenSkillProficiencies.Remove(skill);
            _owner.RemoveSkillProficiency(skill);
        }

        public void AddSavingThrowProficiencies()
        {
            foreach (StatType t in ClassInfo.SavingThrowProficiencies)
            {
                _owner.ApplySavingThrowProficiency(t);
            }  
        }

        public void RemoveAllSavingThrowProficiencies()
        {
            foreach (StatType t in ClassInfo.SavingThrowProficiencies)
            {
                _owner.RemoveSavingThrowProficiency(t);
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
