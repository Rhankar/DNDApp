using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DNDApp.Views
{
    /// <summary>
    /// Interaction logic for CharacterInfoView.xaml
    /// </summary>
    public partial class CharacterInfoView : UserControl
    {
        public CharacterInfoView()
        {
            InitializeComponent();
            //viewModel = new ViewModels.CharacterInfoViewModel();
            //this.DataContext = viewModel;

            //BindCurrentCharacterDataContexts();
        }
    }
}
