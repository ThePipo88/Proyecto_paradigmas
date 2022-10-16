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

        public String numLine = "1\n";

        public MainWindow()
        {
            InitializeComponent();
            fileLineSpace.Text = numLine;
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

        private void fileCodeSpace_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var caretIndex = fileCodeSpace.CaretIndex;
                fileCodeSpace.Text = fileCodeSpace.Text.Insert(caretIndex, "\n");
                numLine += fileCodeSpace.LineCount.ToString() + "\n";
                fileLineSpace.Text = numLine;
                fileCodeSpace.CaretIndex = caretIndex + 1;
            }
        }


        private void fileLineSpace_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var caretIndex = fileLineSpace.CaretIndex;
                fileLineSpace.Text = fileLineSpace.Text.Insert(caretIndex, "\n");
                fileLineSpace.CaretIndex = caretIndex + 1;
            }
        }


        private void txtOutput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var caretIndex = txtOutput.CaretIndex;
                txtOutput.Text = txtOutput.Text.Insert(caretIndex, "\n");
                txtOutput.CaretIndex = caretIndex + 1;
            }
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == sv1)
            {
                sv2.ScrollToVerticalOffset(e.VerticalOffset);
                sv2.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
            else
            {
                sv1.ScrollToVerticalOffset(e.VerticalOffset);
                sv1.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void fileLineSpace_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fileCodeSpace_TextChanged(object sender, TextChangedEventArgs e)
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
