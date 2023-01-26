using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CharacterEditingView.xaml
    /// </summary>
    public partial class CharacterEditingView : UserControl
    {
        public CharacterEditingView()
        {
            InitializeComponent();
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[^0-9\\s]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StatsPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
