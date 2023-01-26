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
using Newtonsoft.Json;
using System.IO;
using DNDApp;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace DNDApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string AAA { get; set; }

        public MainWindow()
        {
            //DNDDatabaseHandler.CreateTestJSON();
            //LoadCharacters();
            InitializeComponent();
            
        }
    }
}
