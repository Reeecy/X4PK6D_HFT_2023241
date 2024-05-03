using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace X4PK6D_HFT_2023241.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PersonButton_Click(object sender, RoutedEventArgs e)
        {
            PersonWindow personWindow = new PersonWindow();
            personWindow.Show();
            this.Close();
        }

        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            PassWindow passWindow = new PassWindow();
            passWindow.Show();
            this.Close();
        }

        private void EntriesExitsButton_Click(object sender, RoutedEventArgs e)
        {
            EntriesExitsWindow entriesExitsWindow = new EntriesExitsWindow();
            entriesExitsWindow.Show();
            this.Close();
        }

    }
}