using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DNDApp.ViewModels
{
    public enum CharacterEditMode
    {
        None,
        NewCharacter,
        EditExistingCharacter,
    }

    public class CharacterEditingViewModel : ViewModelBase
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

        private ICommand _updateCharacterDataCommand;
        public ICommand UpdateCharacterDataCommand
        {
            get => _updateCharacterDataCommand;
            set
            {
                _UpdateField(ref _updateCharacterDataCommand, value);
            }
        }

        private DNDCharacter _characterToEdit;
        public DNDCharacter CharacterToEdit
        {
            get => _characterToEdit;
            set
            {
                _characterToEdit = new DNDCharacter(value);
                //PropertyChanged(nameof(CharacterToEdit));

            }
            //set => _UpdateField(ref characterToEdit, value);
        }

        public CharacterEditMode EditMode { get; set; }

        public CharacterEditingViewModel()
        {
            _updateCharacterDataCommand = new DelegateCommand(UpdateCurrentCharacterStats);
        }

        public void SetAddMode()
        {
            EditMode = CharacterEditMode.NewCharacter;
            CharacterToEdit = new DNDCharacter();
        }

        public void UpdateCurrentCharacterStats()
        {
            if (ShowCharacterInfoCommand.CanExecute(null))
            {
                switch(EditMode)
                {
                    case CharacterEditMode.EditExistingCharacter:
                        //CharInfoViewModel.CurrentCharacter.UpdateStats(CharacterToEdit.Stats);
                        CharInfoViewModel.UpdateCurrentCharacter(CharacterToEdit);
                        break;
                    case CharacterEditMode.NewCharacter:
                        CharInfoViewModel.AddCharacter(CharacterToEdit);
                        break;
                    default:
                        break;
                }
                
                ShowCharacterInfoCommand.Execute(null);
            }
        }

    }
}
