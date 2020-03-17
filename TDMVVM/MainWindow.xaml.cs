using ContactDLL;
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

namespace TDMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Personne> ListPersonnes = new List<Personne>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = PersonneSingleton.Instance.ListPersonnes;

        }

        private void ucContact_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
