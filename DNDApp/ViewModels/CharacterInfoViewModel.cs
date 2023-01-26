using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace DNDApp.ViewModels
{
    public class CharacterInfoViewModel : ViewModelBase
    {
        private ICommand _showCharacterEditingCommand;
        public ICommand ShowCharacterEditingCommand
        {
            get => _showCharacterEditingCommand;
            set
            {
                _UpdateField(ref _showCharacterEditingCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _showAddClassCommand;
        public ICommand ShowAddClassCommand
        {
            get => _showAddClassCommand;
            set
            {
                _UpdateField(ref _showAddClassCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _editCurrentCharacterCommand;
        public ICommand EditCurrentCharacterCommand
        {
            get => _editCurrentCharacterCommand;
            set
            {
                _UpdateField(ref _editCurrentCharacterCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _addNewCharacterCommand;
        public ICommand AddNewCharacterCommand
        {
            get => _addNewCharacterCommand;
            set
            {
                _UpdateField(ref _addNewCharacterCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _deleteCharacterCommand;
        public ICommand DeleteCharacterCommand
        {
            get => _deleteCharacterCommand;
            set
            {
                _UpdateField(ref _deleteCharacterCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _saveCharactersCommand;
        public ICommand SaveCharactersCommand
        {
            get => _saveCharactersCommand;
            set
            {
                _UpdateField(ref _saveCharactersCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _loadCharactersCommand;
        public ICommand LoadCharactersCommand
        {
            get => _loadCharactersCommand;
            set
            {
                _UpdateField(ref _loadCharactersCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _removeCharacterClassCommand;
        public ICommand RemoveCharacterClassCommand
        {
            get => _removeCharacterClassCommand;
            set
            {
                _UpdateField(ref _removeCharacterClassCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _addClassLevelCommand;
        public ICommand AddClassLevelCommand
        {
            get => _addClassLevelCommand;
            set
            {
                _UpdateField(ref _addClassLevelCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _removeClassLevelCommand;
        public ICommand RemoveClassLevelCommand
        {
            get => _removeClassLevelCommand;
            set
            {
                _UpdateField(ref _removeClassLevelCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _addClassCommand;
        public ICommand AddClassCommand
        {
            get => _addClassCommand;
            set
            {
                _UpdateField(ref _addClassCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _addClassSkillChoiceCommand;
        public ICommand AddClassSkillChoiceCommand
        {
            get => _addClassSkillChoiceCommand;
            set
            {
                _UpdateField(ref _addClassSkillChoiceCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        private ICommand _removeClassSkillChoiceCommand;
        public ICommand RemoveClassSkillChoiceCommand
        {
            get => _removeClassSkillChoiceCommand;
            set
            {
                _UpdateField(ref _removeClassSkillChoiceCommand, value);
                //if (_showCharacterEditingCommand == value) return;

                //_showCharacterEditingCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterEditingCommand));
            }
        }

        public CharacterEditingViewModel CharEditingViewModel { get; set; }
        public AddClassViewModel AddClassViewModel { get; set; }

        private ObservableCollection<DNDCharacter> _character;
        public ObservableCollection<DNDCharacter> Characters {
            get
            {
                return _character ?? (_character = new ObservableCollection<DNDCharacter>());
            }
            set
            {
                _UpdateField(ref _character, value);
            }
        }

        private DNDCharacter currentCharacter;
        public DNDCharacter CurrentCharacter //TODO: Separate the current character (aka, don't do currentCharacter = X, add a Copy Constructer to it)
        {
            get
            {
                return currentCharacter;
            }
            set
            {
                _UpdateField(ref currentCharacter, value);
            }
        }

        public int SelectedItem { get; set; }

        public ObservableCollection<DNDClassInfo> ClassInfos { get; set; } //remove me

        public CharacterInfoViewModel()
        {
            //CharEditingViewModel = characterEditingViewModel;
            _editCurrentCharacterCommand = new DelegateCommand(EditCurrentCharacter);
            _addNewCharacterCommand = new DelegateCommand(AddNewCharacter);
            _deleteCharacterCommand = new DelegateCommand(DeleteCharacter);
            _saveCharactersCommand = new DelegateCommand(SaveCharacters);
            _loadCharactersCommand = new DelegateCommand(LoadCharacters);
            _removeCharacterClassCommand = new RelayCommand(RemoveCharacterClass);
            _addClassLevelCommand = new RelayCommand(AddClassLevel);
            _removeClassLevelCommand = new RelayCommand(RemoveClassLevel);
            _addClassCommand = new DelegateCommand(AddClass);
            _addClassSkillChoiceCommand = new RelayCommand(AddClassSkillChoice);
            _removeClassSkillChoiceCommand = new RelayCommand(RemoveClassSkillChoice);

            Characters = new ObservableCollection<DNDCharacter>();
            currentCharacter = new DNDCharacter();

            ClassInfos = DNDDatabaseHandler.classesInfo;
            //DNDDatabaseHandler.CreateTestJSON();
            //LoadCharacters();
        }

        private void LoadCharacters()
        {
            Characters = DNDDatabaseHandler.LoadCharactersJSON();
            //Characters = DNDDatabaseHandler.characters;

            if (ReferenceEquals(Characters, null))
            {
                Console.WriteLine("JSON deserialization failed");
                return;
            }

            SelectedItem = 0;
        }

        public void SetCurrentCharacter(DNDCharacter character)
        {
            this.CurrentCharacter = character;
            CurrentCharacter.RecalculateModifiers();
            CurrentCharacter.ChangeProficiency();
        }

        public void EditCurrentCharacter()
        {
            if (ShowCharacterEditingCommand.CanExecute(null))
            {
                CharEditingViewModel.EditMode = CharacterEditMode.EditExistingCharacter;
                CharEditingViewModel.CharacterToEdit = CurrentCharacter;
                ShowCharacterEditingCommand.Execute(null);
            }
        }

        public void AddNewCharacter()
        {
            if (ShowCharacterEditingCommand.CanExecute(null))
            {
                CharEditingViewModel.SetAddMode();
                //CharEditingViewModel.EditMode = CharacterEditMode.NewCharacter;
                //CharEditingViewModel.CharacterToEdit = CurrentCharacter;
                ShowCharacterEditingCommand.Execute(null);
            }
        }

        public void DeleteCharacter()
        {
            int itemNumber = SelectedItem;

            Characters.Remove(CurrentCharacter);
            Debug.WriteLine("Item: " + SelectedItem);

            if (itemNumber > 0)
            {
                itemNumber--;
            }

            //SelectedItem = itemNumber;
            if (Characters.Count == 0)
            {
                CurrentCharacter = null;
            } else
            {
                CurrentCharacter = Characters[itemNumber];
            }
            
        }

        public void SaveCharacters()
        {
            DNDDatabaseHandler.SaveCharacters(Characters);
        }

        public void RemoveCharacterClass(object item)
        {
            DNDClass c = item as DNDClass;
            CurrentCharacter.RemoveClass(c);
        }

        public void AddClassLevel(object item)
        {
            DNDClass c = item as DNDClass;
            Debug.WriteLine(c.ClassInfo.Name);
            CurrentCharacter.AddClassLevel(c);
        }

        public void RemoveClassLevel(object item)
        {
            DNDClass c = item as DNDClass;
            Debug.WriteLine(c.ClassInfo.Name);
            CurrentCharacter.RemoveClassLevel(c);
        }

        public void AddClass()
        {
            if (ShowAddClassCommand.CanExecute(null))
            {
                AddClassViewModel.Character = CurrentCharacter;
                ShowAddClassCommand.Execute(null);
            }
        }

        public void AddClassSkillChoice(object item)
        {
            DNDClass c = item as DNDClass;

            c.AddSkillProficiency();
            //c.ChoosenSkillProficiencies.Add(c.SelectedItem);
            //Debug.WriteLine(c.ChoosenSkillProficiencies);
        }

        public void RemoveClassSkillChoice(object item)
        {
            //SkillTypes t =  Enum.Parse<SkillTypes>(item.ToString());
            if (item is object[] parameters)
            {
                if (!(parameters[0] is SkillTypes))
                {
                    return;
                }

                if (!(parameters[1] is DNDClass))
                {
                    return;
                }

                SkillTypes t =  Enum.Parse<SkillTypes>(parameters[0].ToString());
                DNDClass c = parameters[1] as DNDClass;

                c.RevomeSkillProficiency(t);
                //c.ChoosenSkillProficiencies.Remove(t);
            }

            //Debug.WriteLine("hi");
        }

        public void UpdateCurrentCharacter(DNDCharacter character)
        {
            CurrentCharacter.UpdateCharacter(character);
            //this.currentCharacter = character;
            CurrentCharacter.RecalculateModifiers();
            CurrentCharacter.ChangeProficiency();
        }

        public void AddCharacter(DNDCharacter character)
        {
            Characters.Add(character);
            SetCurrentCharacter(character);
            SelectedItem = Characters.IndexOf(character);
            //CurrentCharacter = character;
            //CurrentCharacter.RecalculateModifiers();
            //CurrentCharacter.ChangeProficiency();
        }
    }

    public class ArrayMultiValueConverter : IMultiValueConverter
    {
        #region interface implementations

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ProficiencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SkillProficiency)value)
            {
                case SkillProficiency.Proficient:
                    return "⚫";
                case SkillProficiency.Expert:
                    return "🟆";
                default:
                    return "⚪";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
