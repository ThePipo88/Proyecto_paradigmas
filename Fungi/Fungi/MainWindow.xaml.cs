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

namespace Fungi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Fungi";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fileSpaceBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var caretIndex = fileSpaceBox.CaretIndex;
                fileSpaceBox.Text = fileSpaceBox.Text.Insert(caretIndex, "\n");
                fileSpaceBox.CaretIndex = caretIndex + 1;
            }
        }

        private void fileSpaceBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fileSpaceBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void fileSpaceNumber_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txtOutput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fileSpaceBox_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
