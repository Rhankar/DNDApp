using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private readonly CharacterInfoViewModel _characterInfoViewModel;
        private readonly CharacterEditingViewModel _characterEditingViewModel;
        private readonly AddClassViewModel _addClassViewModel;


        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel { 
            get
            {
                return _currentViewModel;
            }
            set
            {
                _UpdateField(ref _currentViewModel, value);
                //if (_currentViewModel == value) return;

                //_currentViewModel = value;
                //OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainViewModel()
        {
            //_characterInfoViewModel = new CharacterInfoViewModel
            //{
            //    ShowCharacterEditingCommand = new DelegateCommand(() => CurrentViewModel = _characterEditingViewModel)
            //};
            DNDDatabaseHandler.LoadClassesJSON();

            _characterEditingViewModel = new CharacterEditingViewModel();
            _characterEditingViewModel.ShowCharacterInfoCommand = new DelegateCommand(() => CurrentViewModel = _characterInfoViewModel);

            _addClassViewModel = new AddClassViewModel();
            _addClassViewModel.ShowCharacterInfoCommand = new DelegateCommand(() => CurrentViewModel = _characterInfoViewModel);

            _characterInfoViewModel = new CharacterInfoViewModel();
            _characterInfoViewModel.ShowCharacterEditingCommand = new DelegateCommand(() => CurrentViewModel = _characterEditingViewModel);
            _characterInfoViewModel.ShowAddClassCommand = new DelegateCommand(() => CurrentViewModel = _addClassViewModel);

            _characterEditingViewModel.CharInfoViewModel = _characterInfoViewModel;
            _addClassViewModel.CharInfoViewModel = _characterInfoViewModel;
            _characterInfoViewModel.CharEditingViewModel = _characterEditingViewModel;
            _characterInfoViewModel.AddClassViewModel = _addClassViewModel;

            //_characterEditingViewModel = new CharacterEditingViewModel
            //{
            //    ShowCharacterInfoCommand = new DelegateCommand(() => CurrentViewModel = _characterInfoViewModel)
            //};

            //_homeViewModel = new HomeViewModel
            //{
            //    ShowSub1Command = new DelegateCommand(() => CurrentViewModel = _sub1ViewModel),
            //    ShowSub2Command = new DelegateCommand(() => CurrentViewModel = _sub2ViewModel)
            //};

            CurrentViewModel = _characterInfoViewModel;

            DNDClass.RemainingLevels = 20;
        }
    }
}
