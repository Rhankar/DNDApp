using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DNDApp.ViewModels
{
    public class AddClassViewModel : ViewModelBase
    {
        public CharacterInfoViewModel CharInfoViewModel { get; set; }

        private ICommand _showCharacterInfoCommand;
        public ICommand ShowCharacterInfoCommand
        {
            get => _showCharacterInfoCommand;
            set
            {
                _UpdateField(ref _showCharacterInfoCommand, value);
                //if (_showCharacterInfoCommand == value) return;

                //_showCharacterInfoCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterInfoCommand));
            }
        }

        private ICommand _addClassToCharacterCommand;
        public ICommand AddClassToCharacterCommand
        {
            get => _addClassToCharacterCommand;
            set
            {
                _UpdateField(ref _addClassToCharacterCommand, value);
                //if (_showCharacterInfoCommand == value) return;

                //_showCharacterInfoCommand = value;
                //OnPropertyChanged(nameof(ShowCharacterInfoCommand));
            }
        }

        public DNDCharacter Character { get; set; } 

        public ObservableCollection<DNDClassInfo> ClassInfos { get; set; }
        private DNDClassInfo _selectedClassInfo;
        public DNDClassInfo SelectedClassInfo 
        {
            get => _selectedClassInfo;
            set
            {
                //_selectedClassInfo = value;
                _UpdateField(ref _selectedClassInfo, value);
            }
        }

        private DNDSubClassInfo _selectedSubClassInfo;
        public DNDSubClassInfo SelectedSubClassInfo
        {
            get => _selectedSubClassInfo;
            set
            {
                //_selectedClassInfo = value;
                _UpdateField(ref _selectedSubClassInfo, value);
            }
        }

        public AddClassViewModel()
        {
            ClassInfos = DNDDatabaseHandler.classesInfo;

            _addClassToCharacterCommand = new DelegateCommand(AddClassToCharacter);
        }

        public void AddClassToCharacter()
        {
            if (ReferenceEquals(null, SelectedClassInfo))
            {
                return;
            }

            bool wasAdded = Character.AddClass(SelectedClassInfo);

            if (wasAdded && ShowCharacterInfoCommand.CanExecute(null))
            {
                ShowCharacterInfoCommand.Execute(null);
            }
        }
    }
}
