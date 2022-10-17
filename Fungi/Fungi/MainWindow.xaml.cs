using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        public String numLine = "1\r\n";
        public int count = 1;


        public MainWindow()
        {
            this.RemoveHandler(KeyDownEvent, new KeyEventHandler(fileCodeSpace_KeyDown));

            this.AddHandler(KeyDownEvent, new KeyEventHandler(fileCodeSpace_KeyDown), true);
            InitializeComponent();
            fileLineSpace.Text = numLine;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void opNuevo_click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, fileCodeSpace.Text);

        }

        private void opAbrir_click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                string[] lines = System.IO.File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    fileCodeSpace.Text += line+'\n';
                }
            }

        }

        private void fileCodeSpace_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                count++;
                fileLineSpace.Text = "";
                var caretIndex = fileCodeSpace.CaretIndex;
                fileCodeSpace.Text = fileCodeSpace.Text.Insert(caretIndex, "\n");
                numLine += count.ToString() + "\r\n";
                fileLineSpace.Text = numLine;
                fileCodeSpace.CaretIndex = caretIndex + 1;
            }
            else if (e.Key == Key.Back)
            {
                if (fileCodeSpace.Text[(fileCodeSpace.Text.Length)-1] == '\n')
                {

                    fileLineSpace.Text = fileLineSpace.Text.Remove(fileLineSpace.Text.LastIndexOf(Environment.NewLine));
                    fileLineSpace.Text = fileLineSpace.Text.Remove(fileLineSpace.Text.LastIndexOf(Environment.NewLine));

                    fileLineSpace.AppendText("\r\n");

                    numLine = numLine.Remove(numLine.LastIndexOf(Environment.NewLine));
                    numLine = numLine.Remove(numLine.LastIndexOf(Environment.NewLine));
                    numLine += "\r\n";

                    var caretIndex = fileCodeSpace.CaretIndex;
                    count--;
                    fileCodeSpace.Select((fileCodeSpace.Text.Length), 0);
                }
                
            }
        }


        private void fileLineSpace_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            fileLineSpace.Text = fileLineSpace.Text.Remove(fileLineSpace.Text.LastIndexOf(Environment.NewLine));
            fileLineSpace.Text = fileLineSpace.Text.Remove(fileLineSpace.Text.LastIndexOf(Environment.NewLine));
            fileLineSpace.AppendText("\r\n");
        }
    }
}
